
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

    private static void FilterFlights()
    {
        Console.WriteLine("1. Flight Number");
        Console.WriteLine("2. Price");
        Console.WriteLine("3. Destination");
        Console.WriteLine("4. Departuer Airport");
        Console.WriteLine("5. Arrival Airport");
        Console.WriteLine("6. Departure Date");
        Console.WriteLine("7. Class");
        Console.Write("What field do you want to search:");

        int choice = GenericUtilites.AskValidInt(7);
        Console.Write("Enter Search Value:");
        string? searchValue = Console.ReadLine();
        List<string> data = FileSystemUtilites.ReadFromFile("flights.csv");
        foreach (string s in data)
        {
            Flight flight = Flight.FromCsv(s);
            switch (choice)
            {
                case 1:
                    if (flight.FlightNumber.ToString() == searchValue)
                        Console.WriteLine(flight.ToString());
                    break;
                case 2:
                    if (flight.Price.ToString() == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
                case 3:
                    if (flight.Destination == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
                case 4:
                    if (flight.DepartureAirport == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
                case 5:
                    if (flight.ArrivalAirport == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
                case 6:
                    if (flight.DepartureDate.ToString() == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
                case 7:
                    if (flight.Class.ToString() == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
            }
        }
    }

    public static void UsersBookings(User user)
    {
        List<string> data = FileSystemUtilites.ReadFromFile("bookings.csv");
        foreach (string s in data)
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
    }
}
