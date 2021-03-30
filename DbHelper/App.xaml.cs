using MVVM_Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Admin
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public ViewModels.MainViewModel MainViewModel => ServiceProvider.GetRequiredService<ViewModels.MainViewModel>();

        protected override void OnStartup(StartupEventArgs e)
        {
            var buiilder = new ServiceProviderBuilder();
            ServiceProvider = buiilder.UseStartup<Startup>().BuidSeriveProvider();
            base.OnStartup(e);
        }
    }
}
