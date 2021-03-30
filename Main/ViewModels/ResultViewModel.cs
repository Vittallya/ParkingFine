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

            

            Message = res ? (edit ? "Заявление успешно редактировано!" : "Заявление успешно оформлено!") : 
                "Произошла ошибка";

            pageservice.ClearHistoryByPool(PoolIndex);
            declarationService.Clear();
        }

        public override void Next()
        {
            pageservice.ChangePage<Pages.SearchAutoPage>(DisappearAndToSlideAnim.ToRight);
        }

        

        public override int PoolIndex => Rules.Pages.MainPool;
    }
}