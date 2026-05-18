using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;
using Welcome.Others;

namespace Welcome.ViewModel
{
    public class UserViewModel
    {
        private string _password;
        private User _user;
        public UserViewModel(User user)
        {
            _user = user;
        }
        public string Names
        {
            get { return _user.Names; }
            set { _user.Names = value; }
        }

        public string Password
        {
            get { return SimpleCipher.Decrypt(_password); }
            set { _password = SimpleCipher.Encrypt(value); }
        }

        public UserRolesEnum Role
        {
            get { return _user.Role; }
            set { _user.Role = value; }
        }
        public static class SimpleCipher
        {
            private static int shift = 3;

            public static string Encrypt(string text)
            {
                return new string(text.Select(c => (char)(c + shift)).ToArray());
            }

            public static string Decrypt(string text)
            {
                return new string(text.Select(c => (char)(c - shift)).ToArray());
            }
        }
    }

}
