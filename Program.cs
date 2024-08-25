using Airport_Ticket_Booking_System.Bookings;
using Airport_Ticket_Booking_System.Utilites;
using Airport_Ticket_Booking_System.Users;
using Airport_Ticket_Booking_System.Flights;
namespace Airport_Ticket_Booking_System;

public class Program
{
    static void Main(string[] args)
    {

        string[] files = { "users.csv", "flights.csv", "bookings.csv" };
        string[] fileHeaders = { User.header, Flight.header, Booking.header };
        FileSystemUtilites.InitFiles(files, fileHeaders);

        User user = new();
        int choice = GenericUtilites.promptLoginRegister();

        switch (choice)
        {
            case 1:
                user = GenericUtilites.RequestLogin();
                Console.WriteLine("Login Successful");
                break;
            case 2:
                user = GenericUtilites.RequestRegister();
                Console.WriteLine("Register Successful");
                break;
            case 3:
                return;
        }

        while (true)
        {
            if (user.Role == UserRole.Manager)
            {
                int managerChoice = UserUtilites.PrinManagerMenu();
                switch (managerChoice)
                {
                    case 1:
                        // BookingUtilites.FilterBookings();
                        break;
                    case 2:
                        FlightUtilites.UploadFlights();
                        break;
                    case 3:
                        return;
                }
                if (user.Role == UserRole.Passenger)
                {
                    int passengerChoice = UserUtilites.PrintPassengerMenu();
                    switch (passengerChoice)
                    {
                        case 1:
                            // BookingsUtilites.BookFlight(user);
                            break;
                        case 2:
                            // BookingsUtilites.UsersBookings(user);
                            break;
                        case 3:
                            return;
                    }

                }

            }

        }
    }
}