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
        private readonly UserService userService;
        private readonly EventBus eventBus;
        private readonly UpdateHandlerService handlerService;

        public MainViewModel(PageService pageService, 
            DbContextLoader contextLoader, 
            ClientPipeHanlder pipeHanlder, 
            UserService userService,
            EventBus eventBus,
            Services.UpdateHandlerService handlerService)
        {
            this.pageService = pageService;
            this.contextLoader = contextLoader;
            this.pipeHanlder = pipeHanlder;
            this.userService = userService;
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
            userService.Autorized += UserService_Autorized;
            userService.Exited += UserService_Exited;

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

        public ICommand ToLoginCommand => new Command(x =>
        {
            pageService.SetupNext<Pages.SearchAutoPage>(Rules.Pages.MainPool, defaultAnim);
            pageService.ChangePage<Pages.LoginPage>(Rules.Pages.MainPool, defaultAnim);
        });

        public ICommand LogoutCommand => new Command(x =>
        {
            userService.Logout();
        });

        public ICommand ParkingsCommand => new Command(x =>
        {
        });

        public string UserName { get; set; }

        public bool IsAutorized { get; set; }

        private void UserService_Exited()
        {
            IsAutorized = false;
            UserName = null;
        }

        private void UserService_Autorized()
        {
            IsAutorized = true;
            UserName = userService.CurrentUser.FIO;
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
