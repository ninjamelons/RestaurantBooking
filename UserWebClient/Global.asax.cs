using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UserWebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterComponents();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_OnEnd(object sender, EventArgs e)
        {
            var orderId = Session["orderId"] as int?;
            if(orderId != null)
            {
                OrderService.OrderServiceClient proxy = new OrderService.OrderServiceClient();
                proxy.DeleteOrder(orderId);
            }
        }
    }
}
