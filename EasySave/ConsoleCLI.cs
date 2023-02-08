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
            int LangueChoix;
            bool langFR = false;
            do
            {
                Console.WriteLine("*--------------Bienvenue sur le programme EasySave--------------*\n");
                Console.WriteLine("Choisir la langue désirée : Anglais (0), Français (1) : ");
                Console.WriteLine("Choose desired language : English (0), French (1) : \n");
                bool Langue = int.TryParse(Console.ReadLine(), out LangueChoix);
                if (Langue)
                {
                    if (LangueChoix == 1)
                    {
                        langFR = true;
                    }
                    else if (LangueChoix == 0)
                    {
                        langFR = false;
                    }
                    if (langFR == true)
                    {
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                        Console.WriteLine(rm.GetString("Select files"));
                    }
                    else if (langFR == false)
                    {
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                        Console.WriteLine(rm.GetString("Select files"));
                    }
                }
                else
                {
                    Console.WriteLine("Erreur, rentrez à nouveau la langue désirée");
                    Console.WriteLine("Error, enter desired language again\n");
                }
            } while (langFR != true && langFR != false);
        }
    }
}
