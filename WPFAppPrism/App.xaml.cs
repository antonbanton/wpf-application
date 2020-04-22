using DataAccess.EventPublishers;
using DataAccess.Mocks;
using DataAccess.Repositories;
using Logic.Services;
using Prism.Ioc;
using Prism.Mvvm;
using System.Windows;
using View.Modules.Prism;
using ViewModel.Modules.Prism;

namespace WPFAppPrism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<MainView, IMainViewModel>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMainViewModel, MainViewModel>();
            containerRegistry.RegisterSingleton<IService, Service>();
            containerRegistry.RegisterSingleton<IRepository, Repository>();
            containerRegistry.RegisterSingleton<IDatabaseConnection, DatabaseConnection>();
            containerRegistry.RegisterSingleton<IEventPublisher, PrismEventPublisher>();
        }
    }
}
