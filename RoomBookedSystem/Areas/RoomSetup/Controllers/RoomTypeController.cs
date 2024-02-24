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
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeService _roomtypeService;
        private readonly IRoomTypeRepository _roomtypeRepository;
        private readonly IRoomTypeAssembler _roomtypeAssembler;
        public RoomTypeController(IRoomTypeService roomtypeService,
            IRoomTypeRepository roomtypeRepository,
            IRoomTypeAssembler roomtypeAssembler)

        {
            _roomtypeRepository = roomtypeRepository;
            _roomtypeAssembler = roomtypeAssembler;
            _roomtypeService = roomtypeService;
        }
        public async Task<IActionResult> Index(RoomTypeViewModel vm, string message)
        {
            vm.RoomTypes = new List<RoomType>();
            var roomtype = await _roomtypeRepository.GetAllRoomTypeAsync();
            vm.RoomTypes = roomtype;
            ViewBag.Message = message;
            return View(vm);
        }
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            RoomTypeDto roomtypeDto = new RoomTypeDto
            {

            };
            return View(roomtypeDto);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(RoomTypeDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roomtypeService.Insertasync(dto);
                    return RedirectToAction("Index", "RoomType", new { message = "RoomType has been saved successfully." });
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
            var entity = await _roomtypeRepository.GetByIdAsync(id.Value) ?? throw new Exception();
            RoomTypeDto dto = new RoomTypeDto();
            _roomtypeAssembler.copyFrom(dto, entity);
            return View(dto);
        }
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Update(RoomTypeDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roomtypeService.UpdateAsync(dto);
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
            var roomtype = await _roomtypeRepository.GetByIdAsync(id) ?? throw new Exception();
            return View(roomtype);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> DeleteConfirmed(RoomType roomtype)
        {
            try
            {
                if (roomtype != null)
                {
                    await _roomtypeService.Delete(roomtype.Id).ConfigureAwait(true);
                    return RedirectToAction("Index", "RoomType", new { messege = "RoomType been Delete successfully." });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: Please contact Administrator.";
            }
            return View(roomtype);
        }
    }
}
