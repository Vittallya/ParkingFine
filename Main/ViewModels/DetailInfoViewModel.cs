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
        private readonly IAutoListService listService;
        private readonly UserService userService;
        private readonly DeclarationService declarationService;

        public bool HasActiveOwn { get; set; }
        public bool HasActiveOther { get; set; }
        public bool HasActive { get; set; }

        public bool NoDeclarations => !HasActiveOther && !HasActiveOwn;
        public bool NeedAutorize { get; set; }

        public DetailInfoViewModel(PageService pageservice,
                                   BL.IAutoListService listService,
                                   UserService userService,
                                   DeclarationService declarationService) : base(pageservice)
        {
            this.listService = listService;
            this.userService = userService;
            this.declarationService = declarationService;
            Evac = listService.SelectedEvac;

            init();
        }

        public bool IsOther { get; set; }

        public bool IsBorderVisible => NeedAutorize || HasActiveOther;

        private async void init()
        {
            Hours = (int)Math.Round((DateTime.Now - Evac.DateTimeEvac).TotalHours);
            Points = new string('.', 45);
            ParkingCost = Evac.Parking.CostByHour * Hours;

            HasActive = await declarationService.SetupEvac(Evac.Id);
            FullCost = declarationService.GetCost();

            if (HasActive)
            {
                NeedAutorize = !userService.IsAutorized;
                if (userService.IsAutorized)
                {
                    HasActiveOther = userService.CurrentUser.Id != declarationService.ProfileId;
                    HasActiveOwn = !HasActiveOther;

                }

            }


        }

        protected override void Next(object param)
        {
            if (userService.IsAutorized)
            {
                pageservice.ChangePage<Pages.DeclarationPage>(PoolIndex, DisappearAndToSlideAnim.ToLeft);
            }
            else
            {
                pageservice.SetupNext<Pages.DeclarationPage>(PoolIndex, DisappearAndToSlideAnim.ToLeft);
                pageservice.ChangePage<Pages.ProfilePage>(PoolIndex, DisappearAndToSlideAnim.ToLeft);
            }

            //if (HasActive)
            //{
            //    pageservice.ChangePage<Pages.LoginPage>(PoolIndex, DisappearAndToSlideAnim.ToLeft);
            //}
            //else
            //{
            //    pageservice.ChangePage<Pages.DeclarationPage>(PoolIndex, DisappearAndToSlideAnim.ToLeft);
            //}
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