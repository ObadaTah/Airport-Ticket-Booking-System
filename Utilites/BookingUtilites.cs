
using Airport_Ticket_Booking_System.Bookings;
using Airport_Ticket_Booking_System.Flights;
using Airport_Ticket_Booking_System.Users;

namespace Airport_Ticket_Booking_System.Utilites;

public static class BookingUtilites
{
    public static void BookFlight(User user)
    {
        FlightUtilites.PrintFlights();
        Console.Write("Enter Flight ID / Enter F for filtering:");
        string? flightId = Console.ReadLine();

        if (String.IsNullOrEmpty(flightId))
        {
            GenericUtilites.PrintError("Invalid Flight ID");
            BookFlight(user);
        }

        if (flightId == "F")
        {
            FlightUtilites.FilterFlights();
            return;
        }

        if (!int.TryParse(flightId, out int flightIdInt))
        {
            GenericUtilites.PrintError("Invalid Flight ID");
            BookFlight(user);
        }
        Console.WriteLine("here");
        Dictionary<int, Flight> flights = FlightUtilites.GetFlights();
        if (!flights.ContainsKey(flightIdInt))
        {
            GenericUtilites.PrintError("Invalid Flight ID");
            BookFlight(user);
        }
        Booking booking = new(FileSystemUtilites.GetNextId("bookings.csv") , user, flights[flightIdInt]);
        FileSystemUtilites.WriteToFile("bookings.csv", Booking.ToCsv(booking));
        GenericUtilites.PrinSucc("Booked Successfully");

    }

    public static void UsersBookings(User user)
    {
        List<string> data = FileSystemUtilites.ReadFromFile("bookings.csv");
        foreach (string s in data)
        {
            try
            {
                Booking booking = Booking.FromCsv(s);
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
                GenericUtilites.PrintError(e.Message);
            }
        }
    }
}
