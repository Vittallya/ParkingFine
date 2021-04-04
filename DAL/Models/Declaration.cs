using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    /// <summary>
    /// Заявление на получение ТС
    /// </summary>
    /// 

    
    public enum DeclayerType
    {
        /// <summary>
        /// Владелец авто
        /// </summary>
        OwnerOfCar, 
        /// <summary>
        /// Представитель
        /// </summary>
        Presenter, 


        /// <summary>
        /// Доверенное лицо
        /// </summary>
        TrueFace
    }

    public enum PayingType
    {
        Online, UpponArrival, Days30
    }

    public enum DecStatus
    {
        /// <summary>
        ///  активно, не оплачено
        /// </summary>
     /// 
        Active, 
        /// <summary>
        /// Активно и оплачено
        /// </summary>
        ActivePaid, 

        /// <summary>
        /// Авто получено, но оплата не внесена
        /// </summary>
        /// 
        GottedNotPaid, 
        /// <summary>
        /// Полностью завершено
        /// </summary>
        Completed, 

        DeclayerNotCome,
        Canceled
    }

    public class Declaration
    {
        public int Id { get; set; }
        public int EvacuationId { get; set; }
        
        public DateTime CreatingDate { get; set; } 
        public DateTime ComingDate { get; set; } 
        public DeclayerType DeclayerType { get; set; }

        public PayingType PayingType { get; set; }
        public DecStatus DecStatus { get; set; }

        public Evacuation Evacuation { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
