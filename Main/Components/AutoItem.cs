using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Components
{
    public class AutoItem
    {
        public AutoItem(Evacuation auto)
        {
            Auto = auto.Auto;
            Evac = auto;
        }



        public int EvacCount => EvacExist ?
            Auto.Evacuations.Count : 0;

        public bool EvacExist => Auto.Evacuations != null && Auto.Evacuations.Count > 0;

        public string StatusText
        {
            get {

                if(!EvacExist)
                {
                    return "Нет данных по эвакуациям данного ТС";
                }

                var last = Auto.Evacuations.OrderBy(x => x.DateTimeEvac).LastOrDefault();


                if (last.CarStatus == CarStatus.AtParking)
                    return "На стоянке";

                return last.CarStatus == CarStatus.WithOwner ? "Выдана владельцу" : "Выдана дов. лицу";
            }
        }

        public Auto Auto { get; }
        public Evacuation Evac { get; }
    }
}
