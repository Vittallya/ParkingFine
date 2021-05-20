using MVVM_Core;

namespace Main.ViewModels
{
    public class UserDeclarationsVM: BaseViewModel
    {
        private readonly PageService pageService;

        public UserDeclarationsVM(
            PageService pageService)
        {
            this.pageService = pageService;
            
        }


    }
}
