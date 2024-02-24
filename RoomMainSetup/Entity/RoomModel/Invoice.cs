using System;
using System.Collections.Generic;
using System.Text;

namespace RoomMainSetup.Entity.RoomModel
{
   public class Invoice: BaseEntity
    {
       
            public long InvoiceId { get; set; }

            public long ReservationId { get; set; } 

            public decimal RoomCharge { get; set; }

            public decimal AdditionalCharges { get; set; } 

            public decimal TaxAmount { get; set; } 
            public decimal TotalAmount { get; set; } 

            public DateTime IssueDate { get; set; } // Date when the invoice is issued

        }

    }

