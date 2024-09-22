using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Models.Enums;
using Airport_Ticket_Booking_System.Services;

namespace Airport_Ticket_Booking_System.Presentation;

public static class BookingPresentation
{
    public static void BookFlight(User user)
    {
        while (true)
        {
            FlightPresentation.PrintFlights();

            Console.Write("Enter Flight ID / Enter F for filtering:");
            string userAnswer = Console.ReadLine() ?? "";

            if (userAnswer == "F")
            {
                FlightPresentation.SearchFlights();
                continue;
            }
            try
            {
                BookingService.BookFlight(user, userAnswer);
            }
            catch (ArgumentException e)
            {
                GenericUtilities.PrintError(e.Message);
                continue;
            }
            GenericUtilities.PrinSucc("Booked Successfully");
            break;
        }
    }

    public static void GetUserBookings(User user)
    {
        var bookings = BookingService.UsersBookings(user);
        foreach (string s in bookings)
        {
            try
            {
                Booking booking = BookingService.FromCsv(s);
                if (booking.User.Email == user.Email)
                {
                    if (booking.Flight.Class == FlightClass.Economy)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    if (booking.Flight.Class == FlightClass.FirstClass)
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    if (booking.Flight.Class == FlightClass.Business)
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("=====================================================");
                    Console.WriteLine(booking.ToString());
                    Console.WriteLine("=====================================================");
                    Console.ResetColor();
                }
            }
            catch (Exception e)
            {
                GenericUtilities.PrintError(e.Message);
            }
        }
    }
}
