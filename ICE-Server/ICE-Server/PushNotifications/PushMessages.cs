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

namespace ICE_Server.PushNotifications
{
    public class Pushmessage
    {
        private string title { get; set; }
        private string message { get; set; }
        private DevicesRepository deviceRepository;

        public enum PushTypes
        {
            requestLocations,
            sendPushNotifications
        }

        /// <summary>
        /// Push a message 
        /// </summary>
        /// <param name="message">String of the message</param>
        public Pushmessage(string title, string message)
        {
            this.deviceRepository = new DevicesRepository(new ICEContext());

            // Configuration: Setup the GCM sender information here
            var config = new GcmConfiguration("877886927121 ", "AIzaSyBksWdag7DeN7h4jRM0gqLgjN6fyEcQ8r0", null);

            // Create a new broker
            var gcmBroker = new GcmServiceBroker(config);

            // Wire up events
            gcmBroker.OnNotificationFailed += (notification, aggregateEx) => {

                aggregateEx.Handle(ex => {

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

            gcmBroker.OnNotificationSucceeded += (notification) => {
                Console.WriteLine("GCM Notification Sent!");
            };
            
            // Start the broker
            gcmBroker.Start();
            foreach (var regId in deviceRepository.GetAll())
            {
                // Queue a notification to send
                gcmBroker.QueueNotification(new GcmNotification
                {
                    RegistrationIds = new List<string> { regId.DeviceID },
                    Data = JObject.Parse("{\"Title\":\"" + title + "\",\"Content\":\"" + message + "\"}")
                });
            }

            // Stop the broker, wait for it to finish   
            // This isn't done after every message, but after you're
            // done with the broker
            gcmBroker.Stop();
        }
    }
}