USE [ParkingFine]
GO



INSERT INTO [Autoes]([Id], [GovNumber], [Mark], [Model]) VALUES(1, N'A437АЕ 147(RUS)', 'Lada', 'Vesta');
INSERT INTO [Autoes]([Id], [GovNumber], [Mark], [Model]) VALUES(2, N'Н473ЗИ 116(RUS)', 'Scoda', 'Octavia');
INSERT INTO [Autoes]([Id], [GovNumber], [Mark], [Model]) VALUES(3, N'548ОП 263(РУС)', 'Nissan', 'Almera');


INSERT INTO [Fines]([Id], [Name], [Cost]) VALUES(1, N'Парковка в неправильном месте', 1000);
INSERT INTO [Fines]([Id], [Name], [Cost]) VALUES(2, N'Парковка на пешеходном переходе', 2500);


INSERT INTO [Parkings]([Id], [Address], [CostByHour]) VALUES(1, N'ул. Пушкина, д.24', 21);
INSERT INTO [Parkings]([Id], [Address], [CostByHour]) VALUES(2, N'Москва, ул. Туполева, д. 45', 25);



INSERT INTO [Evacuations] VALUES(1, 2, 1, N'Ул. Королева', CAST('04/13/2021 18:03:40' AS DATETIME), 900, 0);
INSERT INTO [Evacuations] VALUES(2, 1, 1, N'Ул. Писарева', CAST('03/14/2021 18:03:40' AS DATETIME), 600, 0);
INSERT INTO [Evacuations] VALUES(2, 3, 2, N'Ул. Косыгина', CAST('03/15/2021 18:03:40' AS DATETIME), 1200, 0);