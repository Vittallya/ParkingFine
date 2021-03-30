using DAL;
using BL;
using MVVM_Core;
using System.Linq;

namespace Main.ViewModels
{
    public class PaymentViewModel : BasePageViewModel
    {
        private readonly PageService pageservice;
        private readonly IDeclarationService declarationService;

        public PaymentViewModel(PageService pageservice, IDeclarationService declarationService) : base(pageservice)
        {
            this.pageservice = pageservice;
            this.declarationService = declarationService;
            Cost = declarationService.GetCost();

        }

        public string CardNum { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        public string Secret { get; set; }


        public string ErrorMessage { get; set; }
        public bool ErrorShow { get; set; }

        public double Cost { get; set; }

        public override void Next()
        {
            ErrorShow = false;

            if (CardNum == null || CardNum.Length != 16 || !CardNum.All(char.IsDigit))
            {
                ShowError("Номер карты должен содержать 16 цифр");    
                return;
            }

            if (!int.TryParse(Month, out int m) || m < 1 || m > 12)
            {
                ShowError("Месяц указан некорректно");
                return;
            }

            if (!int.TryParse(Year, out int y) || y < 0 )
            {
                ShowError("Год указан некорректно");
                return;
            }

            if (Secret == null || Secret.Length != 3 || !Secret.All(char.IsDigit) )
            {
                ShowError("Секретный код должен содержать 3 цифры");
                return;
            }
            declarationService.DefineStatus(DAL.Models.DecStatus.ActivePaid);
            pageservice.ChangePage<Pages.ResultPage>(PoolIndex, DisappearAndToSlideAnim.ToLeft);
        }

        void ShowError(string mes)
        {
            ErrorMessage = mes;
            ErrorShow = true;
        }

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}