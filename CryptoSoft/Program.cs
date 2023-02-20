using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using CryptoSoft;

namespace CryptoSoft
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Start timer
            /*Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Wrong arguments
            if (args.Length != 2 || File.Exists(args[0]) == false)
            {
                stopwatch.Stop();
                Console.Error.WriteLine("Wrong arguments : need file in and file out");
                return -1;
            }
            else
            {
                try
                {
                    //Get input
                    string OutputFile = args[1];
                    byte[] data = File.ReadAllBytes(args[0]);

                    //read key
                    byte[] key = File.ReadAllBytes(@".\key.txt");

                    //Do a XOR for each byte
                    for (int i = 0; i < data.Length; i++)
                    {
                        byte x = key[(byte)(i % key.Length)];
                        data[i] = (byte)(data[i] ^ x);
                    }
                    File.WriteAllBytes(OutputFile, data);

                    stopwatch.Stop();
                    return (int)(stopwatch.Elapsed.TotalSeconds * 1000); ;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.ToString());
                    return -1;
                }

            }*/
        

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
