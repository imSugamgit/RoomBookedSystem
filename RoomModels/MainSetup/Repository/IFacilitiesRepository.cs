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
    public interface IFacilitiesRepository : IRepository<Facilities>
    {
        Task<List<Facilities>> GetAllFacilitiesAsync();
    }
    public class FacilitiesRepository : Repository<Facilities>, IFacilitiesRepository
    {
        public FacilitiesRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<Facilities>> GetAllFacilitiesAsync()
        {
            return await GetAllAsync().ToListAsync();
        }
    }
}

