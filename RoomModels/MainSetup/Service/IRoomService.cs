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
   public interface IRoomService
    {
        Task<RoomDto> Insertasync(RoomDto dto);
        Task<Room> Delete(long Id);
        Task<RoomDto> UpdateAsync(RoomDto dto);
    }
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _RoomRepository;
        private readonly IRoomAssembler _assembler;
        public RoomService(IRoomRepository RoomRepository,
            IRoomAssembler assembler)
        {
            _RoomRepository = RoomRepository;
            _assembler = assembler;
        }
        public async Task<RoomDto> Insertasync(RoomDto dto)
        {
            Room Room = new Room();
            _assembler.copyTo(Room, dto);
            await _RoomRepository.AddAsync(Room);
            dto.Id = Room.Id;
            return dto;
        }

        public async Task<RoomDto> UpdateAsync(RoomDto dto)
        {
            Room Room = new Room();
            _assembler.modifyTo(Room, dto);
            await _RoomRepository.UpdateAsync(Room);
            return dto;
        }

        public async Task<Room> Delete(long Id)
        {
            var room = await _RoomRepository.GetByIdAsync(Id) ?? throw new Exception();
            return await _RoomRepository.DeleteAsync(room).ConfigureAwait(true);
        }
    }
}

 