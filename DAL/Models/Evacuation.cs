using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{



    public class Evacuation: ICloneable<Evacuation>
    {
        public int Id { get; set; }
        public int FineId { get; set; }
        public int AutoId { get; set; }
        public int ParkingId { get; set; }

        /// <summary>
        /// Откуда забрали
        /// </summary>
        public string PlaceEvac { get; set; }

        /// <summary>
        /// Когда забрали
        /// </summary>
        public DateTime DateTimeEvac { get; set; }

        /// <summary>
        /// Стоиомсть эвакуации
        /// </summary>
        public double EvacCost { get; set; }

        /// <summary>
        /// Куда забрали
        /// </summary>
        public virtual Parking Parking { get; set; }

        /// <summary>
        /// Почему забрали (нарушение)
        /// </summary>
        public virtual Fine Fine { get; set; }

        /// <summary>
        /// Что забрали
        /// </summary>
        public virtual Auto Auto { get; set; }

        /// <summary>
        /// Где сейчас машина: на стоянке или выдана владельцу
        /// </summary>
        public CarStatus CarStatus { get; set; }

        public ICollection<Declaration> Declarations { get; set; }

        public Evacuation Clone()
        {
            var clone = this.MemberwiseClone() as Evacuation;

            return clone;

        }
    }
}
