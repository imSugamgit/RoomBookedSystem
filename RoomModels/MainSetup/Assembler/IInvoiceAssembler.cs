using RoomMainSetup.Entity.RoomModel;
using RoomModel.Src.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.MainSetup.Assembler
{
    public interface IInvoiceAssembler
    {
        void copyTo(Invoice invoice, InvoiceDto dto);
        void copyFrom(InvoiceDto dto, Invoice invoice);
        void modifyTo(Invoice invoice, InvoiceDto dto);
    }

    public class InvoiceAssembler : IInvoiceAssembler
    {
        //copy to entity(table)
        public void copyTo(Invoice invoice, InvoiceDto dto)
        {
            invoice.CreatedBy = dto.CreatedBy;
            invoice.CreatedDate = DateTime.Now;
            invoice.InvoiceId = dto.InvoiceId;
            invoice.ReservationId = dto.ReservationId;
            invoice.RoomCharge = dto.RoomCharge;
            invoice.AdditionalCharges = dto.AdditionalCharges;
            invoice.TaxAmount = dto.TaxAmount;
            invoice.TotalAmount = dto.TotalAmount;

        }
        //copy from entity(table)
        public void copyFrom(InvoiceDto dto, Invoice invoice)
        {
            dto.Id = invoice.Id;
            dto.CreatedBy = invoice.CreatedBy;
            dto.CreatedDate = invoice.CreatedDate;
            dto.InvoiceId = invoice.InvoiceId;
            dto.ReservationId = invoice.ReservationId;
            dto.RoomCharge = invoice.RoomCharge;
            dto.AdditionalCharges = invoice.AdditionalCharges;
            dto.TaxAmount = invoice.TaxAmount;
            dto.TotalAmount = invoice.TotalAmount;
        }

        //modified to entity(table)
        public void modifyTo(Invoice invoice, InvoiceDto dto)
        {
            invoice.Id = dto.Id;
            invoice.CreatedBy = dto.CreatedBy;
            invoice.CreatedDate = DateTime.Now;
            invoice.InvoiceId = dto.InvoiceId;
            invoice.ReservationId = dto.ReservationId;
            invoice.RoomCharge = dto.RoomCharge;
            invoice.AdditionalCharges = dto.AdditionalCharges;
            invoice.TaxAmount = dto.TaxAmount;
            invoice.TotalAmount = dto.TotalAmount;
            invoice.ModifiedBy = dto.ModifiedBy;
            invoice.ModifiedDate = DateTime.Now;
        }
    }
}
