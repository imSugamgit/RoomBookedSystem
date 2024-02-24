using RoomMainSetup.Entity.RoomModel;
using RoomModel.Src.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.MainSetup.Assembler
{
    public interface IRoomTypeAssembler
    {
        void copyTo(RoomType roomtype, RoomTypeDto dto);
        void copyFrom(RoomTypeDto dto, RoomType roomtype);
        void modifyTo(RoomType roomtype, RoomTypeDto dto);
    }

    public class RoomTypeAssembler : IRoomTypeAssembler
    {
        //copy to entity(table)
        public void copyTo(RoomType roomtype, RoomTypeDto dto)
        {
            roomtype.CreatedBy = dto.CreatedBy;
            roomtype.CreatedDate = DateTime.Now;
            roomtype.RoomTypeId = dto.RoomTypeId;
            roomtype.Name = dto.Name;

        }
        //copy from entity(table)
        public void copyFrom(RoomTypeDto dto, RoomType roomtype)
        {
            dto.Id = roomtype.Id;
            dto.CreatedBy = roomtype.CreatedBy;
            dto.CreatedDate = roomtype.CreatedDate;
            dto.RoomTypeId = roomtype.RoomTypeId;
            dto.Name = roomtype.Name;
        }

        //modified to entity(table)
        public void modifyTo(RoomType roomtype, RoomTypeDto dto)
        {
            roomtype.Id = dto.Id;
            roomtype.CreatedBy = dto.CreatedBy;
            roomtype.CreatedDate = DateTime.Now;
            roomtype.Name = dto.Name;
            roomtype.RoomTypeId = dto.RoomTypeId;
            roomtype.ModifiedBy = dto.ModifiedBy;
            roomtype.ModifiedDate = DateTime.Now;
        }
    }
}
