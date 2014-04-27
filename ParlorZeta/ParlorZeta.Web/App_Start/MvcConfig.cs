using System.Web.Mvc;
using System.Web.Routing;
using ParlorZeta.Web.App_Start;
using ParlorZeta.Web.Infrastructure;

namespace ParlorZeta.Web
{
    public class MvcConfig
    {
        public static void Configure(RouteCollection routes)
        {
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(IoC.Container()));

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
