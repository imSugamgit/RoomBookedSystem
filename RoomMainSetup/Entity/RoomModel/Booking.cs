using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoomMainSetup.Entity.RoomModel
{
   public class Booking : BaseEntity
    {
        public DateTime? BookingDate { get; set; }
        public string Status { get; set; }
        public string BookedBy { get; set; }


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
