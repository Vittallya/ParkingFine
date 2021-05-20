using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public ICollection<Declaration> Declarations { get; set; }
        public string FIO { get; set; }
        public string DriverLicence { get; set; }
        public string Osago { get; set; }

        public string PasportNumber { get; set; }
        public string PasportSerial { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
