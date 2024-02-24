using RoomMainSetup.Entity.RoomModel;
using RoomModel.MainSetup.Assembler;
using RoomModel.MainSetup.Repository;
using RoomModel.Src.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomModel.MainSetup.Service
{
    public interface IBookingService
    {
        Task<BookingDto> Insertasync(BookingDto dto);
        Task<Booking> Delete(long Id);
        Task<BookingDto> UpdateAsync(BookingDto dto);
    }
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _BookingRepository;
        private readonly IBookingAssembler _assembler;
        public BookingService(IBookingRepository BookingRepository,
            IBookingAssembler assembler)
        {
            _BookingRepository = BookingRepository;
            _assembler = assembler;
        }
        public async Task<BookingDto> Insertasync(BookingDto dto)
        {
            Booking Booking = new Booking();
            _assembler.copyTo(Booking, dto);
            await _BookingRepository.AddAsync(Booking);
            dto.Id = Booking.Id;
            return dto;
        }

        public async Task<BookingDto> UpdateAsync(BookingDto dto)
        {
            Booking Booking = new Booking();
            _assembler.modifyTo(Booking, dto);
            await _BookingRepository.UpdateAsync(Booking);
            return dto;
        }

        public async Task<Booking> Delete(long Id)
        {
            var booking = await _BookingRepository.GetByIdAsync(Id) ?? throw new Exception();
            return await _BookingRepository.DeleteAsync(booking).ConfigureAwait(true);
        }
    }
}
