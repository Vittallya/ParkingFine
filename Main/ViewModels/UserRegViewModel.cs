using DAL;
using MVVM_Core;
using MVVM_Core.Validation;
using System;
using System.Linq;
using System.Windows.Controls;
using BL;
namespace Main.ViewModels
{
    public class UserRegViewModel : BasePageViewModel
    {
        private readonly UserRegisterService registerService;
        private readonly EventBus eventBus;
        private readonly Validator validator;

        public string Phone { get; set; }
        public PasswordBox PasswordBox { get; set; } = new PasswordBox();
        public PasswordBox PasswordBoxConf { get; set; } = new PasswordBox();

        public UserRegViewModel(PageService pageservice,
                                UserRegisterService registerService,
                                EventBus eventBus,
                                Validator validator) 
            : base(pageservice)
        {
            this.registerService = registerService;
            this.eventBus = eventBus;
            this.validator = validator;
            Init();
        }

        private void Init()
        {
            validator.ForProperty(() => Phone, "Номер телефона").
                NotEmpty().
                Predicate(s => s.All(char.IsDigit), "Должны быть только цифры");

            validator.ForProperty(() => PasswordBox.Password, "Пароль").
                LengthMoreThan(1).
                Predicate(s => s.Any(char.IsDigit) && s.Any(char.IsLetter) , "Пароль должен содержать цифры и буквы");

            validator.ForProperty(() => PasswordBoxConf.Password, "Потверждение").
                Predicate(s => s == PasswordBox.Password , "Пароли должны совпадать");
        }

        public string Msg { get; set; }
        public bool MsgVis { get; set; }

        public bool IsSplashVisible { get; set; }

        protected override async void Next(object param)
        {
            MsgVis = false;

            if (validator.IsCorrect)
            {
                IsSplashVisible = true;
                registerService.SetupPass(Phone, PasswordBox.Password);

                int id = await registerService.RegisterAsync();
                IsSplashVisible = false;

                if (id > -1)
                {
                    registerService.Clear();
                    pageservice.Next();
                }
                else
                {
                    MsgVis = true;
                    Msg = registerService.ErrorMessage;
                }
                
            }
            else
            {
                MsgVis = true;
                Msg = validator.ErrorMessage;
            }
        }

        public override int PoolIndex => Rules.Pages.SecPool;
    }
}