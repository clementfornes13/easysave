using System;
using System.IO;
using System.Diagnostics;

namespace CryptoSoft
{
    public class Cryptage
    {
        public int test(string inputFilePath)
        {
            //Start timer
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Console.WriteLine(args.ToString());

            if (string.IsNullOrEmpty(inputFilePath))
            {
                stopwatch.Stop();
                Console.Error.WriteLine("Wrong arguments : need file in and file out");
                return -1;
            }
            else
            {
                try
                {
                    // crée le fichier crypter avec le même nom mais rajoute Encrypted avant l'extension 
                    FileInfo fileInfo = new FileInfo(inputFilePath);
                    string newFileName = Path.GetFileNameWithoutExtension(fileInfo.Name) + "Encrypted" + fileInfo.Extension;
                    File.Copy(inputFilePath, newFileName);

                    if (!fileInfo.Exists)
                    {
                        //File.Create();

                    }

                    //optionnel verifier  que le fichier existe bien avec un nouveau file info
                    //forcer la creation du fichier si il n'existe pas
                    string fileContent = File.ReadAllText(inputFilePath);
                    int charCount = fileContent.Length;
                    //Get input
                    string OutputFile = inputFilePath;
                    byte[] inputBytes = File.ReadAllBytes(inputFilePath);
                    //read encryptionKey
                    byte[] encryptionKey = File.ReadAllBytes(@".\encryptionKey.txt");

                    //if (!filesname.Exists)
                    //{
                    //    throw new FileNotFoundException("The file was not found.", FileName);
                    //}

                    //Do a XOR for each byte
                    for (int i = 0; i < inputBytes.Length; i++)
                    {
                        byte x = encryptionKey[(byte)(i % encryptionKey.Length)];
                        inputBytes[i] = (byte)(inputBytes[i] ^ x);
                    }

                    //Write output file if it does not exist
                    if (File.Exists(OutputFile))
                    {
                        string filePath = "crypter.txt";
                        File.WriteAllBytes(OutputFile, inputBytes);
                        Console.WriteLine("Création du fichier cypter ");
                    }

                    stopwatch.Stop();
                    return (int)(stopwatch.Elapsed.TotalSeconds * 1000); ;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.ToString());
                    return -1;
                }
            }
        }
    }
}

