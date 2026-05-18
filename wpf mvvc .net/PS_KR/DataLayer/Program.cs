using DataLayer.Database;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



namespace DataLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            var logger = loggerFactory.CreateLogger<Program>();

            while (true)
            {
                Console.WriteLine("1. List Users\n2. Add User\n3. Delete User\n4. Exit");
                var choice = Console.ReadLine();

                using (var context = new DatabaseContext())
                {
                    
                    context.Database.EnsureCreated();

                    try
                    {
                        switch (choice)
                        {
                            case "1":
                                var users = context.Users.ToList();
                                foreach (var user in users)
                                {
                                    Console.WriteLine($"{user.Names} ({user.Role})");
                                }
                                logger.LogInformation("Listed all users.");
                                break;

                            case "2":
                                Console.Write("Enter name: ");
                                var name = Console.ReadLine();
                                Console.Write("Enter password: ");
                                var password = Console.ReadLine();
                                Console.Write("Enter email: ");
                                var email = Console.ReadLine();

                                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
                                {
                                    logger.LogWarning("Name, password, or email cannot be null or empty.");
                                    break;
                                }

                                context.Users.Add(new DatabaseUser
                                {
                                    Names = name,
                                    Password = password,
                                    Email = email,
                                    Role = Welcome.Others.UserRolesEnum.STUDENT,
                                    Expires = DateTime.Now.AddYears(1)
                                });
                                context.SaveChanges();
                                logger.LogInformation($"Added user: {name}");
                                break;

                            case "3":
                                Console.Write("Enter name to delete: ");
                                var deleteName = Console.ReadLine();
                                var userToDelete = context.Users.FirstOrDefault(u => u.Names == deleteName);
                                if (userToDelete != null)
                                {
                                    context.Users.Remove(userToDelete);
                                    context.SaveChanges();
                                    logger.LogInformation($"Deleted user: {deleteName}");
                                }
                                else
                                {
                                    logger.LogWarning($"User with name {deleteName} not found.");
                                }
                                break;

                            case "4":
                                return;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred while processing the request.");
                    }
                }
            }
        }
    }
}