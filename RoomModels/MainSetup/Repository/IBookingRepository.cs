using Microsoft.EntityFrameworkCore;
using RoomMainSetup.BaseRepo;
using RoomMainSetup.Data;
using RoomMainSetup.Entity.RoomModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomModel.MainSetup.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<List<Booking>> GetAllBookingAsync();
    }
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<Booking>> GetAllBookingAsync()
        {
            return await GetAllAsync().ToListAsync();
        }
    }
}

