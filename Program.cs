using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Models.Enums;
using Airport_Ticket_Booking_System.Presentation;
using Airport_Ticket_Booking_System.Repositories;

namespace Airport_Ticket_Booking_System;

public class Program
{
    static void Main(string[] args)
    {

        string[] files = { "users.csv", "flights.csv", "bookings.csv" };
        string[] fileHeaders = { User.header, Flight.header, Booking.header };
        FileSystemUtilities.InitFiles(files, fileHeaders);

        User user = UserPresentation.GetUserByRegisterOrLogin();

        while (true)
        {
            if (user.Role == UserRole.Manager)
            {
                int managerChoice = GenericUtilities.PrinManagerMenu();
                switch (managerChoice)
                {
                    case 1:
                        FlightPresentation.SearchFlights();
                        break;
                    case 2:
                        FlightPresentation.UploadFlightsFromFile();
                        break;
                    case 3:
                        return;
                }

            }
            if (user.Role == UserRole.Passenger)
            {
                int passengerChoice = GenericUtilities.PrintPassengerMenu();
                switch (passengerChoice)
                {
                    case 1:
                        BookingPresentation.BookFlight(user);
                        break;
                    case 2:
                        BookingPresentation.GetUserBookings(user);
                        break;
                    case 3:
                        return;
                }

            }

        }
    }
}