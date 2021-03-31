using Admin.Components;
using Admin.Services;
using DAL;
using DAL.Models;
using MVVM_Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.ViewModels
{
    public class FinesViewModel : ItemsViewModel<Fine>
    {
        public FinesViewModel(PageService pageservice, AllDbContext dbContext, FieldsGenerator fieldsGenerator, CloneItemsSerivce cloneItems) : 
            base(pageservice, dbContext, fieldsGenerator, cloneItems)
        {
        }


        public override BindingComponent[] BindingList { get; } = new BindingComponent[]
       {
            new BindingComponent("Name", "Название"),
            new BindingComponent("Cost", "Стоимость"),
       };

    }
}