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
    public class FacilitiesController : Controller
    {
     
        private readonly IFacilitiesService _facilitiesService;
        private readonly IFacilitiesRepository _facilitiesRepository;
        private readonly IFacilitiesAssembler _facilitiesAssembler;
        public FacilitiesController(IFacilitiesService facilitiesService,
            IFacilitiesRepository facilitiesRepository,
            IFacilitiesAssembler facilitiesAssembler)

        {
            _facilitiesRepository = facilitiesRepository;
            _facilitiesAssembler = facilitiesAssembler;
            _facilitiesService = facilitiesService;
        }
        public async Task<IActionResult> Index(FacilitiesViewModel vm, string message)
        {
            vm.Facilitiess = new List<Facilities>();
            var facilities = await _facilitiesRepository.GetAllFacilitiesAsync();
            vm.Facilitiess = facilities;
            ViewBag.Message = message;
            return View(vm);
        }
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            FacilitiesDto roomDto = new FacilitiesDto
            {

            };
            return View(roomDto);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(FacilitiesDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _facilitiesService.Insertasync(dto);
                    return RedirectToAction("Index", "Facilities", new { message = "Facilities has been saved successfully." });
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
            var entity = await _facilitiesRepository.GetByIdAsync(id.Value) ?? throw new Exception();
            FacilitiesDto dto = new FacilitiesDto();
            _facilitiesAssembler.copyFrom(dto, entity);
            return View(dto);
        }
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Update(FacilitiesDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _facilitiesService.UpdateAsync(dto);
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
            var room = await _facilitiesRepository.GetByIdAsync(id) ?? throw new Exception();
            return View(room);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> DeleteConfirmed(Facilities facilities)
        {
            try
            {
                if (facilities != null)
                {
                    await _facilitiesService.Delete(facilities.Id).ConfigureAwait(true);
                    return RedirectToAction("Index", "Facilities", new { messege = "Facilities been Delete successfully." });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: Please contact Administrator.";
            }
            return View(facilities);
        }
    }
}
