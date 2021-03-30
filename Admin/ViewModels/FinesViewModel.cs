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
        public FinesViewModel(PageService pageservice, AllDbContext dbContext, FieldsGenerator fieldsGenerator) : 
            base(pageservice, dbContext, fieldsGenerator)
        {
        }

        public override Dictionary<string, string> BindingList { get; } = new Dictionary<string, string>
        {
            {"Name", "Название" },
            {"Cost", "Стоимость" },
        };
        public override BindingComponent[] BindingList1 { get; } = new BindingComponent[]
       {
            new BindingComponent("Name", "Дата Название"),
            new BindingComponent("Cost", "Стоимость"),
       };

        public override Task Edit(Fine item)
        {
            throw new System.NotImplementedException();
        }

    }
}