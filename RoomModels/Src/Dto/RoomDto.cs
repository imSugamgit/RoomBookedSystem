using Microsoft.AspNetCore.Mvc.Rendering;
using RoomMainSetup.Entity.RoomModel;
using RoomMainSetup.Src;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.Src.Dto
{
   public class RoomDto : BaseDto
    {
        public int RoomNumber { get; set; }
        public int RoomId { get; set; }
        public decimal Price { get; set; }

        public long? RoomTypeId { get; set; }

        public IList<RoomType> RoomTypes { get; set; } = new List<RoomType>();
        public SelectList RoomTypeList => new SelectList(RoomTypes, nameof(RoomType.Id), nameof(RoomType.Name));

        public long? CustomerId { get; set; }

        public IList<Customer> Customers { get; set; } = new List<Customer>();
        public SelectList CustomerList => new SelectList(Customers, nameof(Customer.CustomerId), nameof(Customer.Name));


    }
}
