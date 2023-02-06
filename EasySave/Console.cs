using System;
using System.IO;

namespace Console
{
    class EasySave
{
    static void Main(string[] args)
    {
        SaveFile File1 = new SaveFile("1.txt", "C:\\Users\\Eystone\\source\\repos", "C:\\Users\\Eystone\\source");
        while (Console.ReadLine() != "q") { }
    }
}

class Console
{

    public Console(bool english)
    {
        Console.WriteLine("Choisir la langue désirée : Anglais (0), Français (1) : ");
        Console.WriteLine("Choose desired language : English (0), French (1) : ");
        english = ConvertToBoolean(Console.ReadLine());
            if english == 0
                {
                System.Resources.ResourceManager mgr = new
                System.Resources.ResourceManager("EasySave.English", System.Reflection.Assembly.GetExecutingAssembly());
                Console.WriteLine(mgr.GetString("English"));
                Console.ReadLine();
            }
            else
            {
                System.Resources.ResourceManager mgr = new
                System.Resources.ResourceManager("EasySave.French", System.Reflection.Assembly.GetExecutingAssembly());
                Console.WriteLine(mgr.GetString("French"));
                Console.ReadLine();
            }
    }
}
