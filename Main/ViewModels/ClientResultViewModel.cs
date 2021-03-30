using MVVM_Core;

namespace Main.ViewModels
{
    public class ClientResultViewModel: BaseViewModel
    {
        private readonly PageService pageService;

        public ClientResultViewModel(
            PageService pageService)
        {
            this.pageService = pageService;
            
        }


    }
}
