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
    public interface IRoomTypeService
    {
        Task<RoomTypeDto> Insertasync(RoomTypeDto dto);
        Task<RoomType> Delete(long Id);
        Task<RoomTypeDto> UpdateAsync(RoomTypeDto dto);
    }
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _RoomTypeRepository;
        private readonly IRoomTypeAssembler _assembler;
        public RoomTypeService(IRoomTypeRepository RoomTypeRepository,
            IRoomTypeAssembler assembler)
        {
            _RoomTypeRepository = RoomTypeRepository;
            _assembler = assembler;
        }
        public async Task<RoomTypeDto> Insertasync(RoomTypeDto dto)
        {
            RoomType RoomType = new RoomType();
            _assembler.copyTo(RoomType, dto);
            await _RoomTypeRepository.AddAsync(RoomType);
            dto.Id = RoomType.Id;
            return dto;
        }

        public async Task<RoomTypeDto> UpdateAsync(RoomTypeDto dto)
        {
            RoomType RoomType = new RoomType();
            _assembler.modifyTo(RoomType, dto);
            await _RoomTypeRepository.UpdateAsync(RoomType);
            return dto;
        }

        public async Task<RoomType> Delete(long Id)
        {
            var room = await _RoomTypeRepository.GetByIdAsync(Id) ?? throw new Exception();
            return await _RoomTypeRepository.DeleteAsync(room).ConfigureAwait(true);
        }
    }
}

