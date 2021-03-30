using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Auto
    {
        public int Id { get; set; }
        public string GovNumber { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }

        public ICollection<Evacuation> Evacuations { get; set; }
    }
}
public enum CarStatus
{
    AtParking, WithOwner, WithTrueFace
}
