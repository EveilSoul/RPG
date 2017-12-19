using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class EnemyDragon : Enemy
    {
        public EnemyDragon(int level)
        {
            this.Name = "Дракон";
            this.PowerAttack = 150 + level * level / 4;
            this.IsLive = true;
            this.Health = 498 + 2 * level * level;
            this.Accuracy = 0.95f;
            this.MoneyReward = 100 + 2 * level * level;
            this.SkillReward = GetSkill(25, 50, 75, level);
            this.Mimicry = true;
            this.Type = EnemyType.Dragon;
        }

        public static List<Enemy> CreateEnemyDragon(int playerLevel)
        {
            int level = Program.Random.Next(1, 101);

            if (level <= 70) return new List<Enemy> { new EnemyDragon(playerLevel) };
            else if (level <= 80) return new List<Enemy> { new EnemyDragon(playerLevel + 1) };
            else if (level <= 90) return new List<Enemy> { new EnemyDragon(playerLevel - 1) };
            else if (level <= 95) return new List<Enemy> { new EnemyDragon(playerLevel + 2) };
            else if (level <= 98) return new List<Enemy> { new EnemyDragon(playerLevel - 2) };
            return new List<Enemy> { new EnemyDragon(playerLevel + 3) };
        }
    }
}
