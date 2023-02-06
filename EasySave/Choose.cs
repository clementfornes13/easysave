using System;
using System.IO;
using System.Resources;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace EasySave
{
    class EasySave
    {
    static void Main(string[] args)
        {
            Choose.Choose();
        }
    }

    class Choose
    {

        public static Choose(bool english)
        {
            Console.WriteLine("Choisir la langue désirée : Anglais (0), Français (1) : ");
            Console.WriteLine("Choose desired language : English (0), French (1) : ");
            english = ConvertToBoolean(Console.ReadLine());
                if english == 0
                {
                    Langue lng = new Langue("EasySave.Resources.Langue",
                        Assembly.GetExecutingAssembly());
                    Console.WriteLine(Thread.CurrentThread.CurrentUICulture.Name);
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");
                    Console.WriteLine(Thread.CurrentThread.CurrentUICulture.Name);
                Console.WriteLine(lng.GetString("Select files", new CultureInfo("en"));
                Console.ReadLine();
                }
                else
                {
                    Langue lng = new Langue("EasySave.Resources.Langue",
                    Assembly.GetExecutingAssembly());
                    Console.WriteLine(Thread.CurrentThread.CurrentUICulture.Name);
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fr");
                    Console.WriteLine(Thread.CurrentThread.CurrentUICulture.Name);
                    Console.WriteLine(lng.GetString("Select files", new CultureInfo("fr"));
                    Console.ReadLine();
                }
        }
    }
}