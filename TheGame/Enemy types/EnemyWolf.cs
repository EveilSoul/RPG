using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class EnemyWolf : Enemy
    {
        public EnemyWolf(int level)
        {
            if (level < 0) level = 0;
            this.Name = "Волк";
            this.PowerAttack = 10 + level * level/2;
            this.IsLive = true;
            this.Health = 48 + 2*level*level;
            this.Accuracy=0.75f;
            this.MoneyReward = 5 + 2 * level;
            this.SkillReward = GetSkill(30, 80, 5, level);
            this.Mimicry = false;
            this.Type = EnemyType.Wolf;
        }

        /// <summary>
        /// Создание монстров Волк
        /// </summary>
        /// <param name="playerLevel">Уровень игрока</param>
        /// <returns>Лист Волков</returns>
        public static List<Enemy> CreateEnemyWolf(int playerLevel = 1)
        {
            int count = Program.Random.Next(3, 8);
            int level = Program.Random.Next(1, 101);
            var enemyes = new List<Enemy>(count);
            for (int i = 0; i < count; i++)
            {
                if (level<=70) enemyes.Add(new EnemyWolf(playerLevel));
                else if (level <= 80) enemyes.Add(new EnemyWolf(playerLevel + 1));
                else if (level <= 90) enemyes.Add(new EnemyWolf(playerLevel - 1));
                else if (level <= 95) enemyes.Add(new EnemyWolf(playerLevel + 2));
                else if (level <= 98) enemyes.Add(new EnemyWolf(playerLevel - 2));
                else enemyes.Add(new EnemyWolf(playerLevel + 3));
            }
            return enemyes;
        }
    }
}
