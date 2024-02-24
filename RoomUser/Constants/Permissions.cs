using System;
using System.Collections.Generic;
using System.Text;

namespace RoomUser.Permission
{
    public class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Premissions.{module}.Create",
                $"Premissions.{module}.Delete",
                $"Premissions.{module}.Update",
                $"Premissions.{module}.View",
                $"Premissions.{module}.Index",
                $"Premissions.{module}.BranchReport",
            };
        }
        public static class ApplicationPermission
        {

            //Account Register
            public const string AccountView = "Permissions.Account.View";
            public const string AccountRegister = "Permissions.Account.Register";
            public const string AccountIndex = "Permissions.Account.Index";
            public const string Delete = "Permissions.Products.Delete";


        }
    }
}
