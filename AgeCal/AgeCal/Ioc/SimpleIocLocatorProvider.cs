using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Ioc
{
    public class SimpleIocLocatorProvider : CommonServiceLocator.IServiceLocator
    {
        protected SimpleIoc ioc;
        public SimpleIocLocatorProvider(SimpleIoc ioc)
        {
            this.ioc = ioc;
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return ioc.GetAllInstances(serviceType);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return ioc.GetAllInstances<TService>();
        }

        public object GetInstance(Type serviceType)
        {
            return ioc.GetInstance(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return ioc.GetInstance(serviceType, key);
        }

        public TService GetInstance<TService>()
        {
            return ioc.GetInstance<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return ioc.GetInstance<TService>(key);
        }

        public object GetService(Type serviceType)
        {
            return ioc.GetService(serviceType);
        }
    }
}
