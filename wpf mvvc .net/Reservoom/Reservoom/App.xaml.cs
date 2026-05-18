using Reservoom.Exceptions;
using Reservoom.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Reservoom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Hotel hotel = new Hotel("Reservoom");
            try { 
            
            hotel.MakeReservation(new Reservation(
                new RoomID(1, 3),
                "username",
                new DateTime(2023, 1, 1),
                new DateTime(2023, 1, 2)));

            hotel.MakeReservation(new Reservation(
                new RoomID(1, 3),
                "username",
                new DateTime(2023, 1, 1),
                new DateTime(2023, 1, 4)));
            }
            catch (ReservationConflictException ex)
            {
                
            }
            IEnumerable<Reservation> reservations = hotel.GetAllReservations();
            base.OnStartup(e);
        }
    }

}
