using Airport_Ticket_Booking_System.Bookings;
using Airport_Ticket_Booking_System.Utilites;

namespace Airport_Ticket_Booking_System;

public class Program
{
    static void Main(string[] args)
    {

        string[] files = { "users.csv", "flights.csv", "bookings.csv" };
        FileSystemUtilites.InitFiles(files);

        int choice = GenericUtilites.promptLoginRegister();
        switch (choice)
        {
            case 1:
                GenericUtilites.RequestLogin();
                Console.WriteLine("Login Successful");
                break;
            case 2:
                GenericUtilites.RequestRegister();
                Console.WriteLine("Register Successful");
                break;
            case 3:
                break;
            default:
                break;

        }
        
    }
}