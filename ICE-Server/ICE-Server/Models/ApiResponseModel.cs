using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    public class ApiResponseModel
    {
        public string Message { get; set; }
        public JObject ModelState { get; set; }
    }
}