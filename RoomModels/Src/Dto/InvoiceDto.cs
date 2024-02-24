using RoomMainSetup.Src;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.Src.Dto
{
    public class InvoiceDto : BaseDto
    {
        public long InvoiceId { get; set; }

        public long ReservationId { get; set; }

        public decimal RoomCharge { get; set; }

        public decimal AdditionalCharges { get; set; }

        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime IssueDate { get; set; }

    }
}
