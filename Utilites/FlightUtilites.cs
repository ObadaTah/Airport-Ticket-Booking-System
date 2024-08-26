using Airport_Ticket_Booking_System.Flights;

namespace Airport_Ticket_Booking_System.Utilites;

public static class FlightUtilites
{
    public static void UploadFlights()
    {
        Console.WriteLine("Enter File Address:");
        string? fileAddress = Console.ReadLine();
        try
        {
            if (IsFileValid(fileAddress))
            {
                List<string> data = FileSystemUtilites.ReadFromFile(fileAddress!);
                foreach (string s in data)
                {
                    Flight flight = Flight.FromCsv(s);
                    FileSystemUtilites.WriteToFile("flights.csv", Flight.ToCsv(flight));
                }
            }
        }
        catch (Exception e)
        {
            GenericUtilites.PrintError(e.Message);
        }

    }
    public static Boolean IsFileValid(string? fileAddress)
    {
        if (String.IsNullOrEmpty(fileAddress))
            throw new Exception("Invalid File Address");
        if (!File.Exists(fileAddress))
        {
            Console.WriteLine(fileAddress);
            throw new Exception("File Doesn't Exist");
        }
        return true;
    }

    public static void PrintFlights()
    {
        List<string> data = FileSystemUtilites.ReadFromFile("flights.csv");
        foreach (string s in data)
        {
            Flight flight = Flight.FromCsv(s);
            if(flight.Class == FlightClass.Economy)
                Console.ForegroundColor = ConsoleColor.Blue;
            if (flight.Class == FlightClass.FirstClass)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (flight.Class == FlightClass.Business)
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("=====================================================");
            Console.WriteLine(flight.ToString());
            Console.WriteLine("=====================================================");
            Console.ResetColor();
        }
    }

    public static void PrintFlight(Flight flight)
    {
        if (flight.Class == FlightClass.Economy)
            Console.ForegroundColor = ConsoleColor.Blue;
        if (flight.Class == FlightClass.FirstClass)
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        if (flight.Class == FlightClass.Business)
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("=====================================================");
        Console.WriteLine(flight.ToString());
        Console.WriteLine("=====================================================");
        Console.ResetColor();
    }
    public static Dictionary<int, Flight> GetFlights()
    {
        List<string> data = FileSystemUtilites.ReadFromFile("flights.csv");
        Dictionary<int, Flight> flights = [];
        foreach (string s in data)
        {
            Flight flight = Flight.FromCsv(s);
            flights.Add(flight.FlightNumber, flight);
        }
        return flights;
    }
}