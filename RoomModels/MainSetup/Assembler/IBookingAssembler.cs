using RoomMainSetup.Entity.RoomModel;
using RoomModel.Src.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.MainSetup.Assembler
{
    public interface IBookingAssembler
    {
        void copyTo(Booking booking, BookingDto dto);
        void copyFrom(BookingDto dto, Booking booking);
        void modifyTo(Booking booking, BookingDto dto);
    }

    public class BookingAssembler : IBookingAssembler
    {
        //copy to entity(table)
        public void copyTo(Booking booking, BookingDto dto)
        {
            booking.Id = dto.Id;
            booking.CreatedBy = dto.CreatedBy;
            booking.CreatedDate = DateTime.Now;
            booking.BookingDate = dto.BookingDate;
            booking.BookedBy = dto.BookedBy;
            booking.Status = dto.Status;

        }
        //copy from entity(table)
        public void copyFrom(BookingDto dto, Booking booking)
        {
            dto.Id = booking.Id;
            dto.CreatedBy = booking.CreatedBy;
            dto.CreatedDate = booking.CreatedDate;
            dto.BookedBy = booking.BookedBy;
            dto.BookingDate = booking.BookingDate;
            dto.Status = booking.Status;


        }

        //modified to entity(table)
        public void modifyTo(Booking booking, BookingDto dto)
        {
            booking.Id = dto.Id;
            booking.CreatedBy = dto.CreatedBy;
            booking.CreatedDate = DateTime.Now;
            booking.BookingDate = dto.BookingDate;
            booking.BookedBy = dto.BookedBy;
            booking.Status = dto.Status;
            booking.ModifiedBy = dto.ModifiedBy;
            booking.ModifiedDate = DateTime.Now;
        }
    }
}
