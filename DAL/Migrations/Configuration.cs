namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.AllDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.AllDbContext context)
        {
            var auto1 = new Models.Auto { GovNumber = "A437АЕ 147(RUS)", Mark = "Lada", Model = "Vesta" };
            var auto2 = new Models.Auto { GovNumber = "Н473ЗИ 116(RUS)", Mark = "Scoda", Model = "Octavia" };
            var auto3 = new Models.Auto { GovNumber = "А548ОП 263(РУС)", Mark = "Nissan", Model = "Almera" };

            var fine1 = new Models.Fine { Name= "Парковка в неправильном месте", Cost = 1000 };
            var fine2 = new Models.Fine { Name= "Парковка на пешеходном переходе", Cost = 2000 };

            var park1 = new Models.Parking { Address= "ул. Пушкина, д.24", CostByHour = 21 };
            var park2 = new Models.Parking { Address= "Москва, ул. Туполева, д. 45", CostByHour = 25 };

            var evac1 = new Models.Evacuation { Fine = fine1, Auto = auto1, DateTimeEvac = new DateTime(2021, 2, 3), EvacCost = 900, Parking = park1, PlaceEvac = "Ул. Королева" };
            var evac2 = new Models.Evacuation { Fine = fine2, Auto = auto2, DateTimeEvac = new DateTime(2021, 3, 12), EvacCost = 600, Parking = park2, PlaceEvac = "Ул. Писарева" };
            var evac3 = new Models.Evacuation { Fine = fine2, Auto = auto3, DateTimeEvac = new DateTime(2021, 4, 1), EvacCost = 1200, Parking = park1, PlaceEvac = "Ул. Зеленая" };

            context.Autos.AddOrUpdate(auto1, auto2, auto3);
            context.Parkings.AddOrUpdate(park1, park2);
            context.Fines.AddOrUpdate(fine1, fine2);
            context.Evacuations.AddOrUpdate(evac1, evac2, evac3);

            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
