using Airport_Ticket_Booking_System.Models.Enums;

namespace Airport_Ticket_Booking_System.Models;

public class User
{
    public static string header = "id,name,email,password,role";
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Passenger;
    public List<Booking> Bookings { get; set; } = new();

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Email: {Email}, Role: {Role}";
    }
}
