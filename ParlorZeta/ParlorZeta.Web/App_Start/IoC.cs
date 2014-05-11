using System.Net;
using System.Web;
using ParlorZeta.Azure.Contracts;
using ParlorZeta.Web.Infrastructure;
using StructureMap;

namespace ParlorZeta.Web.App_Start
{
    public static class IoC
    {
        public static IContainer Container()
        {
            if (_container == null)
            {
                ObjectFactory.Initialize(init =>
                {                   
                    init.For<IFileSystem>().Use<AspNetFileSystem>(() => new AspNetFileSystem(HttpContext.Current));
                    init.For<IUserSettings>().Use<CookieStore>(() => new CookieStore(HttpContext.Current));
                });
                
                _container = ObjectFactory.Container;

            }
            return _container;
        }

        private static IContainer _container;
    }
}