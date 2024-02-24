using RoomMainSetup.Src;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.Src.Dto
{
   public class CustomerDto : BaseDto
    {
        public string IdCardNumber { get; set; }

        public long CustomerId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public int Number { get; set; }
    }
}
