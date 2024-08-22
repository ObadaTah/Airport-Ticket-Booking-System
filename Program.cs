namespace Airport_Ticket_Booking_System;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        int choice = Utilites.promptLoginRegister();
        switch (choice)
        {
            case 1:
                Utilites.RequestLogin();
                Console.WriteLine("Login Successful");
                break;
            case 2:
                Utilites.RequestRegister();
                Console.WriteLine("Register Successful");
                break;
            case 3:
                break;
            default:
                break;

        }
        
    }
}