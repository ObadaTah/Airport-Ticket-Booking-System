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

    public static Booking FromCsv(string csv)
    {
        string[] values = csv.Split(',');
        Booking booking = new(int.Parse(values[0]), User.GetUserById(int.Parse(values[2])), Flight.GetFlightByNumber(int.Parse(values[1])))
        {
            BookingDate = DateTime.Parse(values[3]),
            Status = (BookingStatus)Enum.Parse(typeof(BookingStatus), values[4])
        };
        return booking;
    }
    public override string ToString()
    {
        return $"Booking ID: {Id}\nFlight: {Flight.FlightNumber}\nUser: {User.Name}\nBooking Date: {BookingDate}\nStatus: {Status}";
    }

}
