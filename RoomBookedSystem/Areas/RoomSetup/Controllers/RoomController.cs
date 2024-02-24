using Microsoft.AspNetCore.Mvc;
using RoomMainSetup.Entity.RoomModel;
using RoomModel.MainSetup.Assembler;
using RoomModel.MainSetup.Repository;
using RoomModel.MainSetup.Service;
using RoomModel.Src.Dto;
using RoomModel.Src.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookedSystem.Areas.RoomSetup.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomAssembler _roomAssembler;
        public RoomController(IRoomService roomService,
            IRoomRepository roomRepository,
            IRoomAssembler roomAssembler)
           
        {
            _roomRepository = roomRepository;
            _roomAssembler = roomAssembler;
            _roomService = roomService;
        }
        public async Task<IActionResult> Index(RoomViewModel vm, string message)
        {
            vm.Rooms = new List<Room>();
            var room = await _roomRepository.GetAllRoomAsync();
            vm.Rooms = room;
            ViewBag.Message = message;
            return View(vm);
        }
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            RoomDto roomDto = new RoomDto
            {
               
            };
            return View(roomDto);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(RoomDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roomService.Insertasync(dto);
                    return RedirectToAction("Index", "Room", new { message = "Room has been saved successfully." });
                }
                else
                {
                    ViewBag.Message = "Error: Invalid data !";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: Please contact Administrator.";
            }
            return View(dto);
        }

        [HttpGet()]
        public async Task<IActionResult> Update(long? id)
        {
            if (!id.HasValue)
            {

            }
            var entity = await _roomRepository.GetByIdAsync(id.Value) ?? throw new Exception();
            RoomDto dto = new RoomDto();
            _roomAssembler.copyFrom(dto, entity);
            return View(dto);
        }
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Update(RoomDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roomService.UpdateAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return View();
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(long id)
        {
            var room = await _roomRepository.GetByIdAsync(id) ?? throw new Exception();
            return View(room);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> DeleteConfirmed(Room room)
        {
            try
            {
                if (room != null)
                {
                    await _roomService.Delete(room.Id).ConfigureAwait(true);
                    return RedirectToAction("Index", "Room", new { messege = "Room been Delete successfully." });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: Please contact Administrator.";
            }
            return View(room);
        }
    }
}
