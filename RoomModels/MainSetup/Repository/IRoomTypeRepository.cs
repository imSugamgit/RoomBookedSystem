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
    public interface IRoomTypeRepository : IRepository<RoomType>
    {
        Task<List<RoomType>> GetAllRoomTypeAsync();
    }
    public class RoomTypeRepository : Repository<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<RoomType>> GetAllRoomTypeAsync()
        {
            return await GetAllAsync().ToListAsync();
        }
    }
}
