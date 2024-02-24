using RoomMainSetup.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoomMainSetup.Entity.RoomModel
{
   public class Cart: BaseEntity
    {
        public int CartId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalDays { get; set; }


        [ForeignKey("CustomerId")]
        public long? CustomerId { get; set; }
        [NotMapped()]
        public virtual Customer Customer { get; set; }


        [ForeignKey("RoomId")]
        public long? RoomId { get; set; }
        [NotMapped()]
        public virtual Room Room { get; set; }



    }
}
