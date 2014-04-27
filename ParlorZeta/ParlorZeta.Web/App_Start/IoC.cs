using System.Web;
using System.Web.Mvc;
using ParlorZeta.Azure.FileSystem;
using ParlorZeta.Web.Controllers;
using ParlorZeta.Web.Infrastructure;
using StructureMap;
using StructureMap.Graph;

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
                    init.Scan(scan =>
                    {
                    });

                    init.For<IFileSystem>().Use<AspNetFileSystem>(() => new AspNetFileSystem(HttpContext.Current));
                });
                
                _container = ObjectFactory.Container;

            }
            return _container;
        }

        private static IContainer _container;
    }
}