using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Reflection.Emit;
using System.Runtime.Remoting.Channels;

namespace EasySave
{
    public class ConsoleCLI
    {
        private string SaveChoix = "";
        private string CreatePath = "";
        private string DeletePath = "";
        private string ExecutePath = "";
        ResourceManager rm = new ResourceManager("EasySave.Resources.Langue", typeof(ConsoleCLI).Assembly);

        public string SaveChoix1 { get => SaveChoix;}
        public string CreatePath1 { get => CreatePath; }
        public string DeletePath1 { get => DeletePath; }
        public string ExecutePath1 { get => ExecutePath;}

        public void ChoixLangue()
        {
            string LangueChoix = "2";
            do
            {
                Console.WriteLine("*--------------Bienvenue sur le programme EasySave--------------*\n");
                Console.WriteLine("-> Choisir la langue désirée : Anglais (0), Français (1) : ");
                Console.WriteLine("-> Choose desired language : English (0), French (1) : \n");
                LangueChoix = Console.ReadLine();
                if (LangueChoix == "1")
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                }
                else if (LangueChoix == "0")
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                }
                else
                {
                    Console.WriteLine("!!!!! Erreur, rentrez à nouveau la langue désirée !!!!!");
                    Console.WriteLine("!!!!! Error, enter desired language again !!!!!\n");
                }
            } while (LangueChoix != "1" && LangueChoix != "0");
        }

        public void ChoixSave()
        {

            Console.WriteLine("\n");
            Console.WriteLine(rm.GetString("Choose save option"));
            Console.WriteLine("\n");
            Console.WriteLine(rm.GetString("Create save"));
            Console.WriteLine("\n");
            Console.WriteLine(rm.GetString("Delete save"));
            Console.WriteLine("\n");
            Console.WriteLine(rm.GetString("Execute save"));
            Console.WriteLine("\n");
            SaveChoix1 = Console.ReadLine();
            switch (SaveChoix1)
            {
                case "1":
                    Console.WriteLine(rm.GetString("Create choice"));
                    Console.WriteLine(rm.GetString("Create path"));
                    Console.WriteLine("\tC:\\...\\...");
                    CreatePath1 = Console.ReadLine();
                    switch (Directory.Exists(CreatePath1))
                    {
                        case true:
                            break;
                        case false:
                            Console.WriteLine(rm.GetString("Invalid path"));
                            ChoixSave();
                            break;
                    }
                    break;
                case "2":
                    Console.WriteLine(rm.GetString("Delete choice"));
                    Console.WriteLine(rm.GetString("Delete path"));
                    Console.WriteLine("\tC:\\...\\...");
                    DeletePath1 = Console.ReadLine();
                    switch (Directory.Exists(DeletePath1))
                    {
                        case true:
                            break;
                        case false:
                            Console.WriteLine(rm.GetString("Invalid path"));
                            ChoixSave();
                            break;
                    }
                    break;
                case "3":
                    Console.WriteLine(rm.GetString("Execute choice"));
                    Console.WriteLine(rm.GetString("Execute path"));
                    Console.WriteLine("\tC:\\...\\...");
                    ExecutePath1 = Console.ReadLine();
                    switch (Directory.Exists(ExecutePath1))
                    {
                        case true:
                            break;
                        case false:
                            Console.WriteLine(rm.GetString("Invalid path"));
                            ChoixSave();
                            break;
                    }
                    break;
                default:
                    Console.WriteLine(rm.GetString("Error"));
                    ChoixSave();
                    break;
            }
        }

    }
}
