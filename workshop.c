using System;

class Program
{
    static void Main(string[] args)
    {
        // Saisie de la cha�ne de caract�res par l'utilisateur
        Console.Write("Entrez une cha�ne de caract�res (entre 1 et 8 caract�res) : ");
        string input = Console.ReadLine();

        // V�rification de la longueur de la cha�ne de caract�res
        if (input.Length > 0 && input.Length <= 8)
        {
            // Conversion en majuscules de la cha�ne de caract�res saisie
            string output = input.ToUpper();

            // Affichage de la cha�ne de caract�res convertie en majuscules
            Console.WriteLine("Cha�ne de caract�res convertie en majuscules : " + output);

            // Attente de l'activation d'une touche clavier par l'utilisateur
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("La cha�ne de caract�res saisie est incorrecte.");
        }
    }
}
