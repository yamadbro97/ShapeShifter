using UnityEngine;
using System.IO;
using UnityEditor.PackageManager;

public static class CSVManager
{
    private static string UserDataDirectory = "UserData";
    private static string UserDataFile = "userData.csv";
    private static string UserDataSeparator = ";";
    private static string[] UserDataHeaders = new string[7] {
        "UserID",
        "LevelID",
        "Input Method",
        "Try Number",
        "Moves Counter",
        "Time",
    "Finished"};
    private static string TimeStampHeader = "Time Stamp";
    public static void AppendToUserData(string[] strings)
    {
        VerifyDirectory();
        VerifyFile();
        using (StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string finalString = "";
            for(int i=0; i<strings.Length; i++)
            {
                if(finalString != "")
                {
                    finalString += UserDataSeparator;
                }
                finalString += strings[i];
            }
            finalString += UserDataSeparator + GetTimeStamp();
            sw.WriteLine(finalString);
        }
    }
    public static void CreateUserData()
    {
        VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath())) 
        {
            string finalString = "";
            for(int i = 0; i < UserDataHeaders.Length;i++)
            {
                if (finalString != "")
                {
                    finalString += UserDataSeparator;
                }
                finalString += UserDataHeaders[i];
            }
            finalString += UserDataSeparator + TimeStampHeader;
            sw.WriteLine(finalString);
        }
    }
    static void VerifyDirectory()
    {
        string dir = GetDirectoryPath();
        if(!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }
    static void VerifyFile()
    {
        string file = GetFilePath();
        if (!File.Exists(file))
        {
            CreateUserData();
        }
    }
    static string GetDirectoryPath()
    {
        return Application.dataPath+"/"+ UserDataDirectory;
    }
    static string GetFilePath()
    {
        return GetDirectoryPath()+ "/"+ UserDataFile;
    }
    static string GetTimeStamp()
    {
        return System.DateTime.Now.ToString();
    }
}
