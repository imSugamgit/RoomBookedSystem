using RoomMainSetup.Entity.RoomModel;
using RoomModel.Src.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.MainSetup.Assembler
{
    public interface IRoomFacilitiesAssembler
    {
        void copyTo(RoomFacilities roomfacilities, RoomFacilitiesDto dto);
        void copyFrom(RoomFacilitiesDto dto, RoomFacilities roomfacilities);
        void modifyTo(RoomFacilities roomfacilities, RoomFacilitiesDto dto);
    }

    public class RoomFacilitiesAssembler : IRoomFacilitiesAssembler
    {
        //copy to entity(table)
        public void copyTo(RoomFacilities roomfacilities, RoomFacilitiesDto dto)
        {
            roomfacilities.CreatedBy = dto.CreatedBy;
            roomfacilities.CreatedDate = DateTime.Now;
            roomfacilities.RoomFacilitiesId = dto.RoomFacilitiesId;

        }
        //copy from entity(table)
        public void copyFrom(RoomFacilitiesDto dto, RoomFacilities roomfacilities)
        {
            dto.Id = roomfacilities.Id;
            dto.CreatedBy = roomfacilities.CreatedBy;
            dto.CreatedDate = roomfacilities.CreatedDate;
            dto.RoomFacilitiesId = roomfacilities.RoomFacilitiesId;
        }

        //modified to entity(table)
        public void modifyTo(RoomFacilities roomfacilities, RoomFacilitiesDto dto)
        {
            roomfacilities.Id = dto.Id;
            roomfacilities.CreatedBy = dto.CreatedBy;
            roomfacilities.CreatedDate = DateTime.Now;
            roomfacilities.RoomFacilitiesId = dto.RoomFacilitiesId;
            roomfacilities.ModifiedBy = dto.ModifiedBy;
            roomfacilities.ModifiedDate = DateTime.Now;
        }
    }
}
