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
        ResourceManager rm = new ResourceManager("EasySave.Resources.Langue", typeof(ConsoleCLI).Assembly);

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
            string SaveChoix = "0";
            do
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
                SaveChoix = Console.ReadLine();
                if (SaveChoix == "1")
                {
                    Console.WriteLine(rm.GetString("Create choice"));
                }
                else if (SaveChoix == "2")
                {
                    Console.WriteLine(rm.GetString("Delete choice"));
                }
                else if (SaveChoix == "3")
                {
                    Console.WriteLine(rm.GetString("Execute choice"));
                }
                else
                {
                    Console.WriteLine(rm.GetString("Error"));
                }

            } while (SaveChoix != "1" && SaveChoix != "2" && SaveChoix != "3");
        }
    }
}
