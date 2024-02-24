using System;
using System.Collections.Generic;
using System.Text;

namespace RoomMainSetup.Entity.RoomModel
{
   public class Facilities: BaseEntity
    {

        public int FacilitiesId { get; set; }
        public string Title { get; set; }
        public decimal Servicecharge { get; set; }
    }
}
