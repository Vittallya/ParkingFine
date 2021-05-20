using System.Threading.Tasks;
using BL;
using System.Windows.Input;
using MVVM_Core;

namespace Main.ViewModels
{
    public class PhoneConfirmViewModel : BasePageViewModel
    {
        private readonly IPhoneConfirmService loginService;
        private readonly PageService pageService;
        private readonly IDeclarationService declarationService;
        private string code;

        public string Phone { get; set; }
        public PhoneConfirmViewModel(IPhoneConfirmService loginService, PageService pageService, IDeclarationService declarationService) : base(pageService)
        {
            this.loginService = loginService;
            this.pageService = pageService;
            this.declarationService = declarationService;
            SendCodeFull();
            DefinePhoneNumber();
        }
        public bool IsErrorVisible { get; set; }
        public string ErrorMessage { get; set; }

        public bool IsAnimationVisible { get; set; }

        void DefinePhoneNumber()
        {
            var phone = declarationService.GetProfile().PhoneNumber;

            Phone = $"{phone[0]}{new string('*', phone.Length - 6)}-{phone.Substring(phone.Length - 5, 4)}";
        }


        public string Code { 
            get => code;
            set 
            { 
                code = value;
                OnPropertyChanged();
                Check();
            } 
        }

        public string PhoneNumber { get; set; }

        bool _nextPossible;

        void Check()
        {
            _nextPossible = code != null && code.Length > 2;
        }

        public bool IsCodeFiledVisible { get; set; }

        public ICommand GetCodeCommand => new CommandAsync(async x =>
        {
            SendCodeFull();

            //IsErrorVisible = false;

            //if (await loginService.SetupPhoneNumber(Phone))
            //{
            //    IsCodeFiledVisible = true;
            //}
            //else
            //{
            //    IsErrorVisible = true;
            //    ErrorMessage = loginService.ErrorMessage;
            //}
        });

        async void SendCodeFull()
        {
            SendCode();

            IsBtnEnabled = false;

            int i = loginService.Time;
            BtnText = $"Отправить код повторно ({i} сек.)";

            while (i > 0)
            {
                await Task.Delay(1000);
                i--;
                BtnText = $"Отправить код повторно ({i} сек.)";
            }
            IsBtnEnabled = true;
            BtnText = $"Отправить код повторно";
        }

        async void SendCode()
        {
            await loginService.SendCodeAsync();
        }

        public bool IsBtnEnabled { get; set; } = true;
        public string BtnText { get; set; }

        public ICommand LoginCommand => new Command(x =>
        {

            IsErrorVisible = false;
            if (loginService.CheckCode(Code))
            {
                pageService.Next();
            }
            else
            {
                IsErrorVisible = true;
                ErrorMessage = loginService.ErrorMessage;
            }


        }, y => _nextPossible);

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}
