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

        User user;
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
                break;
            default:
                break;

        }
        
    }
}