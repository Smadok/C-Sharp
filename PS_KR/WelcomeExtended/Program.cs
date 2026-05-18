using Microsoft.Extensions.Logging;
using Welcome.Model;
using WelcomeExtended.Others;
using Welcome.ViewModel;
using Welcome.View;
using WelcomeExtended.Helpers;
using WelcomeExtended.Data;
namespace WelcomeExtended
{
    class Program
    {
        private static readonly ILogger _logger = LoggerHelper.GetLogger("Program");

        static void Main(string[] args)
        {
            try
            {
                UserData userData = new UserData();
                User studentUser = new User()
                {
                    Names = "Student1",
                    Password = "123",
                    Role = Welcome.Others.UserRolesEnum.STUDENT
                };
                userData.AddUser(studentUser);
                userData.AddUser(new User
                {
                    Names = "Teacher",
                    Password = "1234",
                    Role = Welcome.Others.UserRolesEnum.PROFESSOR
                });
                userData.AddUser(new User
                {
                    Names = "Admin",
                    Password = "12345",
                    Role = Welcome.Others.UserRolesEnum.ADMIN
                });

                //zadacha
                Console.Write("Enter name: ");
                string name = Console.ReadLine() ?? "";
                Console.Write("Enter password: ");
                string password = Console.ReadLine() ?? "";

                bool isValid = userData.ValidateCredentials(name, password);
                if (isValid)
                {
                    var user = userData.GetUser(name, password);
                    Console.WriteLine(user?.ToUserString());
                }
                else
                {
                    throw new Exception("User not found");
                }

            }
            catch (Exception ex)
            {
                // Use delegate for error handling
                ActionOnError errorHandler = LogError;
                errorHandler(ex.Message);
            }
            finally
            {
                // System log
                _logger.LogInformation("Application session ended");
                Console.WriteLine("Executed in any case!");
                Console.ReadKey();
            }
        }

        private static void LogError(string errorMessage)
        {
            // Log error using the logger
            _logger.LogError(errorMessage);

            // Additional error handling logic can be added here
            Console.WriteLine($"Critical error occurred: {errorMessage}");
        }
    }
}
