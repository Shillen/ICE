using PushSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using ICE_Server.Repository;
using ICE_Server.Models;
using ICE_Server.DAL;
using PushSharp.Google;
using PushSharp.Core;
using Newtonsoft.Json.Linq;
using PushSharp.Apple;

namespace ICE_Server.PushNotifications
{
    public class Pushmessage
    {
        private string title { get; set; }
        private string message { get; set; }
        private int? predefinedID  { get; set; }
        private List<Building> buildings { get; set; }
        private string buildingMsg { get; set; }
        private DevicesRepository deviceRepository;

        public enum PushTypes
        {
            Emergency = 1,
            Update
        }

        public static string getPath()
        {
            var actualPath = HttpContext.Current.Server.MapPath("/Resources/ice-apns-dev-cert.p12");
            return actualPath;
        }

        /// <summary>
        /// Push a message 
        /// </summary>
        /// <param name="message">String of the message</param>
        public Pushmessage(PushTypes pushType, BroadcastItem item)
        //public Pushmessage(PushTypes pushType, int title, string message, int? predefinedID)
        { 
            this.deviceRepository = new DevicesRepository(new ICEContext());

            title = item.EmergencyId.ToString();

            // update variables
            //if (pushType == PushTypes.Emergency)
            //{
            //    title = "1";
            //}
            //if (pushType == PushTypes.Update)
            //{
            //    title = "2";
            //}
            buildings = item.Buildings;
            message = item.Message;

            // Configuration: Setup the GCM sender information here
            var config = new GcmConfiguration("504565507826", "AIzaSyAvQgiq9HMGVULdUVzloqW3DSghFTTJ5Wc", null);
            var configApple = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox,
                                    getPath(), "Swampmonster");

            // Windows push notification is currently NOT enabled. Please add the credentials here and uncomment the code which start with WIN-PUSH-ENABLE THIS


            // Create a new broker
            var gcmBroker = new GcmServiceBroker(config);
            var apnsBroker = new ApnsServiceBroker(configApple);
            // WIN PUSH ENABLE THIS
            // var config = new WnsConfiguration("WNS_PACKAGE_NAME", "WNS_PACKAGE_SID", "WNS_CLIENT_SECRET");

            // Apple feedback service
            //var fbs = new FeedbackService(configApple);
            //fbs.FeedbackReceived += (string deviceToken, DateTime timestamp) => {
            //    // Remove the deviceToken from your database
            //    // timestamp is the time the token was reported as expired
            //    deviceRepository.DeleteToken(deviceToken);
            //};
            //fbs.Check();

