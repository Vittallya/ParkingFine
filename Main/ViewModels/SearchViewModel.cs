using MVVM_Core;
using BL;
using DAL;
using DAL.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Main.Components;
using System.Linq;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private readonly EventBus eventBus;
        private readonly IAutoListService listService;
        private readonly PageService pageService;
        private readonly DbContextLoader dbContextLoader;
        private string searchText;

        public bool IsListViewVisible { get; set; }

        public bool AnimationVisible { get; set; }

        public SearchViewModel(EventBus eventBus, 
            IAutoListService listService, 
            PageService pageService, DbContextLoader dbContextLoader)
        {
            this.eventBus = eventBus;
            this.listService = listService;
            this.pageService = pageService;
            this.dbContextLoader = dbContextLoader;
            eventBus.Subscribe<Events.DataUpdated<Evacuation>, SearchViewModel>(Updated, false);
            Init();

        }

        private async void Init()
        {
            await Reload(searchText);
        }

        async Task Updated(Events.DataUpdated<Evacuation> data)
        {
            IsLoading = true;

            await listService.Reload();
            await Task.Delay(250);
            await Reload(SearchText);
        }


        public bool IsLoading { get; set; } = true;

        public ObservableCollection<AutoItem> Autos { get; set; }

        public string SearchText { 
            get => searchText;
            set
            {
                if (value != searchText)
                {
                    IsListViewVisible = true;

                    searchText = value;
                    OnPropertyChanged();
                    Init();
                }

            }
        }

        public AutoItem SelectedItem { get; set; }

        public bool Selected => SelectedItem != null;



        async Task Reload(string text = null)
        {
            Autos = new ObservableCollection<AutoItem>(
            (await listService.GetEvacuations(text)).Select(x => new AutoItem(x)));
            IsLoading = false;
        }

        public ICommand SelectCar => new Command(x => 
        {
            listService.SetCar(SelectedItem.Evac);
            pageService.ChangePage<Pages.DetailInfoPage>(Rules.Pages.MainPool, MVVM_Core.DisappearAndToSlideAnim.ToLeft);
        }, 
        y => Selected);
    }
}