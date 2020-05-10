using AgeCal.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AgeCal.Ioc
{
    public class IocRegistry
    {
        public static void Register<TClass>() where TClass : class
        {
            SimpleIoc.Default.Unregister<TClass>();
            SimpleIoc.Default.Register<TClass>();
        }
        public static void Register<TClass>(Func<TClass> factory) where TClass : class
        {
            SimpleIoc.Default.Unregister<TClass>();
            SimpleIoc.Default.Register(factory);
        }
        public static void Register<TInterface, TClass>() where TInterface : class where TClass : class, TInterface
        {
            SimpleIoc.Default.Unregister<TClass>();
            SimpleIoc.Default.Register<TInterface, TClass>();
        }
        public static void Register<TInterface, TClass>(string name) where TInterface : class where TClass : class, TInterface
        {
            SimpleIoc.Default.Unregister<TClass>();
            SimpleIoc.Default.Register<TInterface>(() => SimpleIoc.Default.GetInstance<TClass>(), name);
        }
        public static void Unregister<TClass>() where TClass : class
        {
            if (SimpleIoc.Default.IsRegistered<TClass>())
                SimpleIoc.Default.Unregister<TClass>();

        }
        public static void Unregister<TInterface>(TInterface instance) where TInterface : class
        {
            if (SimpleIoc.Default.IsRegistered<TInterface>())
                SimpleIoc.Default.Unregister<TInterface>(instance);

        }

        public static T Locate<T>() where T : class
        {
            try
            {
                return SimpleIoc.Default.GetInstance<T>();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public static IEnumerable<T> LocateAll<T>() where T : class
        {
            try
            {
                return SimpleIoc.Default.GetAllInstances<T>();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public static void DistroyViewModels()
        {
            var baseViewModelType = typeof(BaseViewModel);
            List<Type> viewModelsTypes = new List<Type>();
            try
            {
                viewModelsTypes = baseViewModelType.Assembly.GetExportedTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(baseViewModelType)).ToList();
            }
            catch (ReflectionTypeLoadException ex)
            {
                viewModelsTypes = ex.Types?.ToList();


            }
            if (viewModelsTypes?.Any() ?? false)
            {
                try
                {
                    foreach (var viewModel in viewModelsTypes)
                        SimpleIoc.Default.Unregister(viewModel);
                }
                catch (Exception ex)
                {

                }
            }

        }
    }
}
