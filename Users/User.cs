using Airport_Ticket_Booking_System.Bookings;

namespace Airport_Ticket_Booking_System.Users;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Passenger;

    public List<Booking> Bookings { get; set; } = new();

}
