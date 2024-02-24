using RoomMainSetup.Entity.RoomModel;
using RoomModel.Src.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.MainSetup.Assembler
{
    public interface ICustomerAssembler
    {
        void copyTo(Customer customer, CustomerDto dto);
        void copyFrom(CustomerDto dto, Customer cart);
        void modifyTo(Customer customer, CustomerDto dto);
    }

    public class CustomerAssembler : ICustomerAssembler
    {
        //copy to entity(table)
        public void copyTo(Customer customer, CustomerDto dto)
        {
            customer.Id = dto.Id;
            customer.CreatedBy = dto.CreatedBy;
            customer.CreatedDate = DateTime.Now;
            customer.CustomerId = dto.CustomerId;
            customer.Name = dto.Name;
            customer.Pincode = dto.Pincode;
            customer.ContactNumber = dto.ContactNumber;
            customer.Number = dto.Number;
            customer.Email = dto.Email;
            customer.IdCardNumber = dto.IdCardNumber;
            customer.City = dto.City;

        }
        //copy from entity(table)
        public void copyFrom(CustomerDto dto, Customer customer)
        {
            dto.Id = customer.Id;
            dto.CreatedBy = customer.CreatedBy;
            dto.CreatedDate = customer.CreatedDate;
            dto.CustomerId = customer.CustomerId;
            dto.Name = customer.Name;
            dto.Pincode = customer.Pincode;
            dto.ContactNumber = customer.ContactNumber;
            dto.Number = customer.Number;
            dto.Email = customer.Email;
            dto.IdCardNumber = customer.IdCardNumber;
            dto.City = customer.City;
            


        }

        //modified to entity(table)
        public void modifyTo(Customer customer, CustomerDto dto)
        {
            customer.Id = dto.Id;
            customer.CreatedBy = dto.CreatedBy;
            customer.CreatedDate = DateTime.Now;
            customer.Name = dto.Name;
            customer.Pincode = dto.Pincode;
            customer.ContactNumber = dto.ContactNumber;
            customer.Number = dto.Number;
            customer.Email = dto.Email;
            customer.CustomerId = dto.CustomerId;
            customer.ModifiedBy = dto.ModifiedBy;
            customer.ModifiedDate = DateTime.Now; customer.IdCardNumber = dto.IdCardNumber;
            customer.City = dto.City;
        }
    }
}
