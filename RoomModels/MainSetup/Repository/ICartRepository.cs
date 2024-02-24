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
  public  interface ICartRepository: IRepository<Cart>
    {
         Task<List<Cart>> GetAllCartAsync();
        }
        public class CartRepository : Repository<Cart>, ICartRepository
    {
            public CartRepository(ApplicationDbContext context) : base(context)
            {

            }

            public async Task<List<Cart>> GetAllCartAsync()
            {
                return await GetAllAsync().ToListAsync();
            }
        }
    }

