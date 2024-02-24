using System;
using System.Collections.Generic;
using System.Text;

namespace RoomUser.Permission
{
    public enum Roles
    {
        SuperAdmin,
        Admin,
        User
    }

    public class CheckRole
    {
        private readonly string SuperAdmin = "SuperAdmin";
        private readonly string Admin = "Admin";
        private readonly string User = "User";

        public string IsSuperAdmin()
        {
            return SuperAdmin;
        }

        public string IsAdmin()
        {
            return Admin;
        }

        public string IsUser()
        {
            return User;
        }
        public string role { get; set; }
    }
}
