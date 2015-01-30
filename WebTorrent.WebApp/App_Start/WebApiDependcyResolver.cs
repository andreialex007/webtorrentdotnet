using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using StructureMap;

namespace WebTorrent.WebApp
{
    public class WebApiDependcyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            try
            {
                return ObjectFactory.GetInstance(serviceType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                var objects = ObjectFactory.GetAllInstances(serviceType) as IEnumerable<object>;
                return objects;
            }
            catch (Exception exception)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            return new WebApiDependcyResolver();
        }

        public void Dispose()
        {
           
        }
    }
}