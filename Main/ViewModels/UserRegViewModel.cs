using MVVM_Core;
using MVVM_Core.Validation;
using System;

namespace Main.ViewModels
{
    public class UserRegViewModel : BasePageViewModel
    {
        private readonly Validator validator;

        public UserRegViewModel(PageService pageservice, Validator validator) : base(pageservice)
        {
            this.validator = validator;
            Init();
        }

        private void Init()
        {
            
        }

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}