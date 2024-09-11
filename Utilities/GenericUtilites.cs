using Airport_Ticket_Booking_System.Users;

namespace Airport_Ticket_Booking_System.Utilities;

public static partial class GenericUtilities
{
    public static int AskValidInt(int max)
    {
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();
        try
        {
            if (choice == null)
            {
                PrintError("Invalid choice. Please try again.");
                return AskValidInt(max);
            }
            int num = int.Parse(choice);
            if (num > 0 && num <= max)

                return num;

            else
            {
                PrintError("Invalid choice. Please try again.");
                return AskValidInt(max);
            }
        }
        catch (Exception)
        {
            PrintError("Invalid choice. Please try again.");
            return AskValidInt(max);
        }
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void PrinSucc(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static int PrinManagerMenu()
    {
        Console.WriteLine("1. Filter Bookings");
        Console.WriteLine("2. Upload Flights");
        Console.WriteLine("3. Exit");
        return GenericUtilities.AskValidInt(3);
    }

    public static int PrintPassengerMenu()
    {
        Console.WriteLine("1. Book A Flight");
        Console.WriteLine("2. My Bookings");
        Console.WriteLine("3. Exit");
        return GenericUtilities.AskValidInt(3);
    }
}

