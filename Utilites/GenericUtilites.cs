using Airport_Ticket_Booking_System.Users;

namespace Airport_Ticket_Booking_System.Utilites;

public static partial class GenericUtilites
{
    public static int promptLoginRegister()
    {
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");


        return AskValidInt(3);
    }


    public static User RequestLogin()
    {

        Console.Write("Enter your Email: ");
        string? email = Console.ReadLine();
        Console.Write("Enter your password: ");
        string? password = Console.ReadLine();
        try
        {
            User user = User.CheckUserCredintials(email, password);
            return user;

        }
        catch (Exception e)
        {
            PrintError(e.Message);
            return RequestLogin();
        }
      
    }

    public static User RequestRegister()
    {

        Console.Write("Enter your Name: ");
        string? name = Console.ReadLine();
        Console.Write("Enter your Email: ");
        string? email = Console.ReadLine();
        Console.Write("Enter your password: ");
        string? password = Console.ReadLine();

        try
        {
            User user = User.RegisterUser(name, email, password);
            return user;
        }
        catch (Exception e)
        {
            PrintError(e.Message);
            return RequestRegister();
        }
    }
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
}

