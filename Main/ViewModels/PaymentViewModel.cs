using DAL;
using BL;
using MVVM_Core;
using System.Linq;
using MVVM_Core.Validation;
using System;

namespace Main.ViewModels
{
    public class PaymentViewModel : BasePageViewModel
    {
        private readonly Validator validator;
        private readonly DeclarationService declarationService;

        public PaymentViewModel(PageService pageservice,
                                Validator validator,
                                DeclarationService declarationService) : base(pageservice)
        {
            this.validator = validator;
            this.declarationService = declarationService;
            Cost = declarationService.GetCost();
            Init();
        }

        private void Init()
        {
            validator.ForProperty(() => CardNum, "Номер карты").
                LengthEquals(16).
                Predicate(s => s.All(char.IsDigit), "Номер карты должен содержать только цифры");
            validator.ForProperty(() => Month, "Месяц").MoreEqualThan(1).LessEqualThan(12);
            validator.ForProperty(() => Year, "Год").MoreEqualThan(DateTime.Now.Year % 2000);
            validator.ForProperty(() => Secret, "Секретный код").LengthEquals(3).
                Predicate(s => s.All(char.IsDigit), "Секретный код должен содержать только цифры");
        }

        public string CardNum { get; set; } = "0000000000000000";

        public int Month { get; set; } = DateTime.Now.Month;

        public int Year { get; set; } = DateTime.Now.Year % 2000;

        public string Secret { get; set; }


        public string ErrorMessage { get; set; }
        public bool ErrorShow { get; set; }

        public double Cost { get; set; }

        protected override void Next(object param)
        {
            ErrorShow = false;

            if (!validator.IsCorrect)
            {
                ShowError(validator.ErrorMessage);
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