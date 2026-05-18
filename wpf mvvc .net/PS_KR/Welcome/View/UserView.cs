using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.ViewModel;

namespace Welcome.View
{
    public class UserView
    {
        private UserViewModel _viewModel;
        public UserView(UserViewModel viewModel)
        {
            _viewModel = viewModel;   
        }
        public void Display()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine($"User:{_viewModel.Names}");
            Console.WriteLine($"Role:{_viewModel.Role}");
        }
        public void DisplayError()
        {
            throw new Exception("Simulated error for demonstration!");
        }
    }
}
