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
        User user = User.CheckUserCredintials(email, password);
        while (user == null)
        {
            Console.WriteLine("Invalid Credintials. Please Try Again");
            return RequestLogin();
        }
        return user;
    }

    public static User RequestRegister()
    {

        Console.Write("Enter your Name: ");
        string? name = Console.ReadLine();
        Console.Write("Enter your Email: ");
        string? email = Console.ReadLine();
        Console.Write("Enter your password: ");
        string? password = Console.ReadLine();

        User user;
        try
        {
            user = User.RegisterUser(name, email, password);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RequestRegister();
        }
       
        return user;
    }
    public static int AskValidInt(int max)
    {
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();
        try
        {
            if (choice == null)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                return AskValidInt(max);
            }
            int num = int.Parse(choice);
            if (num > 0 && num <= max)

                return num;

            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                return AskValidInt(max);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid choice. Please try again.");
            return AskValidInt(max);
        }
    }
}

