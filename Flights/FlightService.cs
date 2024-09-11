using Airport_Ticket_Booking_System.Utilities;
using FluentValidation.Results;

namespace Airport_Ticket_Booking_System.Flights;

public static class FlightService
{
    public static void UploadFlights()
    {
        Console.WriteLine("Enter File Address:");
        string? fileAddress = Console.ReadLine();
        try
        {
            if (FileSystemUtilities.IsFileValid(fileAddress))
                FlightRepository.SaveFlightsFromFile(fileAddress);
        }
        catch (Exception e)
        {
            GenericUtilities.PrintError(e.Message);
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

        int choice = GenericUtilities.AskValidInt(7);
        Console.Write("Enter Search Value:");
        string? searchValue = Console.ReadLine();
        List<string> data = FileSystemUtilities.ReadFromFile("flights.csv");
        foreach (string s in data)
        {
            Flight flight = FlightService.FromCsv(s);
            switch (choice)
            {
                case 1:
                    if (flight.FlightNumber.ToString() == searchValue)
                        Console.WriteLine(flight.ToString());
                    break;
                case 2:
                    if (flight.Price.ToString() == searchValue)
                        FlightService.PrintFlight(flight);
                    break;
                case 3:
                    if (flight.Destination == searchValue)
                        FlightService.PrintFlight(flight);
                    break;
                case 4:
                    if (flight.DepartureAirport == searchValue)
                        FlightService.PrintFlight(flight);
                    break;
                case 5:
                    if (flight.ArrivalAirport == searchValue)
                        FlightService.PrintFlight(flight);
                    break;
                case 6:
                    if (flight.DepartureDate.ToString() == searchValue)
                        FlightService.PrintFlight(flight);
                    break;
                case 7:
                    if (flight.Class.ToString() == searchValue)
                        FlightService.PrintFlight(flight);
                    break;
            }
        }
    }

    public static Flight FromCsv(string csv, int line = 1)
    {
        string[] values = csv.Split(',');
        List<string> errors = [];

        ParseValues(values, errors, out var flightNumber, out var price, out var departureDate, out var @class);

        if (errors.Any(x => x != null))
        {
            throw new Exception($"In line {line}:\n" + string.Join("\n", errors));
        }

        Flight flight = new(flightNumber, price, values[2], values[3], values[4], departureDate, @class);
        FlightValidator validator = new();
        ValidationResult results = validator.Validate(flight);

        List<String> validationErrors = [];
        if (!results.IsValid)
        {
            foreach (var failure in results.Errors)
            {
                validationErrors.Add("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
            }

            throw new Exception($"In line {line}:\n" + string.Join("\n", validationErrors));
        }
        return flight;
    }

    public static Flight? GetFlightByNumber(int flightNumber)
    {
        List<string> flights = FileSystemUtilities.ReadFromFile("flights.csv");
        foreach (string s in flights)
        {
            Flight flight = FlightService.FromCsv(s);
            if (flight.FlightNumber == flightNumber)
                return flight;
        }
        return null;
    }

    private static void ParseValues(string[] values, List<string> errors, out int flightNumber, out decimal price, out DateTime departureDate, out FlightClass @class)
    {
        #region early fall check
        if (!int.TryParse(values[0], out flightNumber))
            errors.Add("Invalid Flight Number");
        if (!decimal.TryParse(values[1], out price))
            errors.Add("Invalid Price");
        if (string.IsNullOrEmpty(values[2]))
            errors.Add("Invalid Destination");
        if (string.IsNullOrEmpty(values[3]))
            errors.Add("Invalid Departure Airport");
        if (string.IsNullOrEmpty(values[4]))
            errors.Add("Invalid Arrival Airport");
        if (!DateTime.TryParse(values[5], out departureDate))
            errors.Add("Invalid Departure Date");
        if (!Enum.TryParse(values[6], out @class))
            errors.Add("Invalid Class");
        #endregion
    }

    public static string ToCsv(Flight flight)
    {
        return $"{flight.FlightNumber},{flight.Price},{flight.Destination},{flight.DepartureAirport},{flight.ArrivalAirport},{flight.DepartureDate},{flight.Class}";
    }
}
