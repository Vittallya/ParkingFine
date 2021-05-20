using BL;
using MVVM_Core;
using MVVM_Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class LoginViewModel : BasePageViewModel
    {
        private readonly UserService userService;
        private readonly LoginService loginService;
        private readonly Validator validator;

        public LoginViewModel(
            PageService pageservice, 
            UserService userService, 
            LoginService loginService,
            Validator validator) : base(pageservice)
        {
            this.userService = userService;
            this.loginService = loginService;
            this.validator = validator;
            init();
        }

        private void init()
        {
            validator.ForProperty(() => Phone, "Номер телефона").NotEmpty();
            validator.ForProperty(() => PasswordBox.Password, "Пароль").NotEmpty();
        }

        public string Phone { get; set; }

        public PasswordBox PasswordBox { get; set; } = new PasswordBox();

        protected override async Task NextAsync(object param)
        {
            if (validator.IsCorrect)
            {
                var profile = await loginService.LoginAsync(Phone, PasswordBox.Password);

                if (profile != null)
                {
                    userService.SetupUser(profile);
                    pageservice.Next();
                }
                else
                {
                    MessageBox.Show("Пользователь не найден!");
                }
            }
            else
            {
                MessageBox.Show(validator.ErrorMessage);
            }

        }

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}
