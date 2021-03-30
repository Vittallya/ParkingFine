using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProfileDto
    {
        public string FIO { get; set; }
        public string DriverLicence { get; set; }
        public string Osago { get; set; }

        public string PasportNumber { get; set; }
        public string PasportSerial { get; set; }
        public string PhoneNumber { get; set; }
    }
}
