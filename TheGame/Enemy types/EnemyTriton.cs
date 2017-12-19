using System;
using System.Collections.Generic;

namespace TheGame
{
    class EnemyTriton : Enemy
    {
        public EnemyTriton(int level)
        {
            this.Name = "Тритон";
            this.PowerAttack = 50 + level * level / 4;
            this.IsLive = true;
            this.Health = 128 + 2 * level * level;
            this.Accuracy = 0.8f;
            this.MoneyReward = 15 + 2 * level;
            this.SkillReward = GetSkill(30, 80, 15, level);
            this.Mimicry = true;
            this.Type = EnemyType.Triton;
        }

        public static List<Enemy> CreateEnemyTriton(int playerLevel)
        {
            int level = Program.Random.Next(1, 101);

            if (level <= 70) return new List<Enemy> { new EnemyTriton(playerLevel) };
            else if (level <= 80) return new List<Enemy> { new EnemyTriton(playerLevel + 1) };
            else if (level <= 90) return new List<Enemy> { new EnemyTriton(playerLevel - 1) };
            else if (level <= 95) return new List<Enemy> { new EnemyTriton(playerLevel + 2) };
            else if (level <= 98) return new List<Enemy> { new EnemyTriton(playerLevel - 2) };
            return new List<Enemy> { new EnemyTriton(playerLevel + 3) };
        }
    }
}