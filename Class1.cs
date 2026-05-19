using System;

namespace Deltion
{
    public class BlitzEnemy : Enemy
    {
        public int Armor { get; private set; }

        public BlitzEnemy(string name, int health, int attack, int expReward, int armor)
            : base(name, health, attack, expReward)
        {
            Armor = armor;
        }

        public BlitzEnemy(string name, int health, int attack, int expReward)
            : this(name, health, attack, expReward, armor: 0)
        {
        }

        public override void TakeDamage(int damage)
        {
            int actualDamage = damage - Armor;
            if (actualDamage < 0) actualDamage = 0;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"De bepantsering van de {Name} blokkeert {damage - actualDamage} schade!");
            Console.ForegroundColor = ConsoleColor.White;
            base.TakeDamage(actualDamage);
        }

        public override void PerformAttack(Player target)
        {
            Random random = new Random();
            int comboChance = 80;
            bool attacking = true;

            while (attacking && target.Health > 0)
            {
                if (random.Next(1, 101) <= 15)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"De {Name} beweegt te snel en MISST!");
                    Console.ForegroundColor = ConsoleColor.White;
                    comboChance = 80;
                    attacking = false;
                }
                else
                {
                    int damage = random.Next(Attack / 2, Attack + 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"De {Name} valt snel aan en doet {damage} schade!");
                    Console.ForegroundColor = ConsoleColor.White;
                    target.TakeDamage(damage);

                    if (target.Health > 0 && random.Next(1, 101) <= comboChance)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine($"De {Name} bereidt een combo-aanval voor!");
                        Console.ForegroundColor = ConsoleColor.White;
                        comboChance -= 25;
                    }
                    else
                    {
                        attacking = false;
                        comboChance = 80;
                    }
                }
            }
        }
    }
}
