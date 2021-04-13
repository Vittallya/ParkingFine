
            INSERT INTO [Autoes] VALUES(N'A437АЕ 147(RUS)', 'Lada', 'Vesta')
            INSERT INTO [Autoes] VALUES(N'Н473ЗИ 116(RUS)', 'Scoda', 'Octavia')
            INSERT INTO [Autoes] VALUES(N'А548ОП 263(РУС)', 'Nissan', 'Almera')

            INSERT INTO [Fines] VALUES(N'Парковка в неправильном месте', 1000)
            INSERT INTO [Fines] VALUES(N'Парковка на пешеходном переходе', 2500)

            INSERT INTO [Parkings] VALUES(N'ул. Пушкина, д.24', 21)
            INSERT INTO [Parkings] VALUES(N'Москва, ул. Туполева, д. 45', 25)


            INSERT INTO [Evacuations] VALUES(1, 2, 1, N'Ул. Королева', CAST('04/13/2021 18:03:40' AS DATETIME), 900, 0)
            INSERT INTO [Evacuations] VALUES(2, 1, 1, N'Ул. Писарева', CAST('03/14/2021 18:03:40' AS DATETIME), 600, 0)
            INSERT INTO [Evacuations] VALUES(2, 3, 2, N'Ул. Косыгина', CAST('03/15/2021 18:03:40' AS DATETIME), 1200, 0)