
using Airport_Ticket_Booking_System.Bookings;
using Airport_Ticket_Booking_System.Flights;
using Airport_Ticket_Booking_System.Users;
using System.IO;

namespace Airport_Ticket_Booking_System.Utilites;

public static class BookingUtilites
{
    public static void BookFlight(User user)
    {
        FlightUtilites.PrintFlights();
        Console.Write("Enter Flight ID / Enter F for filtering:");
        string? flightId = Console.ReadLine();

        if (String.IsNullOrEmpty(flightId))
        {
            GenericUtilites.PrintError("Invalid Flight ID");
            BookFlight(user);
        }

        if (flightId == "F")
        {
            FilterFlights();
            return;
        }

        if (!int.TryParse(flightId, out int flightIdInt))
        {
            GenericUtilites.PrintError("Invalid Flight ID");
            BookFlight(user);
        }

        Dictionary<int, Flight> flights = FlightUtilites.GetFlights();
        if (!flights.ContainsKey(flightIdInt))
        {
            GenericUtilites.PrintError("Invalid Flight ID");
            BookFlight(user);
        }
        Booking booking = new(FileSystemUtilites.GetNextId("bookings.csv") , user, flights[flightIdInt]);
        FileSystemUtilites.WriteToFile("bookings.csv", Booking.ToCsv(booking));
        GenericUtilites.PrinSucc("Booked Successfully");

    }

    private static void FilterFlights()
    {
        throw new NotImplementedException();
    }

    public static void UsersBookings(User user)
    {
        List<string> data = FileSystemUtilites.ReadFromFile("bookings.csv");
        List<Booking> usersBookings = [];
        foreach (string s in data)
        {
            Booking booking = Booking.FromCsv(s);
            if (booking.User.Email == user.Email)
            {
                usersBookings.Add(booking);
                if (booking.Flight.Class == FlightClass.Economy)
                    Console.ForegroundColor = ConsoleColor.Blue;
                if (booking.Flight.Class == FlightClass.FirstClass)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (booking.Flight.Class == FlightClass.Business)
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("=====================================================");
                Console.WriteLine($". {usersBookings.Count}");
                Console.WriteLine(booking.ToString());
                Console.WriteLine("=====================================================");
                Console.ResetColor();
            }
        }

        Console.WriteLine("Choose a Booking to modify/delete");
        int choice = GenericUtilites.AskValidInt(usersBookings.Count);
        Console.WriteLine("1. Modify");
        Console.WriteLine("2. Delete");
        Console.WriteLine("3. Cancel");
        int action = GenericUtilites.AskValidInt(3);
        if (action == 1)
        {
            Console.WriteLine("1. Cancel");
            Console.WriteLine("2. Confirm");
            int d = GenericUtilites.AskValidInt(2);
            ModifyBooking(usersBookings[choice - 1], d);
        }
        else if (action == 2) 
        {
            DeleteBooking(usersBookings[choice - 1]);
        }
        else
        {
            return;
        }

    }

    private static void DeleteBooking(Booking booking)
    {
        List<string> _ = [];
        string path = Path.Combine(FileSystemUtilites.GetPath(), "storage");
        string file = Path.Combine(path, "bookings.csv");
        try
        {
            using (var reader = new StreamReader(file))
            {

                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values[0] == booking.Id.ToString())
                        continue;

                    _.Add(line);

                }

            }

            File.WriteAllLines(file, _);

        }
        catch (Exception f)
        {
            Console.WriteLine(f);

        }
        GenericUtilites.PrinSucc("Deleted Successfully");
    }

    private static void ModifyBooking(Booking booking, int d)
    {
        List<string> _ = [];
        string path = Path.Combine(FileSystemUtilites.GetPath(), "storage");
        string file = Path.Combine(path, "bookings.csv");
        try
        {
            using (var reader = new StreamReader(file))
            {

                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values[0] == booking.Id.ToString())
                        values[4] = d == 1 ? BookingStatus.Cancelled.ToString() : BookingStatus.Confirmed.ToString();
                    _.Add(string.Join(',', values));
                }

            }

            File.WriteAllLines(file, _);

        }
        catch (Exception f)
        {
            Console.WriteLine(f);

        }
        GenericUtilites.PrinSucc("Deleted Successfully");
    }
}
