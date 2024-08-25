using Airport_Ticket_Booking_System.Bookings;
using Airport_Ticket_Booking_System.Utilites;
namespace Airport_Ticket_Booking_System.Users;

public class User
{
    public static string header = "id,name,email,password,role";
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Passenger;
    public List<Booking> Bookings { get; set; } = new();


    public static User CheckUserCredintials(string? email, string? password)
    {

        if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            throw new Exception("Invalid Data Entered Please Make Sure You Filled All The Fields");

        List<string> data = FileSystemUtilites.ReadFromFile("users.csv");
        foreach (string s in data)
        {
            User user = User.FromCsv(s);
            if (user.Email == email && user.Password == password)
            {
                return user;
            }
        }
        throw new Exception("Invalid Data Entered, Password and Email Doesn't Match");
    }

    public static Boolean UserExists(string? email)
    {
        
            List<string> data = FileSystemUtilites.ReadFromFile("users.csv");
            foreach (string s in data)
            {
                User user = User.FromCsv(s);
                if (user.Email == email)
                {
                    return true;
                }
            }
            return false;
        
    }

    public static User RegisterUser(string? name, string? email, string? password)
    {
        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            throw new Exception("Invalid Data Entered Please Make Sure You Filled All The Fields");
        if (UserExists(email))
            throw new Exception("User With That Email Already Exists");

        User newUser = new()
        {
            Id = FileSystemUtilites.GetNextId("users.csv"),
            Name = name,
            Email = email,
            Password = password
        };
        FileSystemUtilites.WriteToFile("users.csv", ToCsv(newUser));

        return newUser;
    }

    public static string ToCsv(User user)
    {
        return $"{user.Id},{user.Name},{user.Email},{user.Password},{user.Role}";
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

}
