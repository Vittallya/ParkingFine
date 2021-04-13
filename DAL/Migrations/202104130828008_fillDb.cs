namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fillDb : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [Autoes] VALUES(N'A437АЕ 147(RUS)', 'Lada', 'Vesta')");
            Sql("INSERT INTO [Autoes] VALUES(N'Н473ЗИ 116(RUS)', 'Scoda', 'Octavia')");
            Sql("INSERT INTO [Autoes] VALUES(N'А548ОП 263(РУС)', 'Nissan', 'Almera')");

            Sql("INSERT INTO [Fines] VALUES(N'Парковка в неправильном месте', 1000)");
            Sql("INSERT INTO [Fines] VALUES(N'Парковка на пешеходном переходе', 2500)");

            Sql("INSERT INTO [Parkings] VALUES(N'ул. Пушкина, д.24', 21)");
            Sql("INSERT INTO [Parkings] VALUES(N'Москва, ул. Туполева, д. 45', 25)");


            Sql($"INSERT INTO [Evacuations] VALUES(1, 2, 1, N'Ул. Королева', CAST('04/13/2021 18:03:40' AS DATETIME), 900, 0)");
            Sql($"INSERT INTO [Evacuations] VALUES(2, 1, 1, N'Ул. Писарева', CAST('03/14/2021 18:03:40' AS DATETIME), 600, 0)");
            Sql($"INSERT INTO [Evacuations] VALUES(2, 3, 2, N'Ул. Косыгина', CAST('03/15/2021 18:03:40' AS DATETIME), 1200, 0)");

        }
        
        public override void Down()
        {
        }
    }
}
