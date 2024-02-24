using RoomMainSetup.Src;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.Src.Dto
{
   public class FacilitiesDto : BaseDto
    {
        public int FacilitiesId { get; set; }
        public string Title { get; set; }
        public decimal Servicecharge { get; set; }

    }
}
