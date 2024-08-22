namespace Airport_Ticket_Booking_System.Flights;

public class Flight
{
    public decimal Price { get; set; }
    public string Destination { get; set; } = string.Empty;
    public string Departure { get; set; } = string.Empty;
    public DateTime DepartureDate { get; set; }
    public string DepartureAirport { get; set; } = string.Empty;
    public string ArrivalAirport { get; set; } = string.Empty;
    public FlightClass Class { get; set; } = FlightClass.Economy;
}
