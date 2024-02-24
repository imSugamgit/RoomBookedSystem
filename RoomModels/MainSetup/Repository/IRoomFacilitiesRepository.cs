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
    public interface IRoomFacilitiesRepository : IRepository<RoomFacilities>
    {
        Task<List<RoomFacilities>> GetAllRoomFacilitiesAsync();
    }
    public class RoomFacilitiesRepository : Repository<RoomFacilities>, IRoomFacilitiesRepository
    {
        public RoomFacilitiesRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<RoomFacilities>> GetAllRoomFacilitiesAsync()
        {
            return await GetAllAsync().ToListAsync();
        }
    }
}
