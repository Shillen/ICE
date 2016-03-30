//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Web;
//using PushSharp;
//using System.Data.Entity;
//using ICE_Server.DAL;
//using PushSharp.Apple;
//using PushSharp.Google;
//using PushSharp.Core;
//using Newtonsoft.Json.Linq;
//using ICE_Server.Models;

//namespace ICE_Server
//{
//    public class PushMessages
//    {
//        // Create variabeles
//        List<Devices> AllDevices;

//        public enum PushTypes
//        {
//            SendBroadcast,
//            CheckUpdates
//        }

//        private void SendAppleNotification(string deviceID)
//        {
//            var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox, "push-cert.p12", "push-cert-pwd");

//            // Create a new broker
//            var apnsBroker = new ApnsServiceBroker(config);

//            // Wire up events
//            apnsBroker.OnNotificationFailed += (notification, aggregateEx) =>
//            {

//                aggregateEx.Handle(ex =>
//                {

//                    // See what kind of exception it was to further diagnose
//                    if (ex is ApnsNotificationException)
//                    {
//                        var notificationException = (ApnsNotificationException)ex;

//                        // Deal with the failed notification
//                        var apnsNotification = notificationException.Notification;
//                        var statusCode = notificationException.ErrorStatusCode;

//                        Console.WriteLine($"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");

//                    }
//                    else {
//                        // Inner exception might hold more useful information like an ApnsConnectionException           
//                        Console.WriteLine($"Apple Notification Failed for some unknown reason : {ex.InnerException}");
//                    }

//                    // Mark it as handled
//                    return true;
//                });
//            };

//            apnsBroker.OnNotificationSucceeded += (notification) =>
//            {
//                Console.WriteLine("Apple Notification Sent!");
//            };

//            // Start the broker
//            apnsBroker.Start();

//            foreach (var deviceToken in AllDevices)
//            {
//                // Queue a notification to send
//                apnsBroker.QueueNotification(new ApnsNotification
//                {
//                    DeviceToken = deviceToken,
//                    Payload = JObject.Parse("{\"aps\":{\"badge\":7}}")
//                });
//            }

//            // Stop the broker, wait for it to finish   
//            // This isn't done after every message, but after you're
//            // done with the broker
//            apnsBroker.Stop();
//        }

//        private void SendAndroidNotification(string deviceID)
//        {
//            // Configuration
//            var config = new GcmConfiguration("239888546853", "AIzaSyBLIvuek1e_DKEk0wnXddj3RtkSUO8ZluY", null);

//            // Create a new broker
//            var gcmBroker = new GcmServiceBroker(config);

//            // Wire up events
//            gcmBroker.OnNotificationFailed += (notification, aggregateEx) =>
//            {

//                aggregateEx.Handle(ex =>
//                {

//                    // See what kind of exception it was to further diagnose
//                    if (ex is GcmNotificationException)
//                    {
//                        var notificationException = (GcmNotificationException)ex;

//                        // Deal with the failed notification
//                        var gcmNotification = notificationException.Notification;
//                        var description = notificationException.Description;

//                        Console.WriteLine($"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
//                    }
//                    else if (ex is GcmMulticastResultException)
//                    {
//                        var multicastException = (GcmMulticastResultException)ex;

//                        foreach (var succeededNotification in multicastException.Succeeded)
//                        {
//                            Console.WriteLine($"GCM Notification Failed: ID={succeededNotification.MessageId}");
//                        }

//                        foreach (var failedKvp in multicastException.Failed)
//                        {
//                            var n = failedKvp.Key;
//                            var e = failedKvp.Value;

//                            Console.WriteLine($"GCM Notification Failed: ID={n.MessageId}, Desc={e.Description}");
//                        }

//                    }
//                    else if (ex is DeviceSubscriptionExpiredException)
//                    {
//                        var expiredException = (DeviceSubscriptionExpiredException)ex;

//                        var oldId = expiredException.OldSubscriptionId;
//                        var newId = expiredException.NewSubscriptionId;

//                        Console.WriteLine($"Device RegistrationId Expired: {oldId}");

//                        if (!string.IsNullOrWhitespace(newId))
//                        {
//                            // If this value isn't null, our subscription changed and we should update our database
//                            Console.WriteLine($"Device RegistrationId Changed To: {newId}");
//                        }
//                    }
//                    else if (ex is RetryAfterException)
//                    {
//                        var retryException = (RetryAfterException)ex;
//                        // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
//                        Console.WriteLine($"GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}");
//                    }
//                    else {
//                        Console.WriteLine("GCM Notification Failed for some unknown reason");
//                    }

//                    // Mark it as handled
//                    return true;
//                });
//            };

//            gcmBroker.OnNotificationSucceeded += (notification) =>
//            {
//                Console.WriteLine("GCM Notification Sent!");
//            };

//            // Start the broker
//            gcmBroker.Start();

//            foreach (var regId in MY_REGISTRATION_IDS)
//            {
//                // Queue a notification to send
//                gcmBroker.QueueNotification(new GcmNotification
//                {
//                    RegistrationIds = new List<string> {
//            regId
//        },
//                    Data = JObject.Parse("{ \"somekey\" : \"somevalue\" }")
//                });
//            }

//            // Stop the broker, wait for it to finish   
//            // This isn't done after every message, but after you're
//            // done with the broker
//            gcmBroker.Stop();
//        }

//    }

//}