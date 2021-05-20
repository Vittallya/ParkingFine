using BL;
using DAL.Models;
using DAL;
using MVVM_Core;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Main.Events;

namespace Main.ViewModels
{
    public class DeclarationViewModel : BasePageViewModel
    {
        private readonly UserService userService;
        private readonly EventBus eventBus;
        private readonly DeclarationService declarationService;
        private DateTime selectedDate;
        private TimeSpan _selectedTime;

        public DeclarationDto Declaration { get; set; }

        public DeclarationViewModel(PageService pageservice, 
            UserService userService,
            EventBus eventBus,
            BL.DeclarationService declarationService) : base(pageservice)
        {
            this.userService = userService;
            this.eventBus = eventBus;
            this.declarationService = declarationService;

            Declaration = declarationService.GetDeclaration();

            IsPaid = Declaration.DecStatus == DecStatus.ActivePaid && declarationService.IsEdit;
            init();
        }

        public Dictionary<PayingType, string> PayType { get; set; }

        public bool IsSplashVisible { get; set; }
        
        void init()
        {
            declarationService.SetupProfile(userService.CurrentUser.Id);
            PayType = Rules.Static.PayTypeConvert;
            StartDate = DateTime.Now.AddDays(1);
            selectedDate = Declaration.ComingDate;
            _selectedTime = selectedDate.TimeOfDay;
        }
        protected override void Next(object param)
        {
            var time = TimeSpan.Parse(TimeList[SelectedTimeIndex]);

            Declaration.ComingDate = SelectedDate.Date.AddHours(time.Hours);
            declarationService.SetupFilledDeclaration(Declaration);

            if (!IsPaid && Declaration.PayingType == PayingType.Online)
            {
                pageservice.ChangePage<Pages.PaymentPage>(PoolIndex, DisappearAndToSlideAnim.ToLeft);
            }
            else
            {
                pageservice.ChangePage<Pages.ResultPage>(PoolIndex, DisappearAndToSlideAnim.ToLeft);
            }
        }

        protected override void Back(object param)
        {
            pageservice.Back<Pages.DetailInfoPage>(PoolIndex);
        }

        public DateTime SelectedDate { 
            get => selectedDate;
            set 
            {
                if (value != selectedDate)
                {
                    selectedDate = value;
                    OnPropertyChanged();
                    UpdateTime();
                }
            }
        }

        public bool IsPaid { get; set; }

        async void UpdateTime()
        {

            IsTimeLoading = true;
            var time = _selectedTime.ToString("hh\\:mm");

            TimeList = (await declarationService.GetTimesForDate(selectedDate.Date)).ToList();
            //Загадка, время обнуляется почему-то



            if (selectedDate.Date == Declaration.ComingDate.Date && TimeList.Contains(time))
            {
                SelectedTimeIndex = TimeList.IndexOf(time);
            }
            else
            {
                SelectedTimeIndex = 0;
            }


            IsTimeLoading = false;
        }

        public string SelectedTime { get; set; }

        public DateTime StartDate { get; set; }


        public bool IsTimeLoading { get; set; }
        public List<string> TimeList { get; set; }

        public int SelectedTimeIndex { get; set; }

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}
