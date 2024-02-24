using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoomMainSetup.Entity.RoomModel
{
    public class Room : BaseEntity
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
       
        public RoomAvail Status { get; set; }
        public decimal Price { get; set; }
        public string RoomPicture { get; set; }

        [ForeignKey("RoomTypeId")]
        public long? RoomTypeId { get; set; }
        [NotMapped()]
        public virtual RoomType RoomType { get; set; }

        [ForeignKey("CustomerId")]
        public long? CustomerId { get; set; }
        [NotMapped()]
        public virtual Customer Customer { get; set; }

    }

    public enum RoomAvail : int
    {
        Booked = 0,
        Available = 1
    }
}