using Airport_Ticket_Booking_System.Utilites;
namespace Airport_Ticket_Booking_System.Flights;

public class Flight
{
    private int _flightNumber;
    public int FlightNumber
    { 
        get 
        { 
            return _flightNumber;
        } 
        set 
        {

            if (value < 0)
                throw new Exception("Flight Number must be positive");

            _flightNumber = value;
        }
    }

    private decimal _price;
    public decimal Price 
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
    private string _destination;
    public string Destination { get { return _destination; } 
        set 
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Destination cannot be empty");
            if (value.Length < 3)
                throw new Exception("Destination must be at least 3 characters long or more");
            _destination = value;
        } 
    }
    private string _departureAirport;
    public string DepartureAirport { get { return _departureAirport; } 
        set 
        { 
            if (string.IsNullOrEmpty(value))
                throw new Exception("Departure Airport cannot be empty");
            if (value.Length < 3)
                throw new Exception("Departure Airport must be at least 3 characters long or more");   
            _departureAirport = value;
        } 
    }
    private string _arrivalAirport;
    public string ArrivalAirport { get { return _arrivalAirport; } 
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Arrival Airport cannot be empty");
            if (value.Length < 3)
                throw new Exception("Arrival Airport must be at least 3 characters long or more");
            _arrivalAirport = value;
        } 
    } 
    private DateTime _departureDate;
    public DateTime DepartureDate {
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
    public FlightClass Class { get; set; } = FlightClass.Economy;

    public static string ToCsv(Flight flight)
    {
        return $"{flight.FlightNumber},{flight.Price},{flight.Destination},{flight.DepartureAirport},{flight.ArrivalAirport},{flight.DepartureDate},{flight.Class}";
    }

    public static Flight FromCsv(string csv, int line = 1)
    {
        string[] values = csv.Split(',');
        List<string> errors = [];

        Flight flight = new();

        try
            {
            flight.FlightNumber = int.Parse(values[0]);
        }
        catch (Exception e)
        {
            errors.Add( e.Message);
        }
        try
            {
            flight.Price = decimal.Parse(values[1]);
        }
        catch (Exception e)
        {
            errors.Add(e.Message);
        }
        try
            {
            flight.Destination = values[2];
        }
        catch (Exception e)
        {
            errors.Add(e.Message);
        }
        try
            {
            flight.DepartureAirport = values[3];
        }
        catch (Exception e)
        {
            errors.Add(e.Message);
        }
        try
            {
            flight.ArrivalAirport = values[4];
        }
        catch (Exception e)
        {
            errors.Add(e.Message);
        }
        try
            {
            flight.DepartureDate = DateTime.Parse(values[5]);
        }
        catch (Exception e)
        {
            errors.Add(e.Message);
        }
        try
            {
            flight.Class = (FlightClass)Enum.Parse(typeof(FlightClass), values[6]);
        }
        catch (Exception e)
        {
            errors.Add(e.Message);
        }
        if (errors.Any(x => x != null))
        {
            throw new Exception($"In line {line}:\n" + string.Join("\n", errors));
        }
        
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
