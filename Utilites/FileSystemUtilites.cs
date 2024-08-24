namespace Airport_Ticket_Booking_System.Utilites;

public static class FileSystemUtilites
{
    public static string GetPath()
    {
        return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));
    }

    public static string GetPath(string fileName)
    {
        return Path.GetFullPath(Path.Combine(GetPath(), fileName));
    }

    private static void CreateFile(string fileName)
    {
        string path = GetPath(fileName);
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
