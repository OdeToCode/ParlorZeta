using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace ParlorZeta.Web.Infrastructure
{
    public class StructureMapDependencyResolver : 
            System.Web.Http.Dependencies.IDependencyResolver, 
            System.Web.Mvc.IDependencyResolver
    { 
        private readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            var result = _container.TryGetInstance(serviceType);
            return result;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            var nested = _container.GetNestedContainer();
            return new StructureMapDependencyResolver(nested);
        }
    }
}