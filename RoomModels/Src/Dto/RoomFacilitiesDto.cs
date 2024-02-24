using RoomMainSetup.Src;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.Src.Dto
{
   public class RoomFacilitiesDto : BaseDto
    {
        public int RoomFacilitiesId { get; set; }
        public int RoomId { get; set; }
        
        public int FacilitiesId { get; set; }
        public string Facilities { get; set; }
    }
}
