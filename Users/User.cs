using Airport_Ticket_Booking_System.Bookings;

namespace Airport_Ticket_Booking_System.Users;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Passenger;
    public List<Booking> Bookings { get; set; } = new();


    public static Boolean CheckUserCredintials(string? email, string? password)
    {

        if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            return false;
        return true;
    }

    public static Boolean UserExists(string? email)
    {
        if (String.IsNullOrEmpty(email))
            return false;
        return true;
    }

    public static Boolean RegisterUser(string? name, string? email, string? password)
    {
        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            return false;
        // check user already exists
        // if exists return false

        return true;
    }
}
