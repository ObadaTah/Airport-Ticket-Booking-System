using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Services;

namespace Airport_Ticket_Booking_System.Presentation;

public static class UserPresentation
{
    public static void AskEmailAndPassword(out string email, out string password)
    {
        Console.Write("Enter your Email: ");
        email = Console.ReadLine() ?? "";
        Console.Write("Enter your password: ");
        password = Console.ReadLine() ?? "";
    }

    public static void AskRegisterInfo(out string? name, out string? email, out string? password)
    {
        Console.Write("Enter your Name: ");
        name = Console.ReadLine();
        Console.Write("Enter your Email: ");
        email = Console.ReadLine();
        Console.Write("Enter your password: ");
        password = Console.ReadLine();
    }

    public static User GetUserByRegisterOrLogin()
    {
        User user = null!;
        int choice = PromptLoginRegister();

        switch (choice)
        {
            case 1:
                while (true)
                {
                    Login(out user);
                    if (user != null)
                        break;
                }
                break;
            case 2:
                while (true)
                {
                    Register(out user);
                    if (user != null)
                        break;
                }
                break;
            case 3:
                return null!;
        }
        return user!;
    }
    public static void Login(out User user)
    {
        user = null!;
        while (user == null)
        {
            AskEmailAndPassword(out string email, out string password);
            try
            {
                user = UserService.CheckUserCredintials(email, password);
            }
            catch (Exception e)
            {
                GenericUtilities.PrintError(e.Message);
            }
        }

        GenericUtilities.PrinSucc("Login Successful");
    }
    public static int PromptLoginRegister()
    {
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");


        return GenericUtilities.AskValidInt(3);
    }

    public static void Register(out User user)
    {
        user = null!;
        while (user == null)
        {
            AskRegisterInfo(out string? name, out string? email, out string? password);

            try
            {
                user = UserService.RegisterUser(name, email, password);
            }
            catch (Exception e)
            {
                GenericUtilities.PrintError(e.Message);
            }
        }
        GenericUtilities.PrinSucc("Registered Successfully");
    }
}
