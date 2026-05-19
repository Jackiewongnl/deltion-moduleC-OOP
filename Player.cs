using System;

namespace Deltion
{
    public class Player
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public int ExpToNextLevel { get; private set; }
        public int CriticalRate { get; private set; }
        public int CriticalDamage { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int Attack { get; private set; }

        public Player(string name, int maxHealth, int attack)
        {
            Name = name;
            Level = 1;
            Experience = 0;
            ExpToNextLevel = 50;
            CriticalRate = 20;
            CriticalDamage = 15;
            MaxHealth = maxHealth;
            Health = MaxHealth;
            Attack = attack;
        }
        public int GetAttackDamage(out bool isCritical)
        {
            Random random = new Random();
            int baseDamage = random.Next(Attack / 2, Attack + 1);

            isCritical = random.Next(1, 101) <= CriticalRate;

            if (isCritical)
            {
                return baseDamage + CriticalDamage;
            }
            return baseDamage;
        }

        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
        }

        public bool GainExperience(int expAmount)
        {
            Experience += expAmount;
            if (Experience >= ExpToNextLevel)
            {
                LevelUp();
                return true;
            }
            return false;
        }

        private void LevelUp()
        {
            Level++;
            Experience -= ExpToNextLevel;
            ExpToNextLevel = (int)(ExpToNextLevel * 1.5);
            MaxHealth += 20;
            Attack += 5;
            Health = MaxHealth;
            CriticalRate += 2; 
        }
        public string GetLevelUpMessage()
        {
            return $"Gefeliciteerd! {Name} is nu level {Level}.";
        }

        public string GetExperienceStatus()
        {
            return $"Ervaring: {Experience}/{ExpToNextLevel}";
        }

        public string GetHealthStatus()
        {
            return $"Gezondheid: {Health}/{MaxHealth}";
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }
    }
}