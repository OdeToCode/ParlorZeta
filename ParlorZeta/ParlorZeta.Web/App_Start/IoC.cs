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
                    //init.Scan(scan =>
                    //{
                    //    scan.TheCallingAssembly();
                    //    scan.WithDefaultConventions();
                    //});                    
                });

                _container = ObjectFactory.Container;

            }
            return _container;
        }

        private static IContainer _container;
    }
}