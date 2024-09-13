using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Models.Enums;
using Airport_Ticket_Booking_System.Repositories;
namespace Airport_Ticket_Booking_System.Services;

public static class UserService
{
    public static User CheckUserCredintials(string? email, string? password)
    {

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            throw new Exception("Invalid Data Entered Please Make Sure You Filled All The Fields");

        List<string> data = UserRepository.GetUsers();
        foreach (string s in data)
        {
            User user = FromCsv(s);
            if (user.Email == email && user.Password == password)
            {
                return user;
            }
        }
        throw new Exception("Invalid Data Entered, Password and Email Doesn't Match");
    }

    public static User FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');
        User user = new User();
        user.Id = Convert.ToInt32(values[0]);
        user.Name = values[1];
        user.Email = values[2];
        user.Password = values[3];
        user.Role = (UserRole)Enum.Parse(typeof(UserRole), values[4]);
        return user;
    }

    public static User GetUserById(int id)
    {
        List<string> users = FileSystemUtilities.ReadFromFile("users.csv");
        foreach (string s in users)
        {
            User user = FromCsv(s);
            if (user.Id == id)
                return user;
        }
        throw new Exception("User Not Found");
    }
    public static int promptLoginRegister()
    {
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");


        return GenericUtilities.AskValidInt(3);
    }

    public static User RegisterUser(string? name, string? email, string? password)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            throw new Exception("Invalid Data Entered Please Make Sure You Filled All The Fields");
        if (UserExists(email))
            throw new Exception("User With That Email Already Exists");

        User newUser = new()
        {
            Id = FileSystemUtilities.GetNextId("users.csv"),
            Name = name,
            Email = email,
            Password = password
        };
        UserRepository.SaveUser(newUser);

        return newUser;
    }

    public static User RequestLogin()
    {

        Console.Write("Enter your Email: ");
        string? email = Console.ReadLine();
        Console.Write("Enter your password: ");
        string? password = Console.ReadLine();
        try
        {
            User user = CheckUserCredintials(email, password);
            return user;

        }
        catch (Exception e)
        {
            GenericUtilities.PrintError(e.Message);
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
            User user = RegisterUser(name, email, password);
            return user;
        }
        catch (Exception e)
        {
            GenericUtilities.PrintError(e.Message);
            return RequestRegister();
        }
    }

    public static bool UserExists(string? email)
    {
        List<string> data = FileSystemUtilities.ReadFromFile("users.csv");
        foreach (string s in data)
        {
            User user = FromCsv(s);
            if (user.Email == email)
            {
                return true;
            }
        }
        return false;
    }
}
