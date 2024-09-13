using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Services;

namespace Airport_Ticket_Booking_System.Repositories;

public static class FlightRepository
{
    public static Dictionary<int, Flight> GetFlights()
    {
        List<string> data = FileSystemUtilities.ReadFromFile("flights.csv");
        Dictionary<int, Flight> flights = [];
        foreach (string s in data)
        {
            Flight flight = FlightService.FromCsv(s);
            flights.Add(flight.FlightNumber, flight);
        }
        return flights;
    }

    public static void SaveFlightsFromFile(string? fileAddress)
    {
        List<string> data = FileSystemUtilities.ReadFromFile(fileAddress!);
        for (int i = 0; i < data.Count; i++)
        {
            string s = data[i];
            try
            {
                Flight flight = FlightService.FromCsv(s, i + 2);
                FileSystemUtilities.WriteToFile("flights.csv", FlightService.ToCsv(flight));
            }
            catch (Exception e)
            {
                GenericUtilities.PrintError($"Error in Line {i + 2}: {e.Message}");
            }

        }
    }
}