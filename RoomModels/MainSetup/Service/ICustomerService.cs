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
    public interface ICustomerService
    {
        Task<CustomerDto> Insertasync(CustomerDto dto);
        Task<Customer> Delete(long Id);
        Task<CustomerDto> UpdateAsync(CustomerDto dto);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _CustomerRepository;
        private readonly ICustomerAssembler _assembler;
        public CustomerService(ICustomerRepository CustomerRepository,
            ICustomerAssembler assembler)
        {
            _CustomerRepository = CustomerRepository;
            _assembler = assembler;
        }
        public async Task<CustomerDto> Insertasync(CustomerDto dto)
        {
            Customer Customer = new Customer();
            _assembler.copyTo(Customer, dto);
            await _CustomerRepository.AddAsync(Customer);
            dto.Id = Customer.Id;
            return dto;
        }

        public async Task<CustomerDto> UpdateAsync(CustomerDto dto)
        {
            Customer Customer = new Customer();
            _assembler.modifyTo(Customer, dto);
            await _CustomerRepository.UpdateAsync(Customer);
            return dto;
        }

        public async Task<Customer> Delete(long Id)
        {
            var room = await _CustomerRepository.GetByIdAsync(Id) ?? throw new Exception();
            return await _CustomerRepository.DeleteAsync(room).ConfigureAwait(true);
        }
    }
}
