using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoSoft;
using System.IO;
namespace CryptoSoft
{
    class Program
    {
        public static void Main(string[] args)
        {
            Cryptage cryptage = new Cryptage();
            string[] filesToEncrypt = { ".json" };
            var retrieveFile = Directory.GetFiles(Directory.GetCurrentDirectory());

            // regarde dans le dossier de l'excutable tout les fichier compris et test si leur extension et bien = à .json 
            foreach (string file in retrieveFile)
            {
                Console.WriteLine(file);
                var extension = Path.GetExtension(file);
                bool compareExtensions = filesToEncrypt.Contains(extension);

                if (!compareExtensions) { continue; }

                cryptage.test(file);

            }

            //    for (int i = 0; i < filesToEncrypt.Length; i += 2)
            //    {fi
            //        string inputFilePath = filesToEncrypt[i];
            //        string outputFilePath = filesToEncrypt[i + 1];

            //        int result = cpytage.test({ inputFilePath, outputFilePath });
            //    if (result == -1)
            //    {
            //        Console.Error.WriteLine($"Encryption failed for file {inputFilePath}");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"File {inputFilePath} encrypted in {result} ms");
            //    }
            //}
        }
    }
}
