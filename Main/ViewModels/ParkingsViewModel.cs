using MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using BL;
using System.Collections.ObjectModel;
using Main.Events;

namespace Main.ViewModels
{
    public class ParkingsViewModel : BasePageViewModel
    {
        private readonly ParkingsService parkingsService;
        private readonly EventBus eventBus;

        public ParkingsViewModel(PageService pageservice, ParkingsService parkingsService, EventBus eventBus) : base(pageservice)
        {
            this.parkingsService = parkingsService;
            this.eventBus = eventBus;
            Init();
        }

        public ObservableCollection<dynamic> Parkings { get; set; }

        private async void Init()
        {
            eventBus.Subscribe<Events.DataUpdated<Parking>, ParkingsViewModel>(OnUpdated , false);
            await Reload();
        }

        private async Task OnUpdated(DataUpdated<Parking> arg)
        {
            await Reload();
        }

        private async Task Reload()
        {
            await parkingsService.ReloadAsync();

            
            Parkings = new ObservableCollection<dynamic>(
                parkingsService.GetParkings().
                GroupBy(x => x.City, (a, b) =>
                {

                    return new { City = a, Parks = b.ToList() };
                }));

        }

        protected override void Back(object param)
        {
            pageservice.ChangeToLastByPool(Rules.Pages.MainPool);
        }

        public override int PoolIndex => Rules.Pages.SecPool;
    }
}
