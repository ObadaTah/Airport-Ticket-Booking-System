using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Models.Enums;
using Airport_Ticket_Booking_System.Repositories;

namespace Airport_Ticket_Booking_System.Services;


public static class BookingService
{
    public static void BookFlight(User user, string flightIdStr)
    {
        if (string.IsNullOrEmpty(flightIdStr) || !int.TryParse(flightIdStr, out int flightIdInt))
            throw new ArgumentException("Invalid Flight ID");

        Dictionary<int, Flight> flights = FlightRepository.GetFlights();

        if (!flights.ContainsKey(flightIdInt))
        {
            throw new ArgumentException("Invalid Flight ID");
        }

        BookingRepository.SaveBooking(user, flightIdInt, flights);
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

    public static List<string> UsersBookings(User user)
    {
        return BookingRepository.GetBookings();
    }
}
