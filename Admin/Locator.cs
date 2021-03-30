using System;
using Admin.ViewModels;
using Admin.ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Core;

namespace Admin
{
    public class Locator
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public ViewModels.MainViewModel MainViewModel => ServiceProvider.GetRequiredService<MainViewModel>();
        public ViewModels.AutosViewModel AutosViewModel => ServiceProvider.GetRequiredService<AutosViewModel>();
        public ViewModels.EvacuationsViewModel EvacuationsViewModel => ServiceProvider.GetRequiredService<EvacuationsViewModel>();
        public ViewModels.ParkingViewModel ParkingViewModel => ServiceProvider.GetRequiredService<ParkingViewModel>();
        public ViewModels.FinesViewModel FinesViewModel => ServiceProvider.GetRequiredService<FinesViewModel>();
        public ViewModels.DeclarationsViewModel DeclarationsViewModel => ServiceProvider.GetRequiredService<DeclarationsViewModel>();
        public ViewModels.Interfaces.IEditItem EditItemViewModel => _typeGetterDetailItem?.Invoke();

        public IItemsViewModel ItemsViewModel => _typeGetterItems?.Invoke();

        private static Func<IItemsViewModel> _typeGetterItems;
        private static Func<IEditItem> _typeGetterDetailItem;



        public static void SetDataContext<TViewModel>() 
        {
            _typeGetterItems = () => ServiceProvider.GetRequiredService<TViewModel>() as IItemsViewModel;            
        }

        public static void SetDetailsViewModel<TModel>() where TModel: class
        {
            _typeGetterDetailItem = () => ServiceProvider.GetService<EditItemViewModel<TModel>>();
            

        }

        public static void SetupLocator(IServiceProvider provider)
        {
            ServiceProvider = provider;
        }
    }
}
