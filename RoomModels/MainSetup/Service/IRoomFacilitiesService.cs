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
    public interface IRoomFacilitiesService
    {
        Task<RoomFacilitiesDto> Insertasync(RoomFacilitiesDto dto);
        Task<RoomFacilities> Delete(long Id);
        Task<RoomFacilitiesDto> UpdateAsync(RoomFacilitiesDto dto);
    }
    public class RoomFacilitiesService : IRoomFacilitiesService
    {
        private readonly IRoomFacilitiesRepository _RoomFacilitiesRepository;
        private readonly IRoomFacilitiesAssembler _assembler;
        public RoomFacilitiesService(IRoomFacilitiesRepository RoomFacilitiesRepository,
            IRoomFacilitiesAssembler assembler)
        {
            _RoomFacilitiesRepository = RoomFacilitiesRepository;
            _assembler = assembler;
        }
        public async Task<RoomFacilitiesDto> Insertasync(RoomFacilitiesDto dto)
        {
            RoomFacilities RoomFacilities = new RoomFacilities();
            _assembler.copyTo(RoomFacilities, dto);
            await _RoomFacilitiesRepository.AddAsync(RoomFacilities);
            dto.Id = RoomFacilities.Id;
            return dto;
        }

        public async Task<RoomFacilitiesDto> UpdateAsync(RoomFacilitiesDto dto)
        {
            RoomFacilities RoomFacilities = new RoomFacilities();
            _assembler.modifyTo(RoomFacilities, dto);
            await _RoomFacilitiesRepository.UpdateAsync(RoomFacilities);
            return dto;
        }

        public async Task<RoomFacilities> Delete(long Id)
        {
            var room = await _RoomFacilitiesRepository.GetByIdAsync(Id) ?? throw new Exception();
            return await _RoomFacilitiesRepository.DeleteAsync(room).ConfigureAwait(true);
        }
    }
}
