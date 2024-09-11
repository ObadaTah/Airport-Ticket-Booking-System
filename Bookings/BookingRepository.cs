using Airport_Ticket_Booking_System.Flights;
using Airport_Ticket_Booking_System.Users;
using Airport_Ticket_Booking_System.Utilities;

namespace Airport_Ticket_Booking_System.Bookings;

public static class BookingRepository
{

    public static List<string> GetBookings()
    {
        return FileSystemUtilities.ReadFromFile("bookings.csv");
    }

    public static void SaveBooking(User user, int flightIdInt, Dictionary<int, Flight> flights)
    {
        Booking booking = new(FileSystemUtilities.GetNextId("bookings.csv"), user, flights[flightIdInt]);
        FileSystemUtilities.WriteToFile("bookings.csv", BookingService.ToCsv(booking));
    }
}
