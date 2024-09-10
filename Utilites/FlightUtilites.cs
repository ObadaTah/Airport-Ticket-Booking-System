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
                for (int i = 0; i < data.Count; i++)
                {
                    string s = data[i];
                    try
                    {
                        Flight flight = Flight.FromCsv(s, i+2);
                        FileSystemUtilites.WriteToFile("flights.csv", Flight.ToCsv(flight));
                    }
                    catch (Exception e)
                    {
                        GenericUtilites.PrintError(e.Message);
                        continue;
                    }
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

    public static void FilterFlights()
    {
        Console.WriteLine("1. Flight Number");
        Console.WriteLine("2. Price");
        Console.WriteLine("3. Destination");
        Console.WriteLine("4. Departuer Airport");
        Console.WriteLine("5. Arrival Airport");
        Console.WriteLine("6. Departure Date");
        Console.WriteLine("7. Class");
        Console.Write("What field do you want to search:");

        int choice = GenericUtilites.AskValidInt(7);
        Console.Write("Enter Search Value:");
        string? searchValue = Console.ReadLine();
        List<string> data = FileSystemUtilites.ReadFromFile("flights.csv");
        foreach (string s in data)
        {
            Flight flight = Flight.FromCsv(s);
            switch (choice)
            {
                case 1:
                    if (flight.FlightNumber.ToString() == searchValue)
                        Console.WriteLine(flight.ToString());
                    break;
                case 2:
                    if (flight.Price.ToString() == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
                case 3:
                    if (flight.Destination == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
                case 4:
                    if (flight.DepartureAirport == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
                case 5:
                    if (flight.ArrivalAirport == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
                case 6:
                    if (flight.DepartureDate.ToString() == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
                case 7:
                    if (flight.Class.ToString() == searchValue)
                        FlightUtilites.PrintFlight(flight);
                    break;
            }
        }
    }
}