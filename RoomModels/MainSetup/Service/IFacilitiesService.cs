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
    public interface IFacilitiesService
    {
        Task<FacilitiesDto> Insertasync(FacilitiesDto dto);
        Task<Facilities> Delete(long Id);
        Task<FacilitiesDto> UpdateAsync(FacilitiesDto dto);
    }
    public class FacilitiesService : IFacilitiesService
    {
        private readonly IFacilitiesRepository _FacilitiesRepository;
        private readonly IFacilitiesAssembler _assembler;
        public FacilitiesService(IFacilitiesRepository FacilitiesRepository,
            IFacilitiesAssembler assembler)
        {
            _FacilitiesRepository = FacilitiesRepository;
            _assembler = assembler;
        }
        public async Task<FacilitiesDto> Insertasync(FacilitiesDto dto)
        {
            Facilities Facilities = new Facilities();
            _assembler.copyTo(Facilities, dto);
            await _FacilitiesRepository.AddAsync(Facilities);
            dto.Id = Facilities.Id;
            return dto;
        }

        public async Task<FacilitiesDto> UpdateAsync(FacilitiesDto dto)
        {
            Facilities Facilities = new Facilities();
            _assembler.modifyTo(Facilities, dto);
            await _FacilitiesRepository.UpdateAsync(Facilities);
            return dto;
        }

        public async Task<Facilities> Delete(long Id)
        {
            var room = await _FacilitiesRepository.GetByIdAsync(Id) ?? throw new Exception();
            return await _FacilitiesRepository.DeleteAsync(room).ConfigureAwait(true);
        }
    }
}
