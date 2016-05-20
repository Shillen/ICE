using ICE_Server.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICE_Server.Controllers
{
    [ICEApiAuthorize(Roles="Admin")]
    public class SettingsController : BaseController
    {
    }
}
