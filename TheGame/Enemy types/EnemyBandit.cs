using System;
using System.Collections.Generic;

namespace TheGame
{
    class EnemyBandit : Enemy
    {
        public EnemyBandit(int level)
        {
            this.Name = "Бандит";
            this.PowerAttack = 30 + level * level / 4;
            this.IsLive = true;
            this.Health = 68 + 2 * level * level;
            this.Accuracy = 0.8f;
            this.MoneyReward = 12 + 2 * level;
            this.SkillReward = GetSkill(30, 80, 10, level);
            this.Mimicry = false;
            this.Type = EnemyType.Bandit;
        }

        /// <summary>
        /// Создание монстров Бандитов
        /// </summary>
        /// <param name="playerLevel">Уровень игрока</param>
        /// <returns>Лист Бандитов</returns>
        public static List<Enemy> CreateEnemyBandit(int playerLevel)
        {
            int count = Program.Random.Next(2, 5);
            int level = Program.Random.Next(1, 101);
            var enemyes = new List<Enemy>(count);
            for (int i = 0; i < count; i++)
            {
                if (level <= 70) enemyes.Add(new EnemyBandit(playerLevel));
                else if (level <= 80) enemyes.Add(new EnemyBandit(playerLevel + 1));
                else if (level <= 90) enemyes.Add(new EnemyBandit(playerLevel - 1));
                else if (level <= 95) enemyes.Add(new EnemyBandit(playerLevel + 2));
                else if (level <= 98) enemyes.Add(new EnemyBandit(playerLevel - 2));
                else enemyes.Add(new EnemyBandit(playerLevel + 3));
            }
            return enemyes;
        }
    }
}