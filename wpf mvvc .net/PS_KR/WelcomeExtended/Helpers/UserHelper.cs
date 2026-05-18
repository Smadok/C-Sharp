using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;
using WelcomeExtended.Data;

namespace WelcomeExtended.Helpers
{
    static class UserHelper
    {
        //zadachi
        public static string ToUserString(this User user)
        {
            return $"User: {user.Names}\nID: {user.Id}\nRole: {user.Role}\nExpires: {user.Expires}";
        }
        public static bool ValidateCredentials(this UserData userData, string name, string password)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The name cannot be empty");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("The password cannot be empty");
            }
            return userData.ValidateUser(name, password);
        }
        public static User? GetUser(this UserData userData, string name, string password)
        {
            return userData.GetUser(name, password);
        }
    }
}
