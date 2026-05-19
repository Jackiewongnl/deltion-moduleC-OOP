using System;

namespace Deltion
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            bool gameOver = false;
            Random random = new Random();
            int battlesWon = 0; 

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welkom, avonturier!");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Je wordt wakker in een donkere, vochtige grot. De lucht is koud en het enige geluid is het druppelen van water.");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Een zwak licht flikkert van een fakkel die op de grond naast je ligt.\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Wat is je naam? ");
            Console.ForegroundColor = ConsoleColor.White;
            string? inputName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputName))
            {
                inputName = "Vreemdeling";
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nHALLO, {inputName}!");
            Console.ForegroundColor = ConsoleColor.White;

            Player player = new Player(inputName, 100, 20);
            Enemy? enemy = null;

            while (gameOver == false)
            {
                if (enemy == null || enemy.Health <= 0)
                {
                    if (enemy != null && enemy.Health <= 0)
                    {
                        battlesWon++; 
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nZEGE! De {enemy.Name} stort in elkaar.");
                        Console.ForegroundColor = ConsoleColor.White;

                        bool leveledUp = player.GainExperience(enemy.ExpReward);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Je kreeg {enemy.ExpReward} EXP!");
                        Console.ForegroundColor = ConsoleColor.White;

                        if (leveledUp)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"LEVEL OMHOOG! Je bent nu level {player.Level}! Max HP, Aanval en Kritieke kans verhoogd. Gezondheid volledig hersteld!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }

                    
                    int enemyTypePool = 1; 
                    if (battlesWon >= 2) enemyTypePool = 2; 
                    if (battlesWon >= 4) enemyTypePool = 3; 

                    int randomType = random.Next(0, enemyTypePool);

                    if (randomType == 0) 
                    {
                        string[] normalNames = { "Grotgoblin", "Skeletkrijger" };
                        enemy = new Enemy(normalNames[random.Next(normalNames.Length)], random.Next(40, 70), random.Next(8, 15), random.Next(15, 30));
                    }
                    else if (randomType == 1) 
                    {
                        string[] tankNames = { "Stenen Golem", "Gewapende Ork" };
                        enemy = new TankEnemy(tankNames[random.Next(tankNames.Length)], random.Next(60, 90), random.Next(5, 12), random.Next(25, 40), 4); 
                    }
                    else 
                    {
                        string[] blitzNames = { "Moordenaar", "Wilde Wolf" };
                        enemy = new BlitzEnemy(blitzNames[random.Next(blitzNames.Length)], random.Next(30, 50), random.Next(6, 12), random.Next(30, 50));
                    }

                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"\nEr verschijnt een wilde {enemy.Name} uit de schaduwen!");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Het heeft {enemy.Health} HP en {enemy.Attack} Aanvalskracht.");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("===================================================");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n[Lv.{player.Level} {player.Name}: {player.Health}/{player.MaxHealth} HP]  tegen  [{enemy.Name}: {enemy.Health} HP]");
                Console.ForegroundColor = ConsoleColor.White;

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Wat ga je doen? (Typ 'aanval' of 'healen'): ");
                Console.ForegroundColor = ConsoleColor.White;

                string? command = Console.ReadLine()?.Trim().ToLower();

                if (command == "aanval")
                {
                    bool isCritical;
                    int damageDealt = player.GetAttackDamage(out isCritical);
                    if (isCritical)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"KRITISCHE TREFFER! Je slaat de {enemy.Name} voor {damageDealt} schade!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Je slaat de {enemy.Name} met je wapen en doet {damageDealt} schade!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    enemy.TakeDamage(damageDealt);
                }
                else if (command == "healen")
                {
                    int healAmount = random.Next(20, 35);
                    player.Heal(healAmount);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Je gebruikt een genezingsspreuk en herstelt {healAmount} HP!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Je raakt in paniek en struikelt over je eigen voeten! Je mist je beurt...");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (enemy.Health > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("...");
                    Console.ForegroundColor = ConsoleColor.White;
                    enemy.PerformAttack(player);
                }

                if (player.Health <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"\nJe valt op je knieën... De {enemy.Name} heeft je verslagen.");
                    Console.WriteLine($"Je overleefde {battlesWon} gevechten.");
                    Console.WriteLine("SPEL VOORBIJ.");
                    Console.ForegroundColor = ConsoleColor.White;
                    gameOver = true;
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nDruk op Enter om het spel te sluiten...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }

        public static void Print(string textToWrite, ConsoleColor color = ConsoleColor.White, bool writeLine = true)
        {
            Console.ForegroundColor = color;
            if (writeLine)
            {
                Console.WriteLine(textToWrite);
            }
            else
            {
                Console.Write(textToWrite);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Write(string textToWrite, ConsoleColor color = ConsoleColor.White)
        {
            Print(textToWrite, color, writeLine: false);
        }
    }
}