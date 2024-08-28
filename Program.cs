﻿using Airport_Ticket_Booking_System.Bookings;
using Airport_Ticket_Booking_System.Utilites;
using Airport_Ticket_Booking_System.Users;
using Airport_Ticket_Booking_System.Flights;
namespace Airport_Ticket_Booking_System;

public class Program
{
    static void Main(string[] args)
    {

        string[] files = { "users.csv", "flights.csv", "bookings.csv" };
        string[] fileHeaders = { User.header, Flight.header, Booking.header };
        FileSystemUtilites.InitFiles(files, fileHeaders);

        User user = new();
        int choice = GenericUtilites.promptLoginRegister();

        switch (choice)
        {
            case 1:
                user = GenericUtilites.RequestLogin();
                GenericUtilites.PrinSucc("Login Successful");
                break;
            case 2:
                user = GenericUtilites.RequestRegister();
                GenericUtilites.PrinSucc("Register Successful");
                break;
            case 3:
                return;
        }

        while (true)
        {
            if (user.Role == UserRole.Manager)
            {
                int managerChoice = UserUtilites.PrinManagerMenu();
                switch (managerChoice)
                {
                    case 1:
                        FlightUtilites.FilterFlights();
                        break;
                    case 2:
                        FlightUtilites.UploadFlights();
                        break;
                    case 3:
                        return;
                }

            }
            if (user.Role == UserRole.Passenger)
            {
                int passengerChoice = UserUtilites.PrintPassengerMenu();
                switch (passengerChoice)
                {
                    case 1:
                        BookingUtilites.BookFlight(user);
                        break;
                    case 2:
                        BookingUtilites.UsersBookings(user);
                        break;
                    case 3:
                        return;
                }

            }

        }
    }
}