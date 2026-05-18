using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;
using Welcome.Others;

namespace WelcomeExtended.Data
{
    class UserData
    {
        private List<User> _users = new List<User>();
        private int _nextId = 0;
        public UserData()
        {
            _nextId = 0;
            _users = new List<User>();
        }
        public void AddUser(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }
        public void DeleteUser(User user)
        {
            _users.Remove(user);
        }
        public bool ValidateUser(string name, string password)
        {
            foreach (var user in _users)
            {
                if (user.Names == name && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
        public bool ValidateUserLambda(string name, string password)
        {
            return _users.Any(user => user.Names == name && user.Password == password);
        }
        public bool ValidateUserLinq(string name, string password)
        {
            var result = (from user in _users
                          where user.Names == name && user.Password == password
                          select user).FirstOrDefault();
            return result != null;
        }
        //zadacha
        public User? GetUser(string name, string password)
        {
            return _users.FirstOrDefault(u => u.Names == name && u.Password == password);
        }
        public void SetActive(string name, DateTime expires)
        {
            var user = _users.FirstOrDefault(u => u.Names == name);
            if (user != null)
            {
                user.Expires = expires;
            }
        }
        public void AssignUserRole(string name, UserRolesEnum role)
        {
            var user = _users.FirstOrDefault(u => u.Names == name);
            if (user != null)
            {
                user.Role = role;
            }
        }
    }
}
