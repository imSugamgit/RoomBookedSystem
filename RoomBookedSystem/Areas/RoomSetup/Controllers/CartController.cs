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
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartRepository _cartRepository;
        private readonly ICartAssembler _cartAssembler;
        public CartController(ICartService cartService,
            ICartRepository cartRepository,
            ICartAssembler cartAssembler)

        {
            _cartRepository = cartRepository;
            _cartAssembler = cartAssembler;
            _cartService = cartService;
        }
        public async Task<IActionResult> Index(CartViewModel vm, string message)
        {
            vm.Carts = new List<Cart>();
            var cart = await _cartRepository.GetAllCartAsync();
            vm.Carts = cart;
            ViewBag.Message = message;
            return View(vm);
        }
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            CartDto cartDto = new CartDto
            {

            };
            return View(cartDto);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(CartDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _cartService.Insertasync(dto);
                    return RedirectToAction("Index", "Cart", new { message = "Cart has been saved successfully." });
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
            var entity = await _cartRepository.GetByIdAsync(id.Value) ?? throw new Exception();
            CartDto dto = new CartDto();
            _cartAssembler.copyFrom(dto, entity);
            return View(dto);
        }
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Update(CartDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _cartService.UpdateAsync(dto);
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
            var room = await _cartRepository.GetByIdAsync(id) ?? throw new Exception();
            return View(room);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> DeleteConfirmed(Cart cart)
        {
            try
            {
                if (cart != null)
                {
                    await _cartService.Delete(cart.Id).ConfigureAwait(true);
                    return RedirectToAction("Index", "Cart", new { messege = "Cart been Delete successfully." });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: Please contact Administrator.";
            }
            return View(cart);
        }
    }
}
