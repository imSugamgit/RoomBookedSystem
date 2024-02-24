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
    public class InvoiceController : Controller
    {
       
        private readonly IInvoiceService _invoiceService;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceAssembler _invoiceAssembler;
        public InvoiceController(IInvoiceService invoiceService,
            IInvoiceRepository invoiceRepository,
            IInvoiceAssembler invoiceAssembler)

        {
            _invoiceRepository = invoiceRepository;
            _invoiceAssembler = invoiceAssembler;
            _invoiceService = invoiceService;
        }
        public async Task<IActionResult> Index(InvoiceViewModel vm, string message)
        {
            vm.Invoices = new List<Invoice>();
            var invoice = await _invoiceRepository.GetAllInvoiceAsync();
            vm.Invoices = invoice;
            ViewBag.Message = message;
            return View(vm);
        }
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            InvoiceDto invoiceDto = new InvoiceDto
            {

            };
            return View(invoiceDto);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(InvoiceDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _invoiceService.Insertasync(dto);
                    return RedirectToAction("Index", "Invoice", new { message = "Invoice has been saved successfully." });
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
            var entity = await _invoiceRepository.GetByIdAsync(id.Value) ?? throw new Exception();
            InvoiceDto dto = new InvoiceDto();
            _invoiceAssembler.copyFrom(dto, entity);
            return View(dto);
        }
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Update(InvoiceDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _invoiceService.UpdateAsync(dto);
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
            var room = await _invoiceRepository.GetByIdAsync(id) ?? throw new Exception();
            return View(room);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> DeleteConfirmed(Invoice invoice)
        {
            try
            {
                if (invoice != null)
                {
                    await _invoiceService.Delete(invoice.Id).ConfigureAwait(true);
                    return RedirectToAction("Index", "Invoice", new { messege = "Invoice been Delete successfully." });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: Please contact Administrator.";
            }
            return View(invoice);
        }
    }
}
