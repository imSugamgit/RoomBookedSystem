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
    public interface IRoomRepository : IRepository<Room>
    {
        Task<List<Room>> GetAllRoomAsync();
    }
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<Room>> GetAllRoomAsync()
        {
            return await GetAllAsync().ToListAsync();
        }
    }
}
