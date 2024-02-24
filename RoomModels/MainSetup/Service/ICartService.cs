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
    public interface ICartService
    {
        Task<CartDto> Insertasync(CartDto dto);
        Task<Cart> Delete(long Id);
        Task<CartDto> UpdateAsync(CartDto dto);
    }
    public class CartService : ICartService
    {
        private readonly ICartRepository _CartRepository;
        private readonly ICartAssembler _assembler;
        public CartService(ICartRepository CartRepository,
            ICartAssembler assembler)
        {
            _CartRepository = CartRepository;
            _assembler = assembler;
        }
        public async Task<CartDto> Insertasync(CartDto dto)
        {
            Cart Cart = new Cart();
            _assembler.copyTo(Cart, dto);
            await _CartRepository.AddAsync(Cart);
            dto.Id = Cart.Id;
            return dto;
        }

        public async Task<CartDto> UpdateAsync(CartDto dto)
        {
            Cart Cart = new Cart();
            _assembler.modifyTo(Cart, dto);
            await _CartRepository.UpdateAsync(Cart);
            return dto;
        }

        public async Task<Cart> Delete(long Id)
        {
            var room = await _CartRepository.GetByIdAsync(Id) ?? throw new Exception();
            return await _CartRepository.DeleteAsync(room).ConfigureAwait(true);
        }
    }
}

