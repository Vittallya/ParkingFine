using DAL;
using BL;
using MVVM_Core;

namespace Main.ViewModels
{
    public class ResultViewModel : BasePageViewModel

    {
        private readonly PageService pageservice;
        private readonly IDeclarationService declarationService;

        public string Message { get; set; }

        public ResultViewModel(PageService pageservice, IDeclarationService declarationService) : base(pageservice)
        {
            this.pageservice = pageservice;
            this.declarationService = declarationService;
            init();
        }

        async void init()
        {
            bool edit = declarationService.IsEdit;
            bool res = await declarationService.ApplyDeclaration();

            if (declarationService.IsPadied)
            {
                Message = "Оплата прошла успешно!";
            }
            else if (edit)
            {
                Message = "Заявление успешно редактировано!";
            }
            else
            {
                Message = "Заявление успешно оформлено!";
            }

            if(!res)
            {
                Message = "Произошла ошибка!";
            }
                

            pageservice.ClearHistoryByPool(PoolIndex);
            declarationService.Clear();
        }

        public override void Next()
        {
            pageservice.ChangePage<Pages.SearchAutoPage>(PoolIndex, DisappearAndToSlideAnim.ToRight);
        }

        

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}