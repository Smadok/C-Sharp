using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using Uber;

class Program
{
    static void Main(string[] args)
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Uber");

        var usersCollection = database.GetCollection<User>("users");
        var driversCollection = database.GetCollection<Driver>("drivers");
        var ridesCollection = database.GetCollection<Ride>("rides");

        while (true)
        {
            Console.WriteLine("MENU");
            Console.WriteLine("1. Add Ride");
            Console.WriteLine("2. Show All Rides");
            Console.WriteLine("3. Update Ride Status");
            Console.WriteLine("4. Delete Ride");
            Console.WriteLine("5. Revenue per Driver");
            Console.WriteLine("6. CompletedRidesPerDriver");
            Console.WriteLine("0. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddRide(ridesCollection, usersCollection, driversCollection);
                    break;

                case "2":
                    ShowRides(ridesCollection);
                    break;

                case "3":
                    UpdateRide(ridesCollection);
                    break;

                case "4":
                    DeleteRide(ridesCollection);
                    break;

                case "5":
                    RevenuePerDriver(ridesCollection);
                    break;

                case "6":
                    CompletedRidesPerDriver(ridesCollection);
                    break;

                case "0":
                    return;
            }
        }
    }

    static void AddRide(
    IMongoCollection<Ride> rides,
    IMongoCollection<User> users,
    IMongoCollection<Driver> drivers)
    {
        var allUsers = users.Find(_ => true).ToList();

        Console.WriteLine("\nSelect a user:");
        foreach (var u in allUsers)
        {
            Console.WriteLine($"{u.Id} - {u.Name}");
        }

        Console.Write("Enter User Id: ");
        var userInput = Console.ReadLine();

        if (!ObjectId.TryParse(userInput, out var userId))
        {
            Console.WriteLine("Invalid User Id!");
            return;
        }

        var user = users.Find(u => u.Id == userId).FirstOrDefault();

        if (user == null)
        {
            Console.WriteLine("User not found!");
            return;
        }

        var availableDrivers = drivers.Find(d => d.IsAvailable).ToList();

        Console.WriteLine("\nSelect a driver:");
        foreach (var d in availableDrivers)
        {
            Console.WriteLine($"{d.Id} - {d.Name}");
        }

        Console.Write("Enter Driver Id: ");
        var driverInput = Console.ReadLine();

        if (!ObjectId.TryParse(driverInput, out var driverId))
        {
            Console.WriteLine("Invalid Driver Id!");
            return;
        }

        var driver = drivers.Find(d => d.Id == driverId && d.IsAvailable).FirstOrDefault();

        if (driver == null)
        {
            Console.WriteLine("Driver not found or not available!");
            return;
        }

        var ride = new Ride
        {
            UserId = user.Id,
            DriverId = driver.Id,
            Price = new Random().Next(5, 50),
            Status = "completed",
            RequestedAt = DateTime.Now.AddMinutes(-20),
            CompletedAt = DateTime.Now
        };

        rides.InsertOne(ride);

        Console.WriteLine($"Ride created: {user.Name} -> {driver.Name}");
    }

    static void ShowRides(IMongoCollection<Ride> collection)
    {
        var rides = collection.Find(_ => true).ToList();

        foreach (var r in rides)
        {
            Console.WriteLine($"ID: {r.Id} | Price: {r.Price} | Status: {r.Status}");
        }
    }

    static void UpdateRide(IMongoCollection<Ride> collection)
    {
        Console.Write("Enter Ride ID: ");
        var id = Console.ReadLine();

        var filter = Builders<Ride>.Filter.Eq("_id", ObjectId.Parse(id));
        var update = Builders<Ride>.Update.Set("Status", "completed");

        collection.UpdateOne(filter, update);

        Console.WriteLine("Updated!");
    }

    static void DeleteRide(IMongoCollection<Ride> collection)
    {
        Console.Write("Enter Ride ID: ");
        var id = Console.ReadLine();

        var filter = Builders<Ride>.Filter.Eq("_id", ObjectId.Parse(id));
        collection.DeleteOne(filter);

        Console.WriteLine("Deleted!");
    }

    static void RevenuePerDriver(IMongoCollection<Ride> collection)
    {
        var result = collection.Aggregate()
            .Match(r => r.Status == "completed")
            .Group(r => r.DriverId, g => new
            {
                DriverId = g.Key,
                Total = g.Sum(x => x.Price)
            })
            .SortByDescending(x => x.Total)
            .ToList();

        foreach (var r in result)
        {
            Console.WriteLine($"Driver: {r.DriverId} | Revenue: {r.Total}");
        }
    }

    static void CompletedRidesPerDriver(IMongoCollection<Ride> collection)
    {
        var result = collection.Aggregate()
            .Match(r => r.Status == "completed")
            .Group(r => r.DriverId, g => new
            {
                DriverId = g.Key,
                TotalRides = g.Count()
            })
            .SortByDescending(x => x.TotalRides)
            .ToList();

        Console.WriteLine("COMPLETED RIDES PER DRIVER");

        foreach (var r in result)
        {
            Console.WriteLine($"Driver: {r.DriverId} | Rides: {r.TotalRides}");
        }
    }
}