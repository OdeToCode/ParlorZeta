using System.Web.Http;
using ParlorZeta.Web.App_Start;
using ParlorZeta.Web.Infrastructure;

namespace ParlorZeta.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new StructureMapDependencyResolver(IoC.Container());
            
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
