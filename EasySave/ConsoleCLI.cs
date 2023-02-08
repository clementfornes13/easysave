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
using System.Runtime.CompilerServices;

namespace EasySave
{
    public class ConsoleCLI
    {
        private string SaveChoix = "";
        private string PathFrom = "";
        private string DeletePath = "";
        private string ExecutePath = "";
        private string PathTo = "";
        private string LangueChoix = "";
        private string ContinueBlank = "";
        ResourceManager rm = new ResourceManager("EasySave.Resources.Langue", typeof(ConsoleCLI).Assembly);

        public string SaveChoix1 { get => SaveChoix; }
        public string PathFrom1 { get => PathFrom; }
        public string DeletePath1 { get => DeletePath; }
        public string ExecutePath1 { get => ExecutePath; }
        public string PathTo1 { get => PathTo; }
        public string LangueChoix1 { get => LangueChoix; }
        public bool ChoixLangue()
        {
            Console.WriteLine("*--------------Bienvenue sur le programme EasySave--------------*\n");
            Console.WriteLine("-> Choisir la langue désirée : Anglais (0), Français (1) : ");
            Console.WriteLine("-> Choose desired language : English (0), French (1) : \n");
            LangueChoix = Console.ReadLine();
            switch (LangueChoix)
            {
                case "0":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    Console.WriteLine(rm.GetString("Langue choice"));
                    return true;
                case "1":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                    Console.WriteLine(rm.GetString("Langue choice"));
                    return true;
                default:
                    Console.WriteLine("!!!!! Erreur, rentrez à nouveau la langue désirée !!!!!");
                    Console.WriteLine("!!!!! Error, enter desired language again !!!!!\n");
                    return ChoixLangue();
            }
        }

        //Return an INT for the action commanded :
        //1 for create working file
        //2 for delete
        //3 for execute
        public int ChoixSave()
        {
            Console.WriteLine("\n");
            Console.WriteLine(rm.GetString("Choose save option"));
            Console.WriteLine("\n");
//            Console.WriteLine(rm.GetString("BackMenu"));
//            Console.WriteLine("\n");
            Console.WriteLine(rm.GetString("Create save"));
            Console.WriteLine("\n");
            Console.WriteLine(rm.GetString("Delete save"));
            Console.WriteLine("\n");
            Console.WriteLine(rm.GetString("Execute save"));
            Console.WriteLine("\n");
            Console.WriteLine(rm.GetString("ExitProgram"));
            Console.WriteLine("\n");
            SaveChoix = Console.ReadLine();
            switch (SaveChoix)
            {
                case "1":
                    Console.WriteLine(rm.GetString("Create choice"));
                    Console.WriteLine(rm.GetString("pathFrom"));
                    Console.WriteLine("\tC:\\...\\...");
                    PathFrom = Console.ReadLine();
                    switch (Directory.Exists(PathFrom))
                    {
                        case true:
                            Console.WriteLine(rm.GetString("pathTo"));
                            PathTo = Console.ReadLine();
                            switch (Directory.Exists(PathTo))
                            {
                                case true:
                                    return 1;
                                case false:
                                    Console.WriteLine(rm.GetString("BlankOrInvalid"));
                                    ContinueBlank = Console.ReadLine();
                                    switch (ContinueBlank)
                                    {
                                        case "Y":
                                            PathTo = null;
                                            return 1;
                                        case "N":
                                            return ChoixSave();
                                        default:
                                            return ChoixSave();
                                    }
                                default:
                                    return 0;
                            }
                        case false:
                            Console.WriteLine(rm.GetString("Invalid path"));
                            return ChoixSave();
                        default:
                            return 0;
                    }
                case "2":
                    Console.WriteLine(rm.GetString("Delete choice"));
                    Console.WriteLine(rm.GetString("Delete path"));
                    Console.WriteLine("\tC:\\...\\...");
                    DeletePath = Console.ReadLine();
                    switch (Directory.Exists(DeletePath))
                    {
                        case true:
                            return 2;
                        case false:
                            Console.WriteLine(rm.GetString("Invalid path"));
                            return ChoixSave();
                        default:
                            return 0;
                    }
                case "3":
                    Console.WriteLine(rm.GetString("Execute choice"));
                    Console.WriteLine(rm.GetString("Execute path"));
                    Console.WriteLine("\tC:\\...\\...");
                    ExecutePath = Console.ReadLine();
                    switch (Directory.Exists(ExecutePath))
                    {
                        case true:
                            return 3;
                        case false:
                            Console.WriteLine(rm.GetString("Invalid path"));
                            return ChoixSave();
                        default:
                            return 0;
                    }
                case "4":
                    System.Environment.Exit(0);
                    return 0;
                default:
                    Console.WriteLine(rm.GetString("Error"));
                    return ChoixSave();
            }
        }

    }
}
