using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DeclarationDto
    {
        public DateTime CreatingDate { get; set; }
        public DateTime ComingDate { get; set; }
        public DeclayerType DeclayerType { get; set; }

        public PayingType PayingType { get; set; }
        public DecStatus DecStatus { get; set; }
    }
}
