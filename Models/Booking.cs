using Airport_Ticket_Booking_System.Models.Enums;

namespace Airport_Ticket_Booking_System.Models;

public class Booking
{
    public int Id { get; set; }
    public Flight Flight { get; set; }
    public User User { get; set; }
    public DateTime BookingDate { get; set; } = DateTime.Now;
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

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

    public override string ToString()
    {
        return $"Booking ID: {Id}\nFlight: {Flight.FlightNumber}\nUser: {User.Name}\nBooking Date: {BookingDate}\nStatus: {Status}";
    }

}
