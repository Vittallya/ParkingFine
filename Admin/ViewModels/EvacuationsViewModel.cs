using Admin.Components;
using Admin.Services;
using DAL;
using DAL.Models;
using MVVM_Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Admin.ViewModels
{
    public class EvacuationsViewModel : ItemsViewModel<Evacuation>
    {
        public EvacuationsViewModel(PageService pageservice, AllDbContext dbContext, FieldsGenerator fieldsGenerator) : base(pageservice, dbContext, fieldsGenerator)
        {
            
        }

        public override Dictionary<string, string> BindingList { get; } = new Dictionary<string, string>
        {
            {"Auto.Name", "Автомобиль" },
            {"DateTimeEvac", "Дата эвакуации" },
            {"PlaceEvac", "Место эвакуации" },
        };

        public override BindingComponent[] BindingList1 { get; } = new BindingComponent[]
        {
            new BindingComponent("DateTimeEvac", "Дата эвакуации", PropertyType.Simple),
            new BindingComponent("PlaceEvac", "Место эвакуации (откуда)", PropertyType.Simple),
            new BindingComponent("Auto.Name", "Автомобиль", PropertyType.OuterPropertyClassNonSerializable),
            new BindingComponent("AutoId", "Mark", "Автомобиль"),
            new BindingComponent("FineId", "Name", "Нарушение"),
            new BindingComponent("ParkingId", "Address", "Парковка"),
        };

        public override async Task<bool> CheckBeforeAdd(Evacuation item)
        {
            var same = await dbContext.Evacuations.FirstOrDefaultAsync(
                x => x.AutoId == item.AutoId && x.CarStatus == CarStatus.AtParking);

            bool res = same == null;

            if (!res)
            {
                MessageBox.Show($"Данное авто уже есть на стоянке по адр. '{same.Parking?.Address}'");
            }

            return res;
        }

        public override async Task Edit(Evacuation item)
        {
            
        }
    }
}