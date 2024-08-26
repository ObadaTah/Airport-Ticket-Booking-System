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
        if (!File.Exists(path))
        {
            using (File.Create(path)) { };
        }
    }

    public static void InitFiles(string[] files, string[] fileHeaders)
    {
        for (int i = 0; i < files.Length; i++)
        {
            CreateFile(files[i]);
        }

        for (int i = 0; i < files.Length; i++)
        {
            if (CheckIfFileEmpty(files[i]))
            {
                WriteToFile(files[i], fileHeaders[i]);
            }
        }
    }


    public static Boolean CheckIfFileEmpty(string fileName)
    {
        string path = Path.Combine(GetPath(), "storage");
        path = Path.Combine(path, fileName);
        FileInfo fi = new FileInfo(path);
        if (fi.Length == 0)
        {
            return true;
        }
        return false;
    }
    public static void WriteToFile(string fileName, string data)
    {
        string path = Path.Combine(GetPath(), "storage");
        path = Path.Combine(path, fileName);
        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine(data);
        }
    }

    public static void WriteToFile(string fileName, List<string> data)
    {
        string path = Path.Combine(GetPath(), "storage");
        path = Path.Combine(path, fileName);
        using (StreamWriter sw = File.AppendText(path))
        {
            foreach (string s in data)
            {
                sw.WriteLine(s);
            }
        }
    }


    public static List<string> ReadFromFile(string fileName)
    {
        List<string> data = [];
        string path = Path.Combine(GetPath(), "storage");
        path = Path.Combine(path, fileName);
        using (StreamReader sr = File.OpenText(path))
        {
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                data.Add(s);
            }
        }
        return data[1..^0]; // return data without the header
    }

    internal static int GetNextId(string filename)
    {
        try
        {
            string lastEntry = ReadFromFile(filename)[^1].Split(',')[0];
            return Int32.Parse(lastEntry) + 1;

        }
        catch (Exception)
        {
            return 1;
        }
    }
}
