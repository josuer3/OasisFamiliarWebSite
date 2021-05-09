using OasisFamiliarWebSite.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OasisFamiliarWebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {




        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //}

        //protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        var identity =
        //            new IdentityPersonalizado(
        //                HttpContext.Current.User.Identity);
        //        var principal = new PrincipalPersonalizado(identity);
        //        HttpContext.Current.User = principal;
        //    }
        //}



        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
