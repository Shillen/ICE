using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using PushSharp;


namespace ICE_Server
{
    public class PushMessages
    {
        private PushBroker pushBroker { get; set; }
        private string message { get; set; }
        private bool succces = true;

        /// <param name="message">String of the message</param>
        public Pushmessage(string message)
        {
            this.message = message;
            this.pushBroker = new PushBroker();
            
            // Regiser all the events
            this.pushBroker.OnChannelCreated += push_OnChannelCreated;
            this.pushBroker.OnChannelDestroyed += push_OnChannelDestroyed;
            this.pushBroker.OnChannelException += push_OnChannelException;
            this.pushBroker.OnNotificationRequeue += push_OnNotificationRequeue;
            this.pushBroker.OnServiceException += push_OnServiceException;
            this.pushBroker.OnNotificationSent += push_OnNotificationSent;
            this.pushBroker.OnNotificationFailed += push_OnNotificationFailed;
            this.pushBroker.OnDeviceSubscriptionExpired += push_OnDeviceSubscriptionExpired;

            this.pushBroker.RegisterGcmService(new GcmPushChannelSettings("AIzaSyBLIvuek1e_DKEk0wnXddj3RtkSUO8ZluY"));
        }
    }
}