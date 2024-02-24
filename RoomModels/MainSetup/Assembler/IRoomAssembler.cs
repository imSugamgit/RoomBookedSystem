using RoomMainSetup.Entity.RoomModel;
using RoomModel.Src.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomModel.MainSetup.Assembler
{
    public interface IRoomAssembler
    {
        void copyTo(Room room, RoomDto dto);
        void copyFrom(RoomDto dto, Room room);
        void modifyTo(Room room, RoomDto dto);
    }

    public class RoomAssembler : IRoomAssembler
    {
        //copy to entity(table)
        public void copyTo(Room room, RoomDto dto)
        {
            room.CreatedBy = dto.CreatedBy;
            room.CreatedDate = DateTime.Now;
            room.RoomNumber = dto.RoomNumber;
            room.RoomTypeId = dto.RoomTypeId;
            room.Price = dto.Price;
            
        }
        //copy from entity(table)
        public void copyFrom(RoomDto dto, Room room)
        {
            dto.Id = room.Id;
            dto.CreatedBy = room.CreatedBy;
            dto.CreatedDate = room.CreatedDate;
            dto.RoomNumber = room.RoomNumber;
            dto.RoomTypeId = room.RoomTypeId;
        }

        //modified to entity(table)
        public void modifyTo(Room room, RoomDto dto)
        {
            room.Id = dto.Id;
            room.CreatedBy = dto.CreatedBy;
            room.CreatedDate = DateTime.Now;
            room.RoomNumber = dto.RoomNumber;
            room.RoomTypeId = dto.RoomTypeId;
            room.ModifiedBy = dto.ModifiedBy;
            room.ModifiedDate = DateTime.Now;
        }
    }
}
  