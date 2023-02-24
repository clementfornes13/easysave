using System;
using System.Resources;
using System.IO;
using System.Globalization;
using System.Threading;

namespace ConsoleApp
{
    public class ConsoleCLI
    {
        private string _saveChoix = "";
        private string _pathFrom = "";
        private string _deletePath = "";
        private string _executePath = "";
        private string _pathTo = "";
        private string _langueChoix = "";
        private string _continueBlank = "";
        ResourceManager rm = new ResourceManager("ConsoleApp.Resources.Langue", typeof(ConsoleApp).Assembly);

        public string SaveChoix1 { get => _saveChoix; }
        public string PathFrom1 { get => _pathFrom; }
        public string DeletePath1 { get => _deletePath; }
        public string ExecutePath1 { get => _executePath; }
        public string PathTo1 { get => _pathTo; }
        public string LangueChoix1 { get => _langueChoix; }
        public bool ChoixLangue()
        {
            //Setting up the language French or English of the program
            Console.WriteLine("*--------------Bienvenue sur le programme EasySave--------------*\n");
            Console.WriteLine("-> Choisir la langue désirée : Anglais (0), Français (1) : ");
            Console.WriteLine("-> Choose desired language : English (0), French (1) : \n");
            _langueChoix = Console.ReadLine();
            switch (_langueChoix)
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

        //Return an int for the desired action :
        //1 for create working file
        //2 for delete
        //3 for execute
        public int ChoixSave()
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
            Console.WriteLine(rm.GetString("ExitProgram"));
            Console.WriteLine("\n");
            Console.WriteLine("-------------------------------------------------");
            _saveChoix = Console.ReadLine();
            switch (_saveChoix)
            {
                case "1"://Create
                    Console.WriteLine(rm.GetString("Create choice"));
                    Console.WriteLine(rm.GetString("pathFrom"));
                    Console.WriteLine("\tC:\\...\\...");
                    _pathFrom = Console.ReadLine();
                    switch (Directory.Exists(_pathFrom))
                    {
                        case true:
                            Console.WriteLine(rm.GetString("pathTo"));
                            _pathTo = Console.ReadLine();
                            switch (Directory.Exists(_pathTo))
                            {
                                case true:
                                    return 1;
                                case false:
                                    Console.WriteLine(rm.GetString("BlankOrInvalid"));
                                    _continueBlank = Console.ReadLine();
                                    switch (_continueBlank)
                                    {
                                        case "Y":
                                            _pathTo = null;
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
                case "2": //Delete
                    Console.WriteLine(rm.GetString("Delete choice"));
                    Console.WriteLine(rm.GetString("Delete path"));
                    Console.WriteLine("\tC:\\...\\...");
                    _deletePath = Console.ReadLine();
                    switch (Directory.Exists(_deletePath))
                    {
                        case true:
                            return 2;
                        case false:
                            Console.WriteLine(rm.GetString("Invalid path"));
                            return ChoixSave();
                        default:
                            return 0;
                    }
                case "3": //Execute
                    Console.WriteLine(rm.GetString("Execute choice"));
                    Console.WriteLine(rm.GetString("Execute path"));
                    Console.WriteLine("\tC:\\...\\...");
                    _executePath = Console.ReadLine();
                    switch (Directory.Exists(_executePath))
                    {
                        case true:
                            return 3;
                        case false:
                            Console.WriteLine(rm.GetString("Invalid path"));
                            return ChoixSave();
                        default:
                            return 0;
                    }
                case "4": //Quit the program
                    System.Environment.Exit(0);
                    return 0;
                default:
                    Console.WriteLine(rm.GetString("Error"));
                    return ChoixSave();
            }
        }

    }
}
