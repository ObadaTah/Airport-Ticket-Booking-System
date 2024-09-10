using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Airport_Ticket_Booking_System.Flights;

public class FlightValidator : AbstractValidator<Flight>
{
    public FlightValidator()
    {
        RuleFor(flight => flight.FlightNumber).NotEmpty();
        RuleFor(flight => flight.Price).NotEmpty().GreaterThan(0);
        RuleFor(flight => flight.Destination).NotEmpty().MinimumLength(3);
        RuleFor(flight => flight.DepartureAirport).NotEmpty().MinimumLength(3);
        RuleFor(flight => flight.ArrivalAirport).NotEmpty().MinimumLength(3);
        RuleFor(flight => flight.DepartureDate).NotEmpty().Must((date) => date>DateTime.Now);
    }
}
