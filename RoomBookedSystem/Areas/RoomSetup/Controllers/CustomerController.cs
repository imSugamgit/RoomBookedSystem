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
    public class CustomerController : Controller
    {

        private readonly ICustomerService _customerService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerAssembler _customerAssembler;
        public CustomerController(ICustomerService customerService,
            ICustomerRepository customerRepository,
            ICustomerAssembler customerAssembler)

        {
            _customerRepository = customerRepository;
            _customerAssembler = customerAssembler;
            _customerService = customerService;
        }
        public async Task<IActionResult> Index(CustomerViewModel vm, string message)
        {
            vm.Customers = new List<Customer>();
            var customer = await _customerRepository.GetAllCustomerAsync();
            vm.Customers = customer;
            ViewBag.Message = message;
            return View(vm);
        }
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            CustomerDto customerDto = new CustomerDto
            {

            };
            return View(customerDto);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(CustomerDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _customerService.Insertasync(dto);
                    return RedirectToAction("Index", "Customer", new { message = "Customer has been saved successfully." });
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
            var entity = await _customerRepository.GetByIdAsync(id.Value) ?? throw new Exception();
            CustomerDto dto = new CustomerDto();
            _customerAssembler.copyFrom(dto, entity);
            return View(dto);
        }
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Update(CustomerDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _customerService.UpdateAsync(dto);
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
            var room = await _customerRepository.GetByIdAsync(id) ?? throw new Exception();
            return View(room);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> DeleteConfirmed(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    await _customerService.Delete(customer.Id).ConfigureAwait(true);
                    return RedirectToAction("Index", "Customer", new { messege = "Customer been Delete successfully." });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: Please contact Administrator.";
            }
            return View(customer);
        }
    }
}