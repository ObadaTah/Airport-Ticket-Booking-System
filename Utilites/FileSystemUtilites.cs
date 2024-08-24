namespace Airport_Ticket_Booking_System.Utilites;

public static class FileSystemUtilites
{
    public static string GetPath()
    {
        return Path.GetFullPath(@"..\..\..\");
    }

    private static void CreateFile(string fileName)
    {
        string path = Path.Combine(GetPath(), "storage");
        path = Path.Combine(path, fileName);
        Console.WriteLine(path);
        if (!File.Exists(path))
        {
            using (File.Create(path)) { };
        }
    }

    public static void InitFiles(string[] files)
    {
        for (int i = 0; i < files.Length; i++)
        {
            CreateFile(files[i]);
        }
    }

}
