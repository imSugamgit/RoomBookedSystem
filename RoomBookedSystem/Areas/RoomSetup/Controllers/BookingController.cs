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
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingAssembler _bookingAssembler;
        public BookingController(IBookingService bookingService,
            IBookingRepository bookingRepository,
            IBookingAssembler bookingAssembler)

        {
            _bookingRepository = bookingRepository;
            _bookingAssembler = bookingAssembler;
            _bookingService = bookingService;
        }
        public async Task<IActionResult> Index(BookingViewModel vm, string message)
        {
            vm.Bookings = new List<Booking>();
            var booking = await _bookingRepository.GetAllBookingAsync();
            vm.Bookings = booking;
            ViewBag.Message = message;
            return View(vm);
        }
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            BookingDto bookingDto = new BookingDto
            {

            };
            return View(bookingDto);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(BookingDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _bookingService.Insertasync(dto);
                    return RedirectToAction("Index", "Booking", new { message = "Booking has been saved successfully." });
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
            var entity = await _bookingRepository.GetByIdAsync(id.Value) ?? throw new Exception();
            BookingDto dto = new BookingDto();
            _bookingAssembler.copyFrom(dto, entity);
            return View(dto);
        }
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Update(BookingDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _bookingService.UpdateAsync(dto);
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
            var booking = await _bookingRepository.GetByIdAsync(id) ?? throw new Exception();
            return View(booking);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> DeleteConfirmed(Booking booking)
        {
            try
            {
                if (booking != null)
                {
                    await _bookingService.Delete(booking.Id).ConfigureAwait(true);
                    return RedirectToAction("Index", "Booking", new { messege = "Booking been Delete successfully." });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: Please contact Administrator.";
            }
            return View(booking);
        }
    }
}
