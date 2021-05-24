using BL;
using DAL.Models;
using DAL;
using MVVM_Core;
using MVVM_Core.Validation;
using System;

namespace Main.ViewModels
{
    public class ProfileViewModel : BasePageViewModel
    {        
        private readonly Validator validator;
        private readonly UserService userService;
        private readonly UserRegisterService registerService;
        private readonly DeclarationService declarationService;

        public ProfileDto Profle { get; set; }

        public ProfileViewModel(PageService pageservice, 
            Validator validator,
            UserService userService,
            UserRegisterService registerService,
            DeclarationService declarationService) : base(pageservice)
        {           
            this.validator = validator;
            this.userService = userService;
            this.registerService = registerService;
            this.declarationService = declarationService;

            Init();
        }

        private void Init()
        {
            if (userService.IsAutorized)
            {
                Profle = userService.CurrentUser;
            }
            else if (registerService.IsBeginned)
            {
                Profle = registerService.GetProfile();
            }
            else
                Profle = new ProfileDto();

            validator.ForProperty(() => Profle.FIO, "ФИО").NotEmpty();
            validator.ForProperty(() => Profle.Osago, "Номер ОСАГО").NotEmpty();
            validator.ForProperty(() => Profle.DriverLicence, "Номер вод. прав").NotEmpty();
            validator.ForProperty(() => Profle.PasportNumber, "Номер паспорта").NotEmpty();
            validator.ForProperty(() => Profle.PasportSerial, "Серия паспорта").NotEmpty();
        }

        public string Msg { get; set; }
        public bool MsgVis { get; set; }

        protected override void Next(object param)
        {
            MsgVis = false;

            if (!validator.IsCorrect)
            {
                Msg = validator.ErrorMessage;
                MsgVis = true;
                return;
            }
            registerService.SetupProfileData(Profle);
            pageservice.ChangePage<Pages.UserRegPage>(Rules.Pages.SecPool, DisappearAndToSlideAnim.ToLeft);

            //if (declarationService.GetDeclaration().PayingType == PayingType.Online)
            //{
            //    pageservice.ChangePage<Pages.PaymentPage>(Rules.Pages.MainPool, DisappearAndToSlideAnim.ToLeft);
            //}
            //else
            //{
            //    pageservice.ChangePage<Pages.ResultPage>(Rules.Pages.MainPool, DisappearAndToSlideAnim.ToLeft);
            //}
        }

        protected override void Back(object param)
        {
            registerService.Clear();
            pageservice.ChangeToLastByPool(Rules.Pages.MainPool);
        }

        public override int PoolIndex => Rules.Pages.SecPool;
    }
}