using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EasySave
{
    class Controller
    {
        public Controller()
        {
            createUI(); //Summon UI to get choice
            Console.WriteLine("Debiluuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuus");
            Console.ReadKey();
        }

        private void createUI() // clement
        {
            ConsoleCLI cli = new ConsoleCLI();
            cli.ChoixLangue();
        }

        private void addWorkingFiles(string[] filePaths) //Thomas - Jordan
        {
            
        }

        private void createJob() //Thomas
        {

        }

        private void showStates()
        {

        }

        //tests methods
        private void JordanAmusetoi()
        {

        }
    }
}