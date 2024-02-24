using System;
using System.Collections.Generic;
using System.Text;

namespace RoomMainSetup.Entity.RoomModel
{
   public class RoomFacilities: BaseEntity
    {
        public int RoomFacilitiesId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int FacilitiesId { get; set; }
        public string Facilities { get; set; }

    }
}
