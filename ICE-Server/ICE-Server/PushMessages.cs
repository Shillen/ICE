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
        private string pushMessage { get; set; }
        private bool succces = true;

        /// <param name="message">String of the message</param>
        public Pushmessage(string pushMessage)
        {
            this.pushBroker = new PushBroker();
            this.pushBroker.RegisterGcmService(new GcmPushChannelSettings("AIzaSyBLIvuek1e_DKEk0wnXddj3RtkSUO8ZluY"));
        }
    }
}