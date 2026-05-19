using System;

namespace Deltion
{
    public class TankEnemy : Enemy
    {
        public int Armor { get; private set; }

        public TankEnemy(string name, int health, int attack, int expReward, int armor)
            : base(name, health, attack, expReward)
        {
            Armor = armor;
        }

        public override void TakeDamage(int damage)
        {
            int actual = damage - Armor;
            if (actual < 0) actual = 0;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"De dikke huid van de {Name} is te dik om de schade met {damage - actual}.");
            Console.ForegroundColor = ConsoleColor.White;
            base.TakeDamage(actual);
        }
    }
}
