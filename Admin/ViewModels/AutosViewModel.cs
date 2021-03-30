using MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using System.Data.Entity;
using System.Collections.ObjectModel;
using Admin.Components;

namespace Admin.ViewModels
{
    public class AutosViewModel : ItemsViewModel<Auto>
    {
        public AutosViewModel(PageService pageservice, DAL.AllDbContext context, Services.FieldsGenerator fieldsGenerator) 
            : base(pageservice, context, fieldsGenerator)
        {
        }

        public override Dictionary<string, string> BindingList { get; } = new Dictionary<string, string>
        {
            { "GovNumber", "Гос. номер"},
            { "Mark", "Марка"},
            { "Model", "Модель"},
        };

        public override BindingComponent[] BindingList1 { get; } = new BindingComponent[]
        {
            new BindingComponent("GovNumber", "Гос. номер"),
            new BindingComponent("Mark", "Марка авто"),
            new BindingComponent("Model", "Модель авто"),
        };

        public override async Task Edit(Auto item)
        {            
            var auto = dbContext.Autos.Find(item.Id);

            auto.Mark = item.Mark;
            auto.Model = item.Model;
            auto.GovNumber = item.GovNumber;

            dbContext.Entry(auto).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

    }
}
