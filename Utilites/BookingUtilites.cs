
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
            FilterFlights();
            return;
        }

        if (!int.TryParse(flightId, out int flightIdInt))
        {
            GenericUtilites.PrintError("Invalid Flight ID");
            BookFlight(user);
        }

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

    private static void FilterFlights()
    {
        throw new NotImplementedException();
    }
}
