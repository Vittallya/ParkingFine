using BL;
using DAL.Models;
using MVVM_Core;
using System;
using System.Linq;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class DetailInfoViewModel : BasePageViewModel
    {
        private readonly PageService pageservice;
        private readonly IAutoListService listService;
        private readonly IDeclarationService declarationService;

        public bool HasActive { get; set; }

        public DetailInfoViewModel(PageService pageservice, BL.IAutoListService listService, IDeclarationService declarationService) : base(pageservice)
        {
            this.pageservice = pageservice;
            this.listService = listService;
            this.declarationService = declarationService;
            Evac = listService.SelectedEvac;

            init();
        }

        private async void init()
        {
            Hours = (int)Math.Round((DateTime.Now - Evac.DateTimeEvac).TotalHours);
            Points = new string('.', 45);
            ParkingCost = Evac.Parking.CostByHour * Hours;

            HasActive = await declarationService.SetupEvac(Evac.Id);
            FullCost = declarationService.GetCost();                
        }

        public override void Next()
        {
            if (HasActive)
            {                
                pageservice.ChangePage<Pages.LoginPage>(PoolIndex, DisappearAndToSlideAnim.ToLeft);
            }
            else
            {
                pageservice.ChangePage<Pages.DeclarationPage>(PoolIndex, DisappearAndToSlideAnim.ToLeft);
            }
        }

        public string Points { get; set; }

        public override int PoolIndex => Rules.Pages.MainPool;

        public Evacuation Evac { get; }

        public Evacuation Evacuation { get; }
        public string EvacPlace { get; }

        public double ParkingCost { get; set; }
        public int FullCost { get; set; }
        public int Hours { get; set; }
    }
}