
namespace Airport_Ticket_Booking_System.Utilites;

public static class UserUtilites
{
    public static int PrinManagerMenu()
    {
        Console.WriteLine("1. Filter Bookings");
        Console.WriteLine("2. Upload Flights");
        Console.WriteLine("3. Exit");
        return GenericUtilites.AskValidInt(3);
    }

    public static int PrintPassengerMenu()
    {
        Console.WriteLine("1. Book A Flight");
        Console.WriteLine("2. My Bookings");
        Console.WriteLine("3. Exit");
        return GenericUtilites.AskValidInt(3);
    }
}
