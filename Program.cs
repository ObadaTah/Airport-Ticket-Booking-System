using Airport_Ticket_Booking_System.Bookings;
using Airport_Ticket_Booking_System.Utilities;
using Airport_Ticket_Booking_System.Users;
using Airport_Ticket_Booking_System.Flights;
namespace Airport_Ticket_Booking_System;

public class Program
{
    static void Main(string[] args)
    {

        string[] files = { "users.csv", "flights.csv", "bookings.csv" };
        string[] fileHeaders = { User.header, Flight.header, Booking.header };
        FileSystemUtilities.InitFiles(files, fileHeaders);

        User user = new();
        int choice = UserService.promptLoginRegister();

        switch (choice)
        {
            case 1:
                user = UserService.RequestLogin();
                GenericUtilities.PrinSucc("Login Successful");
                break;
            case 2:
                user = UserService.RequestRegister();
                GenericUtilities.PrinSucc("Register Successful");
                break;
            case 3:
                return;
        }

        while (true)
        {
            if (user.Role == UserRole.Manager)
            {
                int managerChoice = GenericUtilities.PrinManagerMenu();
                switch (managerChoice)
                {
                    case 1:
                        FlightService.FilterFlights();
                        break;
                    case 2:
                        FlightService.UploadFlights();
                        break;
                    case 3:
                        return;
                }

            }
            if (user.Role == UserRole.Passenger)
            {
                int passengerChoice = GenericUtilities.PrintPassengerMenu();
                switch (passengerChoice)
                {
                    case 1:
                        Console.WriteLine("aaaaaa");
                        BookingService.BookFlight(user);
                        break;
                    case 2:
                        BookingService.UsersBookings(user);
                        break;
                    case 3:
                        return;
                }

            }

        }
    }
}