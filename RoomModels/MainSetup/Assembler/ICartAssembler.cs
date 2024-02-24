using RoomMainSetup.Entity.RoomModel;
using RoomModel.Src.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.MainSetup.Assembler
{
    public interface ICartAssembler
    {
        void copyTo(Cart cart, CartDto dto);
        void copyFrom(CartDto dto, Cart cart);
        void modifyTo(Cart cart, CartDto dto);
    }

    public class CartAssembler : ICartAssembler
    {
        //copy to entity(table)
        public void copyTo(Cart cart, CartDto dto)
        {
            cart.Id = dto.Id;
            cart.CreatedBy = dto.CreatedBy;
            cart.CreatedDate = DateTime.Now;
            cart.CartId = dto.CartId;
            cart.TotalDays = dto.TotalDays;
            cart.TotalPrice = dto.TotalPrice;
            cart.CustomerId = dto.CustomerId;

        }
        //copy from entity(table)
        public void copyFrom(CartDto dto, Cart cart)
        {
            dto.Id = cart.Id;
            dto.CreatedBy = cart.CreatedBy;
            dto.CreatedDate = cart.CreatedDate;
            dto.CustomerId = cart.CustomerId;
            dto.TotalPrice = cart.TotalPrice;
            dto.TotalDays = cart.TotalDays;
            dto.CartId = cart.CartId;


        }

        //modified to entity(table)
        public void modifyTo(Cart cart, CartDto dto)
        {
            cart.Id = dto.Id;
            cart.CreatedBy = dto.CreatedBy;
            cart.CreatedDate = DateTime.Now;
            cart.CartId = dto.CartId;
            cart.TotalDays = dto.TotalDays;
            cart.TotalPrice = dto.TotalPrice;
            cart.CustomerId = dto.CustomerId;
            cart.ModifiedBy = dto.ModifiedBy;
            cart.ModifiedDate = DateTime.Now;
        }
    }
}
