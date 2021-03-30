using Admin.Components;
using Admin.Services;
using DAL;
using DAL.Models;
using MVVM_Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.ViewModels
{
    public class ParkingViewModel : ItemsViewModel<DAL.Models.Evacuation>
    {
        public ParkingViewModel(PageService pageservice, AllDbContext dbContext, FieldsGenerator fieldsGenerator) : base(pageservice, dbContext, fieldsGenerator)
        {
        }

        public override Dictionary<string, string> BindingList { get; } = new Dictionary<string, string>
        {
            {"", "" },
        };

        public override BindingComponent[] BindingList1 { get; } = new BindingComponent[]
        {
            new BindingComponent("GovNumber", "Гос. номер"),
            new BindingComponent("Mark", "Марка авто"),
            new BindingComponent("Model", "Модель авто"),
        };

        public override Task Edit(Evacuation item)
        {
            throw new System.NotImplementedException();
        }
    }
}