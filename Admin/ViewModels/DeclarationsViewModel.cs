using Admin.Components;
using Admin.Events;
using Admin.Services;
using DAL;
using MVVM_Core;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Admin.ViewModels
{
    public class DeclarationsViewModel : ItemsViewModel<DAL.Models.Declaration>
    {
        public DeclarationsViewModel(
            PageService pageservice, 
            AllDbContext dbContext, 
            FieldsGenerator fieldsGenerator, 
            CloneItemsSerivce cloneItems, 
            EventBus eventBus) : base(pageservice, dbContext, fieldsGenerator, cloneItems, eventBus)
        {
        }

        protected override void GenerateBindingVIew()
        {
            
        }

        public ICommand Accept => new CommandAsync(async x =>
        {
            SelectedItem.DecStatus = DAL.Models.DecStatus.Completed;
            SelectedItem.Evacuation.CarStatus = CarStatus.WithOwner;

            await Edit(SelectedItem);
        }, x => SelectedItem != null);

        protected override async Task OnLoadItems()
        {
            await dbContext.Declarations.Include(x => x.Evacuation).LoadAsync();
        }

        public override BindingComponent[] BindingList => throw new System.NotImplementedException();
    }
}