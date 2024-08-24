﻿namespace Airport_Ticket_Booking_System.Utilites;

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

    public static void InitFiles(string[] files, string[] fileHeaders)
    {
        for (int i = 0; i < files.Length; i++)
        {
            CreateFile(files[i]);
        }

        for (int i = 0; i < files.Length; i++)
        {
            if (checkIfFileEmpty(files[i]))
            {
                WriteToFile(files[i], fileHeaders[i]);
            }
        }
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

    public static Boolean checkIfFileEmpty(string fileName)
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

}
