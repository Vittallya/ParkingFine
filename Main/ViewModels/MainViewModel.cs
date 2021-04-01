using BL;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MVVM_Core;
using System.Threading.Tasks;
using DAL;
using System;
using Main.Services;

namespace Main.ViewModels
{
    
    public class MainViewModel : BaseSliderViewModel
    {
        private readonly PageService pageService;
        private readonly DbContextLoader contextLoader;
        private readonly ClientPipeHanlder pipeHanlder;
        private readonly EventBus eventBus;
        private readonly UpdateHandlerService handlerService;

        public MainViewModel(PageService pageService, 
            DbContextLoader contextLoader, 
            ClientPipeHanlder pipeHanlder, 
            EventBus eventBus,
            Services.UpdateHandlerService handlerService)
        {
            this.pageService = pageService;
            this.contextLoader = contextLoader;
            this.pipeHanlder = pipeHanlder;
            this.eventBus = eventBus;
            this.handlerService = handlerService;
            pageService.PageChanged += PageService_PageChanged;
            

            Init();
        }

        public string LoadingText { get; set; } = "Инициализация бд...";

        async void Init()
        {
            pipeHanlder.Init();
            pipeHanlder.UpdateCalled += PipeHanlder_UpdateCalled;
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

        private void PipeHanlder_UpdateCalled(string msg)
        {
            handlerService.Handle(msg);
        }

        public bool IsLoaded { get; set; }

        public int Width { get; set; } = 800;
        public override Page CurrentPage { get; set; }
    }
}
