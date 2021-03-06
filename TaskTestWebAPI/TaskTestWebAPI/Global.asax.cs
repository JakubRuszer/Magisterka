﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

using System.Web.Http.Hosting;
using TaskTestWebAPI.Controllers.Utils;

namespace TaskTestWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHostBufferPolicySelector), new NoBufferPolicyModifier());
        }
    }
}
