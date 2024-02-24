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
    public class RoomFacilitiesController : Controller
    {

        private readonly IRoomFacilitiesService _roomfacilitiesService;
        private readonly IRoomFacilitiesRepository _roomfacilitiesRepository;
        private readonly IRoomFacilitiesAssembler _roomfacilitiesAssembler;
        public RoomFacilitiesController(IRoomFacilitiesService roomfacilitiesService,
            IRoomFacilitiesRepository roomfacilitiesRepository,
            IRoomFacilitiesAssembler roomfacilitiesAssembler)

        {
            _roomfacilitiesRepository = roomfacilitiesRepository;
            _roomfacilitiesAssembler = roomfacilitiesAssembler;
            _roomfacilitiesService = roomfacilitiesService;
        }
        public async Task<IActionResult> Index(RoomFacilitiesViewModel vm, string message)
        {
            vm.RoomFacilitiess = new List<RoomFacilities>();
            var roomfacilities = await _roomfacilitiesRepository.GetAllRoomFacilitiesAsync();
            vm.RoomFacilitiess = roomfacilities;
            ViewBag.Message = message;
            return View(vm);
        }
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            RoomFacilitiesDto roomDto = new RoomFacilitiesDto
            {

            };
            return View(roomDto);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(RoomFacilitiesDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roomfacilitiesService.Insertasync(dto);
                    return RedirectToAction("Index", "RoomFacilities", new { message = "RoomFacilities has been saved successfully." });
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
            var entity = await _roomfacilitiesRepository.GetByIdAsync(id.Value) ?? throw new Exception();
            RoomFacilitiesDto dto = new RoomFacilitiesDto();
            _roomfacilitiesAssembler.copyFrom(dto, entity);
            return View(dto);
        }
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Update(RoomFacilitiesDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roomfacilitiesService.UpdateAsync(dto);
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
            var room = await _roomfacilitiesRepository.GetByIdAsync(id) ?? throw new Exception();
            return View(room);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> DeleteConfirmed(RoomFacilities roomfacilities)
        {
            try
            {
                if (roomfacilities != null)
                {
                    await _roomfacilitiesService.Delete(roomfacilities.Id).ConfigureAwait(true);
                    return RedirectToAction("Index", "RoomFacilities", new { messege = "RoomFacilities been Delete successfully." });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: Please contact Administrator.";
            }
            return View(roomfacilities);
        }
    }
}