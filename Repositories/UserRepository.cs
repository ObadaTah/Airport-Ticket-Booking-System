using Airport_Ticket_Booking_System.Models;

namespace Airport_Ticket_Booking_System.Repositories;

public static class UserRepository
{

    public static List<string> GetUsers()
    {
        return FileSystemUtilities.ReadFromFile("users.csv");
    }

    public static void SaveUser(User newUser)
    {
        FileSystemUtilities.WriteToFile("users.csv", ToCsvFormat(newUser));
    }

    private static string ToCsvFormat(User user)
    {
        return $"{user.Id},{user.Name},{user.Email},{user.Password},{user.Role}";
    }
}
