using System;
using System.Collections.Generic;

namespace TheGame
{
    class EnemyOrk : Enemy
    {
        public EnemyOrk(int level)
        {
            this.Name = "Ork";
            this.PowerAttack = 35 + level * level / 2;
            this.IsLive = true;
            this.Health = 98 + 2 * level * level;
            this.Accuracy = 0.85f;
            this.MoneyReward = 25 + 2 * level;
            this.SkillReward = GetSkill(30, 80, 25, level);
            this.Mimicry = false;
        }

        public static List<Enemy> CreateEnemyOrk(int playerLevel)
        {
            int count = Program.Random.Next(2, 5);
            int level = Program.Random.Next(1, 101);
            var enemyes = new List<Enemy>(count);
            for (int i = 0; i < count; i++)
            {
                if (level <= 70) enemyes.Add(new EnemyOrk(playerLevel));
                else if (level <= 80) enemyes.Add(new EnemyOrk(playerLevel + 1));
                else if (level <= 90) enemyes.Add(new EnemyOrk(playerLevel - 1));
                else if (level <= 95) enemyes.Add(new EnemyOrk(playerLevel + 2));
                else if (level <= 98) enemyes.Add(new EnemyOrk(playerLevel - 2));
                else enemyes.Add(new EnemyOrk(playerLevel + 3));
            }
            return enemyes;
        }
    }
}