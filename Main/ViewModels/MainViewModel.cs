using BL;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MVVM_Core;
using System.Threading.Tasks;
using DAL;
using System;

namespace Main.ViewModels
{
    
    public class MainViewModel : BaseSliderViewModel
    {
        private readonly PageService pageService;
        private readonly DbContextLoader contextLoader;

        public MainViewModel(PageService pageService, DbContextLoader contextLoader)
        {
            this.pageService = pageService;
            this.contextLoader = contextLoader;
            pageService.PageChanged += PageService_PageChanged;

            Init();
        }

        public string LoadingText { get; set; } = "Инициализация бд...";

        async void Init()
        {
            try
            {
                await contextLoader.LoadAsync();
                IsLoaded = true;
                pageService.ChangePage<Pages.SearchAutoPage>(Rules.Pages.MainPool, new DisappearAnimation() { TimeOpacityNewSec = 0.6f });
            }
            catch(Exception e)
            {
                LoadingText = e.Message;
            }
            
        }
        public bool IsLoaded { get; set; }

        public int Width { get; set; } = 800;
        public override Page CurrentPage { get; set; }
    }
}
