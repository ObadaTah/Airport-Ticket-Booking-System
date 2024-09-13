using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Models.Enums;
using Airport_Ticket_Booking_System.Repositories;

namespace Airport_Ticket_Booking_System.Services;


public static class BookingService
{
    public static void BookFlight(User user, bool printFlights = true)
    {
        if (printFlights)
            FlightService.PrintFlights();
        Console.Write("Enter Flight ID / Enter F for filtering:");
        string? flightId = Console.ReadLine();

        if (string.IsNullOrEmpty(flightId))
        {
            GenericUtilities.PrintError("Invalid Flight ID");
            BookFlight(user, false);
            return;
        }

        if (flightId == "F")
        {
            FlightService.FilterFlights();
            BookFlight(user, false);
            return;

        }

        if (!int.TryParse(flightId, out int flightIdInt))
        {
            GenericUtilities.PrintError("Invalid Flight ID");
            BookFlight(user, false);
            return;

        }

        Dictionary<int, Flight> flights = FlightRepository.GetFlights();
        if (!flights.ContainsKey(flightIdInt))
        {
            GenericUtilities.PrintError("Invalid Flight ID");
            BookFlight(user, false);
            return;

        }

        BookingRepository.SaveBooking(user, flightIdInt, flights);
        GenericUtilities.PrinSucc("Booked Successfully");
    }

    public static Booking FromCsv(string csv)
    {
        string[] values = csv.Split(',');

        #region early fall check
        if (int.TryParse(values[0], out int bookingId))
            throw new Exception("Invalid Booking ID");

        if (int.TryParse(values[1], out int flightNumber))
            throw new Exception("Invalid Flight Number");

        if (int.TryParse(values[2], out int userId))
            throw new Exception("Invalid User ID");

        if (DateTime.TryParse(values[3], out DateTime bookingDate))
            throw new Exception("Invalid Booking Date");

        if (Enum.TryParse(values[4], out BookingStatus status))
            throw new Exception("Invalid Booking Status");
        #endregion

        Flight? flight = FlightService.GetFlightByNumber(flightNumber) ?? throw new Exception("Flight Not Found");
        Booking booking = new(bookingId, UserService.GetUserById(userId), flight, bookingDate, status);
        return booking;
    }

    public static string ToCsv(Booking booking)
    {
        return $"{booking.Id},{booking.Flight.FlightNumber},{booking.User.Id},{booking.BookingDate},{booking.Status}";
    }

    public static void UsersBookings(User user)
    {
        List<string> data = BookingRepository.GetBookings();
        foreach (string s in data)
        {
            try
            {
                Booking booking = FromCsv(s);
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
