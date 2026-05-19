using System;

namespace Deltion
{
    public class Enemy
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Attack { get; protected set; }
        public int ExpReward { get; protected set; }

        public Enemy(string name, int health, int attack, int expReward)
        {
            Name = name;
            MaxHealth = health;
            Health = health;
            Attack = attack;
            ExpReward = expReward;
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public virtual void PerformAttack(Player target)
        {
            Random random = new Random();
            int damage = random.Next(Attack / 2, Attack + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"De {Name} slaat terug en raakt je voor {damage} schade!");
            Console.ForegroundColor = ConsoleColor.White;
            target.TakeDamage(damage);
        }
    }
}
