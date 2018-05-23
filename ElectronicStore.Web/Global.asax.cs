using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ElectronicStore.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["VisitedCount"] = 0;
            Application["OnlineCount"] = 0;

            // check exits file Count_Visited.txt
            if (!File.Exists(Server.MapPath("/Assets/Admin/staticstic/Count_Visited.txt")))
                    File.WriteAllText(Server.MapPath("/Assets/Admin/staticstic/Count_Visited.txt"), "0");
                Application["VisitedCount"] = int.Parse(File.ReadAllText(Server.MapPath("/Assets/Admin/staticstic/Count_Visited.txt")));
            }

        protected void Session_Start()
        {
            Application.Lock();
            // Increase Online Count when user online
            if (Application["OnlineCount"] == null || (int)Application["OnlineCount"] == 0)
                Application["OnlineCount"] = 1;
            else
                Application["OnlineCount"] = (int)Application["OnlineCount"] + 1;

            // Increase Visited Count when user online
            Application["VisitedCount"] = (int)Application["VisitedCount"] + 1;
            File.WriteAllText(Server.MapPath("/Assets/Admin/staticstic/Count_Visited.txt"), Application["VisitedCount"].ToString());

            Application.UnLock();
        }

        protected void Session_End()
        {
            Application.Lock();
            Application["OnlineCount"] = (int)Application["OnlineCount"] - 1;
            Application.UnLock();
        }
    }
}
