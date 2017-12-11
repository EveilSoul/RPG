using System;
using System.Collections.Generic;

namespace TheGame
{
    class EnemyGriffin : Enemy
    {
        public EnemyGriffin(int level)
        {
            this.Name = "Грифон";
            this.PowerAttack = 58 + level * level / 2;
            this.IsLive = true;
            this.Health = 148 + 2 * level * level;
            this.Accuracy = 0.9f;
            this.MoneyReward = 35 + 2 * level;
            this.SkillReward = GetSkill(30, 80, 35, level);
            this.Mimicry = true;
        }

        public static List<Enemy> CreateGriffin(int playerLevel)
        {
            int level = Program.Random.Next(1, 101);

            if (level <= 70) return new List<Enemy> { new EnemyGriffin(playerLevel) };
            else if (level <= 80) return new List<Enemy> { new EnemyGriffin(playerLevel + 1) };
            else if (level <= 90) return new List<Enemy> { new EnemyGriffin(playerLevel - 1) };
            else if (level <= 95) return new List<Enemy> { new EnemyGriffin(playerLevel + 2) };
            else if (level <= 98) return new List<Enemy> { new EnemyGriffin(playerLevel - 2) };
            return new List<Enemy> { new EnemyGriffin(playerLevel + 3) };
        }
    }
}