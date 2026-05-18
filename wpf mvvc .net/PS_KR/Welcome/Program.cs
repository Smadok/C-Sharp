using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;

namespace Welcome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User
            {
                Names = "John Doe",
                Password = "12345",
                Role = Others.UserRolesEnum.STUDENT
            };

            UserViewModel userViewModel = new UserViewModel(user);
            UserView userView = new UserView(userViewModel);
            userView.Display();
            Console.ReadKey();

        }
    }
}
