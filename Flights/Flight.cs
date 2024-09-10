using Airport_Ticket_Booking_System.Utilites;
using FluentValidation.Results;

namespace Airport_Ticket_Booking_System.Flights;

public class Flight(int flightNumber, decimal price, string destination, string departureAirport, string arrivalAirport,
              DateTime departureDate, FlightClass @class)
{
    public int FlightNumber { get; set; } = flightNumber;

    public decimal Price { get; set; } = price;

    public string Destination { get; set; } = destination;

    public string DepartureAirport { get; set; } = departureAirport;

    public string ArrivalAirport { get; set; } = arrivalAirport;

    public DateTime DepartureDate { get; set; } = departureDate;

    public FlightClass Class { get; set; } = @class;

    public static string ToCsv(Flight flight)
    {
        return $"{flight.FlightNumber},{flight.Price},{flight.Destination},{flight.DepartureAirport},{flight.ArrivalAirport},{flight.DepartureDate},{flight.Class}";
    }

    public static Flight FromCsv(string csv, int line = 1)
    {
        string[] values = csv.Split(',');
        List<string> errors = [];

        ParseVavlues(values, errors, out var flightNumber, out var price, out var departureDate, out var @class);

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

    private static void ParseVavlues(string[] values, List<string> errors, out int flightNumber, out decimal price, out DateTime departureDate, out FlightClass @class)
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

    public override string ToString()
    {
        return $"Flight Number: {FlightNumber}\nPrice: {Price}\nDestination: {Destination}\nDeparture Airport: {DepartureAirport}\nArrival Airport: {ArrivalAirport}\nDeparture Date: {DepartureDate}\nClass: {Class}";
    }

    public static string header = "flightNumber,price,destination,departureAirport,arrivalAirport,departureDate,class";

    public static Flight? GetFlightByNumber(int flightNumber)
    {
        List<string> flights = FileSystemUtilites.ReadFromFile("flights.csv");
        foreach (string s in flights)
        {
            Flight flight = Flight.FromCsv(s);
            if (flight.FlightNumber == flightNumber)
                return flight;
        }
        return null;
    }
}
