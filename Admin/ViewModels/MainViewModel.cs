﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Core;
using BL;
using DAL;
using System.Windows;
using DAL.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Admin.ViewModels
{
    [Singleton]
    public class MainViewModel: BaseSliderViewModel
    {
        private readonly DbContextLoader loader;
        private readonly PageService pageService;

        public bool LoadingContext { get; set; } = true;

        public MainViewModel(DbContextLoader loader, PageService pageService)
        {
            this.loader = loader;
            this.pageService = pageService;
            pageService.PageChanged += PageService_PageChanged;
            init();
        }

        public ICommand AutosCommand => new Command(x =>
        {
            Locator.SetDataContext<AutosViewModel>();
            pageService.ClearHistoryByPool(1);
            pageService.ChangePage<Pages.ItemsPage>(1, DisappearAndToSlideAnim.Default);
        });
        public ICommand FinesCommand => new Command(x =>
        {
            Locator.SetDataContext<FinesViewModel>();
            pageService.ClearHistoryByPool(1);
            pageService.ChangePage<Pages.ItemsPage>(1, DisappearAndToSlideAnim.Default);
        });

        public ICommand ParkingCommand => new Command(x =>
        {
            Locator.SetDataContext<ParkingViewModel>();
            pageService.ClearHistoryByPool(1);
            pageService.ChangePage<Pages.ItemsPage>(1, DisappearAndToSlideAnim.Default);
        });
        public ICommand EvacCommand => new Command(x =>
        {
            Locator.SetDataContext<EvacuationsViewModel>();
            pageService.ClearHistoryByPool(1);
            pageService.ChangePage<Pages.ItemsPage>(1, DisappearAndToSlideAnim.Default);
        });
        public ICommand DeclarationCommand => new Command(x =>
        {
            Locator.SetDataContext<DeclarationsViewModel>();
            pageService.ClearHistoryByPool(1);
            pageService.ChangePage<Pages.ItemsPage>(1, DisappearAndToSlideAnim.Default);
        });


        public ObservableCollection<Auto> Autos { get; set; }
        public override Page CurrentPage { get; set; }

        async void init()
        {
            try
            {
                await loader.LoadAsync();
                LoadingContext = false;
            }
            catch(Exception ex)
            {
                MessageBoxResult res = MessageBox.Show(ex.Message, "", MessageBoxButton.OK);

                if (res == MessageBoxResult.OK || res == MessageBoxResult.Cancel)
                {
                    
                    Application.Current.Shutdown();
                }
            }
        }
    }
}
