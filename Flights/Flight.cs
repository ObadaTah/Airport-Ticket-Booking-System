using Airport_Ticket_Booking_System.Utilites;
namespace Airport_Ticket_Booking_System.Flights;

public class Flight
{
    private int _flightNumber;
    public required int FlightNumber
    { 
        get 
        { 
            return _flightNumber;
        } 
        set 
        {

            if (Flight.GetFlightByNumber(value) != null)
                throw new Exception("Flight Number already exists");
            _flightNumber = value;
        }
    }

    private decimal _price;
    public required decimal Price 
    {
        get 
        {
            return _price;
        }
            
        set
        { 
            if (value < 0) 
                throw new Exception("Price must be positive");
            _price = value;
        }
    }
    public required string Destination { get; set; } = string.Empty;
    public required string DepartureAirport { get; set; } = string.Empty;
    public required string ArrivalAirport { get; set; } = string.Empty;
    private DateTime _departureDate;
    public required DateTime DepartureDate {
        get
        {
            return _departureDate;
        }
        set
        {
            if (value < DateTime.Now)
                throw new Exception("Departure Date must be in the future");
            _departureDate = value;
        }
    }
    public required FlightClass Class { get; set; } = FlightClass.Economy;

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
