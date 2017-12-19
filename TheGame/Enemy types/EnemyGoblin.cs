using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class EnemyGoblin : Enemy
    {
        public EnemyGoblin(int level)
        {
            this.Name = "Гоблин";
            this.PowerAttack = 20 + level * level / 4;
            this.IsLive = true;
            this.Health = 48 + 2 * level * level;
            this.Accuracy = 0.8f;
            this.MoneyReward = 9 + 2 * level;
            this.SkillReward = GetSkill(30, 80, 7, level);
            this.Mimicry = false;
            this.Type = EnemyType.Goblin;
        }

        public static List<Enemy> CreateEnemyGoblin(int playerLevel)
        {
            int count = Program.Random.Next(2, 4);
            int level = Program.Random.Next(1, 101);
            var enemyes = new List<Enemy>(count);
            for (int i = 0; i < count; i++)
            {
                if (level <= 70) enemyes.Add(new EnemyGoblin(playerLevel));
                else if (level <= 80) enemyes.Add(new EnemyGoblin(playerLevel + 1));
                else if (level <= 90) enemyes.Add(new EnemyGoblin(playerLevel - 1));
                else if (level <= 95) enemyes.Add(new EnemyGoblin(playerLevel + 2));
                else if (level <= 98) enemyes.Add(new EnemyGoblin(playerLevel - 2));
                else enemyes.Add(new EnemyWolf(playerLevel + 3));
            }
            return enemyes;
        }
    }
}
