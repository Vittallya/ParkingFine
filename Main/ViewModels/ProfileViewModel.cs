using BL;
using DAL.Models;
using DAL;
using MVVM_Core;

namespace Main.ViewModels
{
    public class ProfileViewModel : BasePageViewModel
    {
        private readonly PageService pageservice;
        private readonly IDeclarationService declarationService;

        public ProfileDto Profle { get; set; }

        public ProfileViewModel(PageService pageservice, IDeclarationService declarationService) : base(pageservice)
        {
            this.pageservice = pageservice;
            this.declarationService = declarationService;
            Profle = declarationService.GetProfile();
        }

        public override void Next()
        {
            //Нектороые проверочки
            declarationService.SetupFilledProfile(Profle);

            if (declarationService.GetDeclaration().PayingType == PayingType.Online)
            {
                pageservice.ChangePage<Pages.PaymentPage>(Rules.Pages.MainPool, DisappearAndToSlideAnim.ToLeft);
            }
            else
            {
                pageservice.ChangePage<Pages.ResultPage>(Rules.Pages.MainPool, DisappearAndToSlideAnim.ToLeft);
            }
        }

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}