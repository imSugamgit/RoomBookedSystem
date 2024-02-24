using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoomMainSetup.Entity.RoomModel
{
   public class Customer :BaseEntity
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
