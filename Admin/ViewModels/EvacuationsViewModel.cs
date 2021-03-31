using Admin.Components;
using Admin.Events;
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
        private readonly EventBus eventBus;

        public EvacuationsViewModel(PageService pageservice, AllDbContext dbContext, 
            FieldsGenerator fieldsGenerator, CloneItemsSerivce cloneItems, EventBus eventBus) : base(pageservice, dbContext, fieldsGenerator, cloneItems)
        {
            this.eventBus = eventBus;
        }


        public override BindingComponent[] BindingList { get; } = new BindingComponent[]
        {
            new BindingComponent("DateTimeEvac", "Дата эвакуации", PropertyType.Simple),
            new BindingComponent("PlaceEvac", "Место эвакуации (откуда)", PropertyType.Simple),
            new BindingComponent("Auto.GovNumber", "Автомобиль", PropertyType.OuterPropertyClassNonSerializable),
            new BindingComponent("AutoId", "GovNumber", "Автомобиль"),
            new BindingComponent("FineId", "Name", "Нарушение"),
            new BindingComponent("ParkingId", "Address", "Парковка"),
            new BindingComponent("CarStatus", "Статус авто"),
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

        protected async override Task OnAdd()
        {
            await eventBus.Publish(new UpdatePipe());
        }

        protected async override Task OnEdit()
        {
            await eventBus.Publish(new UpdatePipe());
        }
    }
}