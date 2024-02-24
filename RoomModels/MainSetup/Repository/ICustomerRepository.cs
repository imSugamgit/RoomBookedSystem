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
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<List<Customer>> GetAllCustomerAsync();
    }
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            return await GetAllAsync().ToListAsync();
        }
    }
}
