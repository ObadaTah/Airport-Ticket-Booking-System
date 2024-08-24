namespace Airport_Ticket_Booking_System.Flights;

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

    public static string header = "flightNumber,price,destination,departureAirport,arrivalAirport,departureDate,class";

}
