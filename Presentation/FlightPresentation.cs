using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Models.Enums;
using Airport_Ticket_Booking_System.Repositories;
using Airport_Ticket_Booking_System.Services;

namespace Airport_Ticket_Booking_System.Presentation;

public static class FlightPresentation
{
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

    public static void PrintFlights()
    {
        List<string> data = FileSystemUtilities.ReadFromFile("flights.csv");
        foreach (string s in data)
        {
            Flight flight = FlightService.FromCsv(s);
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
    }

    public static void SearchFlights()
    {
        var choice = AskFilterSubject();
        var searchValue = AskForSearchValue();
        var flights = FlightService.FilterFlights(choice, searchValue);
        foreach (var flight in flights)
            PrintFlight(flight);
    }

    public static void UploadFlightsFromFile()
    {
        Console.WriteLine("Enter File Address:");
        string fileAddress = Console.ReadLine() ?? "";
        try
        {
            FlightService.UploadFlights(fileAddress);
        }
        catch (Exception e)
        {
            GenericUtilities.PrintError(e.Message);
        }
    }

    public static string AskForSearchValue()
    {
        Console.Write("Enter Search Value:");
        return Console.ReadLine() ?? "";
    }

    public static int AskFilterSubject()
    {
        Console.WriteLine("1. Flight Number");
        Console.WriteLine("2. Price");
        Console.WriteLine("3. Destination");
        Console.WriteLine("4. Departuer Airport");
        Console.WriteLine("5. Arrival Airport");
        Console.WriteLine("6. Departure Date");
        Console.WriteLine("7. Class");
        Console.Write("What field do you want to search:");

        int choice = GenericUtilities.AskValidInt(7);
        return choice;
    }
}
