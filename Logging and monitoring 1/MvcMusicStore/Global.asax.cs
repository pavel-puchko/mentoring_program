using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using NLog;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILogger _logger;

        public MvcApplication()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _logger.Info("App started.");
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();

            _logger.Error("Path: {0}; \nStack trace: \n{1}", Request.RawUrl, ex.StackTrace);
        }
    }
}
