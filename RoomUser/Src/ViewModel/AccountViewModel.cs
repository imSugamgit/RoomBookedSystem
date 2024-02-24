using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoomUser.Src.ViewModel
{
    public class UserViewModel
    {
        public IEnumerable<IdentityUser> Users { get; set; }
    }
    public class AccountViewModel
    {
        public class RegisterViewModel
        {

            [Required(AllowEmptyStrings = false, ErrorMessage = "Full Name is requierd")]
            public string UserName { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID is requierd")]
            public string Email { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Password is requierd")]
            [DataType(DataType.Password)]
            [MinLength(6, ErrorMessage = "Need min 6 character")]
            public string Password { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is requierd")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Confirm Password should match with Password")]
            public string ConfirmPassword { get; set; }
            public string UserRole { get; set; }
            public long UserId { get; set; }
           
        }
        //REgister
        public class LoginViewModel
        {
            [Required]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember Me")]
            public bool RememberMe { get; set; }
        }//Login 
        public class RecoveryPasswordViewModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }//Forgot Password
        public class ChangePasswordViewModel
        {
            public string UserId { get; set; }

            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            //[DataType(DataType.Password)]
            //[Display(Name = "Old password")]
            //public string OldPassword { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            public string ConfirmPassword { get; set; }

            public string Token { get; set; }
        }
        //ChangePassword
        public class ManageUserViewModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [NotMapped()]
            public string UserName { get; set; }
        }
    }
    public class UpdateViewModel
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }
}
