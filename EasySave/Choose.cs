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
    public class Choose
    {
        public void ChooseFct()
        {
            ResourceManager rm = new ResourceManager("prosoft.Resources.Langue",
        typeof(Choose).Assembly);
            Console.WriteLine("Choisir la langue désirée : Anglais (0), Français (1) : ");
            Console.WriteLine("Choose desired language : English (0), French (1) : ");
            int Valeurs;
            bool success = int.TryParse(Console.ReadLine(), out Valeurs);
            bool english = false;
            if (success)
            {
                if (Valeurs == 1)
                {
                    english = true;
                }
            }
            else
            {
                Console.WriteLine("Entrée invalide, réessayez");
            }

            if (english == false)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                Console.WriteLine(rm.GetString("Select files"));

            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
                Console.WriteLine(rm.GetString("Select files"));
            }
        }
    }
}
