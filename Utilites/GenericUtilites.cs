using Airport_Ticket_Booking_System.Users;

namespace Airport_Ticket_Booking_System.Utilites;

public static partial class GenericUtilites
{
    public static int promptLoginRegister()
    {
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");


        return askValidInt(3);
    }


    public static bool RequestLogin()
    {

        Console.Write("Enter your Email: ");
        string? email = Console.ReadLine();
        Console.Write("Enter your password: ");
        string? password = Console.ReadLine();

        while (!User.CheckUserCredintials(email, password))
        {
            Console.WriteLine("Invalid Credintials. Please Try Again");
            return RequestLogin();
        }
        return true;
    }

    public static bool RequestRegister()
    {
        Console.Write("Enter your Name: ");
        string? name = Console.ReadLine();
        Console.Write("Enter your Email: ");
        string? email = Console.ReadLine();
        Console.Write("Enter your password: ");
        string? password = Console.ReadLine();

        while (!User.RegisterUser(name, email, password))
        {
            Console.WriteLine("Invalid Credintials. Please Try Again");
            return RequestRegister();
        }
        return true;
    }
    public static int askValidInt(int max)
    {
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();
        try
        {
            if (choice == null)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                return askValidInt(max);
            }
            int num = int.Parse(choice);
            if (num > 0 && num <= max)

                return num;

            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                return askValidInt(max);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid choice. Please try again.");
            return askValidInt(max);
        }
    }
}

