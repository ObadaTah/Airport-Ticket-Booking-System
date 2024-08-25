﻿namespace Airport_Ticket_Booking_System.Flights;

public class Flight
{
    public int FlightNumber { get; set; }
    public decimal Price { get; set; }
    public string Destination { get; set; } = string.Empty;
    public string DepartureAirport { get; set; } = string.Empty;
    public string ArrivalAirport { get; set; } = string.Empty;
    public DateTime DepartureDate { get; set; }
    public FlightClass Class { get; set; } = FlightClass.Economy;

    public static string ToCsv(Flight flight)
    {
        return $"{flight.FlightNumber},{flight.Price},{flight.Destination},{flight.DepartureAirport},{flight.ArrivalAirport},{flight.DepartureDate},{flight.Class}";
    }

    public static Flight FromCsv(string csv)
    {
        string[] values = csv.Split(',');
        Flight flight = new()
        {
            FlightNumber = int.Parse(values[0]),
            Price = decimal.Parse(values[1]),
            Destination = values[2],
            DepartureAirport = values[3],
            ArrivalAirport = values[4],
            DepartureDate = DateTime.Parse(values[5]),
            Class = (FlightClass)Enum.Parse(typeof(FlightClass), values[6])
        };
        return flight;
    }

    public static string header = "flightNumber,price,destination,departureAirport,arrivalAirport,departureDate,class";

}
