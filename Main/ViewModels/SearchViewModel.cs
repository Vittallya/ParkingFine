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
        private string searchText;

        public bool IsListViewVisible { get; set; }

        public bool AnimationVisible { get; set; }

        public SearchViewModel(EventBus eventBus, IAutoListService listService, PageService pageService)
        {
            this.eventBus = eventBus;
            this.listService = listService;
            this.pageService = pageService;
            Reload();

        }
        


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
                    Reload(value);
                }

            }
        }

        public AutoItem SelectedItem { get; set; }

        public bool Selected => SelectedItem != null;

        async void Reload(string text = null)
        {
            Autos = new ObservableCollection<AutoItem>(
            (await listService.GetEvacuations(text)).Select(x => new AutoItem(x)));
        }

        public ICommand SelectCar => new Command(x => 
        {
            listService.SetCar(SelectedItem.Evac);
            pageService.ChangePage<Pages.DetailInfoPage>(Rules.Pages.MainPool, MVVM_Core.DisappearAndToSlideAnim.ToLeft);
        }, 
        y => Selected);
    }
}