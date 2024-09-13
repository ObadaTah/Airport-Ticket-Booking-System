using Airport_Ticket_Booking_System.Models.Enums;
namespace Airport_Ticket_Booking_System.Models;

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

    public override string ToString()
    {
        return $"Flight Number: {FlightNumber}\nPrice: {Price}\nDestination: {Destination}\nDeparture Airport: {DepartureAirport}\nArrival Airport: {ArrivalAirport}\nDeparture Date: {DepartureDate}\nClass: {Class}";
    }

    public static string header = "flightNumber,price,destination,departureAirport,arrivalAirport,departureDate,class";
}
