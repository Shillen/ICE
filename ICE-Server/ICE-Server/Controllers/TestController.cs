﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICE_Server.Controllers
{
    public class TestController : ApiController
    {
        // GET: api/test
        public string Get()
        {
            return "test";
        }
    }
}