            // Wire up events for Google 
            gcmBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {

                aggregateEx.Handle(ex =>
                {

                    // See what kind of exception it was to further diagnose
                    if (ex is GcmNotificationException)
                    {
                        var notificationException = (GcmNotificationException)ex;

                        // Deal with the failed notification
                        var gcmNotification = notificationException.Notification;
                        var description = notificationException.Description;

                        Console.WriteLine($"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
                    }
                    else if (ex is GcmMulticastResultException)
                    {
                        var multicastException = (GcmMulticastResultException)ex;

                        foreach (var succeededNotification in multicastException.Succeeded)
                        {
                            Console.WriteLine($"GCM Notification Failed: ID={succeededNotification.MessageId}");
                        }

                        foreach (var failedKvp in multicastException.Failed)
                        {
                            var n = failedKvp.Key;
                            var e = failedKvp.Value;

                            Console.WriteLine($"GCM Notification Failed: ID={n.MessageId}, Desc={e.Message}");
                        }

                    }
                    else if (ex is DeviceSubscriptionExpiredException)
                    {
                        var expiredException = (DeviceSubscriptionExpiredException)ex;

                        var oldId = expiredException.OldSubscriptionId;
                        var newId = expiredException.NewSubscriptionId;

                        Console.WriteLine($"Device RegistrationId Expired: {oldId}");

                        if (!string.IsNullOrWhiteSpace(newId))
                        {
                            // If this value isn't null, our subscription changed and we should update our database
                            deviceRepository.UpdateToken(oldId, newId);
                            Console.WriteLine($"Device RegistrationId Changed To: {newId}");
                        }
                        if (!string.IsNullOrWhiteSpace(oldId))
                        {
                            deviceRepository.DeleteToken(oldId);
                        }
                    }
                    else if (ex is RetryAfterException)
                    {
                        var retryException = (RetryAfterException)ex;
                        // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
                        Console.WriteLine($"GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}");
                    }
                    else {
                        Console.WriteLine("GCM Notification Failed for some unknown reason");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            gcmBroker.OnNotificationSucceeded += (notification) =>
            {
                Console.WriteLine("GCM Notification Sent!");
            };

            // Wire up Apple events
            apnsBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {

                aggregateEx.Handle(ex =>
                {

                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException)
                    {
                        var notificationException = (ApnsNotificationException)ex;

                        // Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        Console.WriteLine($"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");

                    }
                    else {
                        // Inner exception might hold more useful information like an ApnsConnectionException           
                        Console.WriteLine($"Apple Notification Failed for some unknown reason : {ex.InnerException}");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            apnsBroker.OnNotificationSucceeded += (notification) =>
            {
                Console.WriteLine("Apple Notification Sent!");
            };

            // WIN PUSH ENABLE THIS
            // Wire up Windows events
            //wnsBroker.OnNotificationFailed += (notification, aggregateEx) => {

            //    aggregateEx.Handle(ex => {

            //        // See what kind of exception it was to further diagnose
            //        if (ex is WnsNotificationException)
            //        {
            //            var notificationException = (WnsNotificationException)ex;
            //            Console.WriteLine($"WNS Notification Failed: {notificationException.Message}");
            //        }
            //        else {
            //            Console.WriteLine("WNS Notification Failed for some (Unknown Reason)");
            //        }

            //        // Mark it as handled
            //        return true;
            //    });
            //};


            // Start the broker
            gcmBroker.Start();
            apnsBroker.Start();
            // WIN PUSH ENABLE THIS
            //wnsBroker.Start();


            string androidParse;
            string iOSParse;
            // WIN PUSH ENABLE THIS
            //string windowsParse;
            buildingMsg = "[";
            foreach (Building buildingitem in buildings)
            {
                buildingMsg += buildingitem.ID + ",";
            }
            buildingMsg = buildingMsg.Remove(buildingMsg.LastIndexOf(","), 1);
            buildingMsg += "]";

            if (predefinedID == null)
            {
                androidParse = "{\"Type\":" + (int)pushType + ",\"Title\":\"" + title + "\",\"Content\":{\"Message\":\"" + message + "\", \"Building\":" + buildingMsg + "}}";
                //iOSParse = "{\"aps\":{\"alert\":\"New emergency\",\"badge\":\"1\", \"ICE\":{\"Type\":1,\"Title\":1,\"Content\":{\"Message\":\"" + message + ", \"Building\":" + buildingMsg + "}}}}";
                iOSParse = "{\"aps\":{\"alert\":\"New emergency\",\"badge\":\"1\", \"ICE\":" + androidParse + "}}";
            }
            else
            {
                androidParse = "{\"Type\":" + (int)pushType + ",\"Title\":\"" + title + "\",\"Content\":{\"Message\":\"" + message + "\", \"Building\":" + buildingMsg + "\",\"PredefinedMessage\":" + predefinedID + "}}";
                iOSParse = "{\"aps\":{\"alert\":\"New emergency\",\"badge\":\"1\", \"ICE\":{\"Type\":1,\"Title\":1,\"Content\":{\"Message\":\"" + message + ", \"Building\":" + buildingMsg + "}}}}";
            }
            
            foreach (var regId in deviceRepository.GetAll())
             {
                if (regId.DeviceOS == OS.Android)
                {
                    //Debug.WriteLine("Test: "+ androidParse); 
                    // Queue an Android notification to send
                    gcmBroker.QueueNotification(new GcmNotification
                    {
                        RegistrationIds = new List<string> { regId.DeviceID },
                        Data = JObject.Parse(androidParse)
                    });

                }
                if (regId.DeviceOS == OS.iOS)
                {
                    // Queue an Apple notification to send
                    apnsBroker.QueueNotification(new ApnsNotification
                    {
                        DeviceToken = regId.DeviceID,
                        Payload = JObject.Parse(iOSParse)
                    });
                }
                if (regId.DeviceOS == OS.WinPhone)
                {
                    // Queue an Windows notification to send
                    // WIN PUSH ENABLE THIS (ALSO EDIT THIS PART)
                    //wnsBroker.QueueNotification(new WnsToastNotification
                    //{
                    //    ChannelUri = uri,
                    //    Payload = XElement.Parse (@"
                                                //< toast >
                                                //    < visual >
                                                //        < binding template = ""ToastText01"" >
   
                                                //               < text id = ""1"" > WNS_Send_Single </ text >
      
                                                //              </ binding >
      
                                                //          </ visual >
      
                                                //      </ toast > ")
                    //});
                }
            }

            // Stop the broker, wait for it to finish   
            // This isn't done after every message, but after you're done with the broker
            gcmBroker.Stop();
            apnsBroker.Stop();
        }
    }
}