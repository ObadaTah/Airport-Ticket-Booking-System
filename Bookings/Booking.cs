using Airport_Ticket_Booking_System.Flights;
using Airport_Ticket_Booking_System.Users;

namespace Airport_Ticket_Booking_System.Bookings;

public class Booking
{
    public int Id { get; set; }
    public Flight Flight { get; set; }
    public User User { get; set; }
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

}
