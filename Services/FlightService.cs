using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Models.Enums;
using Airport_Ticket_Booking_System.Repositories;
using FluentValidation.Results;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Airport_Ticket_Booking_System.Services;

public static class FlightService
{
    public static void UploadFlights(string fileAddress)
    {
        if (!FileSystemUtilities.IsFileValid(fileAddress))
            return;
            FlightRepository.SaveFlightsFromFile(fileAddress);
    }

    public static List<Flight> FilterFlights(int choice, string searchValue)
    {
        var data = FileSystemUtilities.ReadFromFile("flights.csv");

        return FindFlights(choice, searchValue, data);
    }

    private static List<Flight> FindFlights(int choice, string? searchValue, List<string> data)
    {
        List<Flight> flights = new();
        foreach (string s in data)
        {
            Flight flight = FromCsv(s);
            switch (choice)
            {
                case 1:
                    if (flight.FlightNumber.ToString() == searchValue)
                        flights.Add(flight);
                    break;
                case 2:
                    if (flight.Price.ToString() == searchValue)
                        flights.Add(flight);
                    break;
                case 3:
                    if (flight.Destination == searchValue)
                        flights.Add(flight);
                    break;
                case 4:
                    if (flight.DepartureAirport == searchValue)
                        flights.Add(flight);
                    break;
                case 5:
                    if (flight.ArrivalAirport == searchValue)
                        flights.Add(flight);
                    break;
                case 6:
                    if (flight.DepartureDate.ToString() == searchValue)
                        flights.Add(flight);
                    break;
                case 7:
                    if (flight.Class.ToString() == searchValue)
                        flights.Add(flight);
                    break;
            }
        }
        return flights;
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

        List<string> validationErrors = [];
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
            Flight flight = FromCsv(s);
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
