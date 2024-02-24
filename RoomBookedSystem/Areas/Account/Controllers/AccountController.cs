using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomMainSetup.Data;
using RoomUser.Src.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using static RoomUser.Src.ViewModel.AccountViewModel;

namespace RoomBookedSystem.Areas.Account.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
       
        public AccountController(UserManager<ApplicationUser> userManager,
                                     SignInManager<ApplicationUser> signInManager
            , ApplicationDbContext applicationDbContext,
            RoleManager<IdentityRole> roleManager
           
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
            _roleManager = roleManager;
           
        }
        //Index
        [AllowAnonymous]
        public async Task<IActionResult> Index(UserViewModel vm)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allUserExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
            vm.Users = allUserExceptCurrentUser;
            return View(vm);
        }//Index

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string message, string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            if (!String.IsNullOrEmpty(returnUrl))
            {

                return RedirectToAction(returnUrl);
            }
            ViewBag.Message = message;
            return View();
        }//GET:/Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { message = "Welcome ! You have Successfully Logged In." });
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Invalid username or password !");
                }
            }
            return View(model);
        } // POST: /Account/Login

        [HttpGet()]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }//Register:GETACTION
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                AddErrors(result);
            }
            return View(model);
        }//POST/Account/Reister

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpGet()]
        [AllowAnonymous()]
        public IActionResult RecoveryPassword()
        {
            return View();
        }//GET:ACcount//ForgotPassword
        [HttpPost()]
        [AllowAnonymous()]
        public async Task<IActionResult> RecoveryPassword(RecoveryPasswordViewModel vm)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(vm.Email);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ChangePassword", "Account", new { userId = user.Id, token = HttpUtility.UrlEncode(token) }, protocol: Request.Scheme);
            string to = vm.Email;
            string subject = "Password Recovery";
            string messege = "Dear " + vm.Name + "," + "<br/>The Request for your password recovery is accepted." +
                             "Please click on the below link to recover your account." +
                             "<br/><br/><a href='" + callbackUrl + "'>" + "Change Password" + "</a> ";
            MailMessage mm = new MailMessage
            {
                Subject = subject,
                Body = messege,
                IsBodyHtml = true,

                From = new MailAddress("ejamsugam@gmail.com")
            };
            mm.To.Add(to);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = true,
                EnableSsl = true,
            };
            smtp.Credentials = new System.Net.NetworkCredential("ejamsugam@gmail.com", "frpfcpvrtblrddks");
            smtp.Send(mm);
            return RedirectToAction("RecoveryPasswordConfirmed", "Account", new { Email = vm.Email, Name = vm.Name });
        }//POST:Account//ForgotPassword

        [HttpGet()]
        [AllowAnonymous()]
        public ActionResult RecoveryPasswordConfirmed(string Email, string Name)
        {
            RecoveryPasswordViewModel vm = new RecoveryPasswordViewModel()
            {
                Email = Email,
                Name = Name
            };
            return View(vm);
        }//Get/Account/ForgetConfirmPassword

        public async Task<IActionResult> SignOut()//Logout
        {
            // after signout this will redirect to your provided target
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            return RedirectToAction("Login", "Account");
        }

        [HttpGet()]
        [AllowAnonymous()]
        public async Task<IActionResult> ChangePassword(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (userId == null || token == null)
            {
                return RedirectToAction(nameof(Login));
            }
            ChangePasswordViewModel vm = new ChangePasswordViewModel
            {
                UserId = userId,
                Email = user.Email,
                Token = token
            };
            return View(vm);
        }

        [HttpPost()]
        [AllowAnonymous()]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");

            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByIdAsync(model.UserId);
                var result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(model.Token), model.NewPassword);

                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(user.UserName, model.NewPassword, false, lockoutOnFailure: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(model);
        }

        [HttpGet()]
        [AllowAnonymous()]
        public IActionResult Manage(string UserName)
        {
            ManageUserViewModel model = new ManageUserViewModel();
            if (!string.IsNullOrEmpty(UserName))
            {
                model.UserName = UserName;
            }
            else
            {
                model.UserName = User.Identity.Name;
            }

            return View(model);
        }//Manage Bhaneko chai ChangePassword

        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(ManageUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var newPassword = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
                user.PasswordHash = newPassword;
                var res = await _userManager.UpdateAsync(user);

                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { Messege = Messege.ChangePasswordSuccess });
                }
                else
                {
                    AddErrors(res);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private bool HasPassword(string username)
        {
            string id = _applicationDbContext.Users.Where(x => x.UserName == username).FirstOrDefault().Id;
            var user = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum Messege
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
        [HttpGet]
        public async Task<IActionResult> Update(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            UpdateViewModel vm = new UpdateViewModel
            {
                UserId = userId,
                UserName = user.UserName,
                EmailAddress = user.Email,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(vm.UserId);
                    user.UserName = vm.UserName;
                    user.Email = vm.EmailAddress;

                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index", "Account");
                }
            }
            catch (Exception ex)
            {

            }
            return View(vm);
        }
        [HttpGet()]
        [AllowAnonymous()]
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (userId == null || token == null)
            {
                return RedirectToAction(nameof(Login));
            }
            ResetPasswordViewModel vm = new ResetPasswordViewModel
            {
                UserId = userId,
                Email = user.Email,
                Token = token
            };
            return View(vm);
        }

        [HttpPost()]
        [AllowAnonymous()]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");

            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByIdAsync(model.UserId);
                var result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(model.Token), model.NewPassword);

                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(user.UserName, model.NewPassword, false, lockoutOnFailure: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(model);
        }
    }
}
