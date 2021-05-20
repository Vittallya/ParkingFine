using Microsoft.Extensions.DependencyInjection;
using System;
using Main.ViewModels;
using DAL;
using BL;
using System.Threading.Tasks;
using Main.Pages;

namespace Main
{
    public class Locator
    {
        public static IServiceProvider Services { get; private set; }
        public static void InitServices(IServiceProvider provider)
        {
            Services = provider;
        }

        public  MainViewModel MainViewModel => Services.GetRequiredService<MainViewModel>();
        public  PhoneConfirmViewModel PhoneConfirmViewModel => Services.GetRequiredService<PhoneConfirmViewModel>();
        public  ClientResultViewModel ClientResultViewModel => Services.GetRequiredService<ClientResultViewModel>();
        public  ViewModels.SearchViewModel SearchViewModel => Services.GetRequiredService<SearchViewModel>();
        public  ViewModels.DetailInfoViewModel DetailInfoViewModel => Services.GetRequiredService<DetailInfoViewModel>();
        public  ViewModels.DeclarationViewModel DeclarationViewModel => Services.GetRequiredService<DeclarationViewModel>();
        public  ViewModels.ProfileViewModel ProfileViewModel => Services.GetRequiredService<ProfileViewModel>();
        public  ViewModels.PaymentViewModel PaymentViewModel => Services.GetRequiredService<PaymentViewModel>();
        public  ViewModels.ResultViewModel ResultViewModel => Services.GetRequiredService<ResultViewModel>();
        public  ViewModels.UserRegViewModel UserRegViewModel => Services.GetRequiredService<UserRegViewModel>();
        public  ViewModels.LoginViewModel LoginViewModel => Services.GetRequiredService<LoginViewModel>();
    }
}

