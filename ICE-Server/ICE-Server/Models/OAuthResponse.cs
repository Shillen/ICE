using ICE_Server.Models.Views.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    public class OAuthResponse
    {
        public DateTimeOffset Expires { get; set; }
        public DateTimeOffset Issued { get; set; }
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public UserViewModel User { get; set; }
    }
}