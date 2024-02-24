using RoomMainSetup.Src;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.Src.Dto
{
   public class BookingDto : BaseDto

    {
        public DateTime? BookingDate { get; set; }
        public string Status { get; set; }
        public string BookedBy { get; set; }
    }
}
