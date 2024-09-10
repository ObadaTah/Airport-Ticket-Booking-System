using Airport_Ticket_Booking_System.Flights;
using Airport_Ticket_Booking_System.Users;

namespace Airport_Ticket_Booking_System.Bookings;

public class Booking
{
    public int Id { get; set; }
    public Flight Flight { get; set; }
    public User User { get; set; }
    public DateTime BookingDate { get; set; } = DateTime.Now;
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    public static string ToCsv(Booking booking)
    {
        return $"{booking.Id},{booking.Flight.FlightNumber},{booking.User.Id},{booking.BookingDate},{booking.Status}";
    }

    public static string header = "bookingId,flightNumber,userId,bookingDate,status";

    public Booking(int id, User user, Flight flight)
    {
        Id = id;
        User = user;
        Flight = flight;
    }

    public Booking(int id, User user, Flight flight, DateTime bookingDate, BookingStatus status)
    {
        Id = id;
        Flight = flight;
        User = user;
        BookingDate = bookingDate;
        Status = status;
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

        Flight? flight = Flight.GetFlightByNumber(flightNumber) ?? throw new Exception("Flight Not Found");
        Booking booking = new(bookingId, User.GetUserById(userId), flight, bookingDate, status);
        return booking;
    }
    public override string ToString()
    {
        return $"Booking ID: {Id}\nFlight: {Flight.FlightNumber}\nUser: {User.Name}\nBooking Date: {BookingDate}\nStatus: {Status}";
    }

}
