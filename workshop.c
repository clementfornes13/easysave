using System;

class Program
{
    static void Main(string[] args)
    {
        // Saisie de la chaîne de caractères par l'utilisateur
        Console.Write("Entrez une chaîne de caractères (entre 1 et 8 caractères) : ");
        string input = Console.ReadLine();

        // Vérification de la longueur de la chaîne de caractères
        if (input.Length > 0 && input.Length <= 8)
        {
            // Conversion en majuscules de la chaîne de caractères saisie
            string output = input.ToUpper();

            // Affichage de la chaîne de caractères convertie en majuscules
            Console.WriteLine("Chaîne de caractères convertie en majuscules : " + output);

            // Attente de l'activation d'une touche clavier par l'utilisateur
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("La chaîne de caractères saisie est incorrecte.");
        }
    }
}
