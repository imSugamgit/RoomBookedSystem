using RoomMainSetup.Entity.RoomModel;
using RoomModel.Src.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.MainSetup.Assembler
{
    public interface IFacilitiesAssembler
    {
        void copyTo(Facilities facilities, FacilitiesDto dto);
        void copyFrom(FacilitiesDto dto, Facilities facilities);
        void modifyTo(Facilities facilities, FacilitiesDto dto);
    }

    public class FacilitiesAssembler : IFacilitiesAssembler
    {
        //copy to entity(table)
        public void copyTo(Facilities facilities, FacilitiesDto dto)
        {
            facilities.CreatedBy = dto.CreatedBy;
            facilities.CreatedDate = DateTime.Now;
            facilities.FacilitiesId = dto.FacilitiesId;
            facilities.Title = dto.Title;

        }
        //copy from entity(table)
        public void copyFrom(FacilitiesDto dto, Facilities facilities)
        {
            dto.Id = facilities.Id;
            dto.CreatedBy = facilities.CreatedBy;
            dto.CreatedDate = facilities.CreatedDate;
            dto.FacilitiesId = facilities.FacilitiesId;
            dto.Title = facilities.Title;
        }

        //modified to entity(table)
        public void modifyTo(Facilities facilities, FacilitiesDto dto)
        {
            facilities.Id = dto.Id;
            facilities.CreatedBy = dto.CreatedBy;
            facilities.CreatedDate = DateTime.Now;
            facilities.FacilitiesId = dto.FacilitiesId;
            facilities.Title = dto.Title;
            facilities.ModifiedBy = dto.ModifiedBy;
            facilities.ModifiedDate = DateTime.Now;
        }
    }
}
