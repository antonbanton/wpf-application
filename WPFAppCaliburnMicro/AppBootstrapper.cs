using Caliburn.Micro;
using DataAccess.EventPublishers;
using DataAccess.Mocks;
using DataAccess.Repositories;
using Logic.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using View.Modules.CaliburnMicro;
using ViewModel.Modules.CaliburnMicro;

namespace WPFAppCaliburnMicro
{
    public static class NameSpaceBinding
    {
        public const string View = "View.Modules.CaliburnMicro";
        public const string ViewModel = "ViewModel.Modules.CaliburnMicro";
    }

    public class AppBootstrapper : BootstrapperBase
    {
        private IKernel kernel;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            ConfigureTypeMappings();
            kernel = new StandardKernel();

            // Caliburn Micro
            kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            kernel.Bind<IMainViewModel>().To<MainViewModel>();
            kernel.Bind<IService>().To<Service>().InSingletonScope();
            kernel.Bind<IRepository>().To<Repository>().InSingletonScope();
            kernel.Bind<IDatabaseConnection>().To<DatabaseConnection>();
            kernel.Bind<IEventPublisher>().To<CaliburnMicroEventPublisher>();
        }

        protected void ConfigureTypeMappings()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = NameSpaceBinding.View,
                DefaultSubNamespaceForViewModels = NameSpaceBinding.ViewModel,
            };

            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var viewAssembly = typeof(MainView).Assembly;

            if (!AssemblySource.Instance.Contains(viewAssembly))
                yield return viewAssembly;
        }

        protected override object GetInstance(Type service, string key)
        {
            if (service == null)
            {
                var typeName = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.Name.Contains(key))
                    .Select(x => x.AssemblyQualifiedName).Single();

                service = Type.GetType(typeName);
            }

            return kernel.Get(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return kernel.GetAll(service);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            kernel.Dispose();
            base.OnExit(sender, e);
        }

        protected override void BuildUp(object instance)
        {
            kernel.Inject(instance);
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
           await DisplayRootViewForAsync<IMainViewModel>();
        }
    }
}
