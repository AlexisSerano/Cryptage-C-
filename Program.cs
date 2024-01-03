public class Program
{
    public static void Main(string[] args)
    {
        Crypt crypt = new Crypt();
        while (true)
        {
            
            try
            {
                Console.WriteLine();
                Console.WriteLine("choisir option");
                Console.WriteLine("1 - crypter");
                Console.WriteLine("2 - decrypter");
                Console.WriteLine("3 - voir cryptage");
                Console.WriteLine("4 - Supprimer Cryptage");
                Console.WriteLine("5 - Sauvegarder");
                Console.WriteLine("6 - Quitter");

                int choix = int.Parse(Console.ReadLine());

                if (choix > 6)
                {
                    Console.WriteLine("il n'y a pas autant de choix");
                }
                else
                {
                    switch (choix)
                    {
                        case 1:
                            crypt.Crypter();
                            break;
                        case 2:
                            crypt.Decrypter();
                            break;
                        case 3:
                            crypt.voir_crypt();
                            break;
                        case 4:
                            crypt.Supprimer();
                            break;
                        case 5:
                            crypt.Sauvegarder();
                            break;
                        case 6:
                            return;
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("veuiller rentrer 1 2 3 4 5 ou 6");
            }           
        }
    }
}

class Crypt
    {
    List<List<string>> Database = new List<List<string>>();
    public void Crypter()
    {
        List<string> Crypted = new List<string>();

        Console.WriteLine("rentrer une phrase a crypter");
        string input = Console.ReadLine();
        foreach (char c in input)
        {
            double x = ((int)c * 2.553) / 10;
            Crypted.Add(x.ToString());
            
        }
        Console.WriteLine($"cryptage : {Database.Count + 1}");
        Console.WriteLine($"cryptage : {string.Join(" ",Crypted)}");
        Database.Add(Crypted);
        Console.WriteLine("cryptage ajouter");
        
    }

    public void voir_crypt()
    {
        if (Database.Count == 0)
        {
            Console.WriteLine("aucun crypage n'a été enregistrer");
        }
        else
        {
            for (int i = 0; i < Database.Count; i++)
            {
                Console.WriteLine($"Cryptage : {i + 1}");
                for (int j = 0; j < Database[i].Count; j++)
                {
                    Console.Write(" ");
                    Console.Write(String.Join(" ", Database[i][j]));
                    
                }
                Console.WriteLine();
            }
        }

    }

    public void Supprimer()
    {
        Console.WriteLine("quel crypage veux tu supprimer (numero de ligne)");
        int choix = int.Parse(Console.ReadLine());
        try
        {
            if (choix <= 0 || choix > Database.Count)
            {
                Console.WriteLine("cette ligne n'existe pas");
            }
            else
            {
                Database.RemoveAt(choix - 1);
                Console.WriteLine("crypage supprimer avec succès");
            }
        }
        catch (FormatException) { Console.WriteLine("le format n'est pas bon"); }
    }

    public void Decrypter()
    {
        try
        {
            Console.WriteLine("choisissez : ");
            Console.WriteLine("0 - quitter");
            Console.WriteLine("1 - message enregistrer dans la database");
            Console.WriteLine("2 - message exterieur ");

            int input = int.Parse(Console.ReadLine());

            if (input == 0) { return; }
            else if  (input > 2) { Console.WriteLine("choissez entre 1 et 2"); }
            else if (input == 1)
            {
                Console.WriteLine("quel crypage veux tu decrypter ? (num de ligne) ?");

                int choix = int.Parse(Console.ReadLine());

                if (choix <= 0 || choix > Database.Count) { Console.WriteLine("la ligne n'existe pas"); }
                else
                {
                    string decryptedText = string.Join("", Database[choix - 1]
                        .Select(num => Convert.ToChar((int)Math.Round(double.Parse(num) * 10 / 2.553))));


                    Console.WriteLine($"decryptage -> {decryptedText}");
                }
            }
            else
            {
                try
                {
                    Console.WriteLine("Rentrer les valeurs en question");
                    string choix = Console.ReadLine();

                    string[] valueStrings = choix.Split(' '); // Séparer les caractères par un espace
                    List<char> decryptedChars = new List<char>();

                    foreach (string valueString in valueStrings)
                    {
                        string[] values = valueString.Split(' '); // Séparer les valeurs par la virgule

                        foreach (string value in values)
                        {
                            if (double.TryParse(value, out double num))
                            {
                                int charValue = (int)Math.Round(num * 10 / 2.553);
                                decryptedChars.Add((char)charValue);
                            }
                            else
                            {
                                Console.WriteLine($"La valeur '{value}' n'est pas un nombre valide.");
                                return;
                            }
                        }
                    }

                    string decryptedText = new string(decryptedChars.ToArray());

                    Console.WriteLine($"Décryptage -> {decryptedText}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Le message n'est pas au bon format");
                }
            }


        }
        catch (FormatException) { Console.WriteLine("le format n'est pas le bon"); }
        
    }

    public void Sauvegarder()
    {
        Console.WriteLine("quel nom voulez vous donner a votre fichier ? ");
        string NomFichier = Console.ReadLine();
               
            if (!NomFichier.EndsWith(".txt"))
            {
                NomFichier += ".txt";
            }
        using (StreamWriter writer = new StreamWriter(NomFichier))
        {
            for (int i = 0; i < Database.Count; i++)
            {
                writer.WriteLine($"cryptage : {i + 1}");
                for (int j = 0; j < Database[i].Count;  j++)
                {
                    writer.Write($"{Database[i][j]} ");
                }
                writer.WriteLine();
            }
            writer.Close();
            Console.WriteLine("enregistrement dans le repertoire du fichier reussi avec succès");
        }

    }
}



