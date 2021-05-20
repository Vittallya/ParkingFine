using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Rules
{
    public static class Static
    {
        public static readonly Dictionary<DAL.Models.PayingType, string> PayTypeConvert = new Dictionary<DAL.Models.PayingType, string>
        {
            { DAL.Models.PayingType.Online, "Онлайн"},
            { DAL.Models.PayingType.UpponArrival, "По прибытию"},
            { DAL.Models.PayingType.Days30, "Через 30 дней после получения ТС"},
        };
    }
}
