using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySave;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Controller controller = new Controller();

        Console.WriteLine("Veuillez entrer les chemins de fichier séparés par des virgules :");
        string filePathsInput = Console.ReadLine();

        string[] filePaths = filePathsInput.Split(',');

        controller.addWorkingFiles(filePaths);

    }
    // test yo 

}

class Controller
{
    List<string> workingFiles;

    public void addWorkingFiles(string[] filePaths)
    {
        workingFiles = new List<string>();

        foreach (string filePath in filePaths)
        {
            // Vérifiez si le fichier existe avant de l'ajouter à la liste de travail
            if (File.Exists(filePath))
            {
                workingFiles.Add(filePath);
            }
            else
            {
                Console.WriteLine("Le fichier " + filePath + " n'existe pas.");
            }
        }
    }

    // Autres méthodes ...
}
