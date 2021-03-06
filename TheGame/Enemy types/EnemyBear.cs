﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class EnemyBear : Enemy
    { 

        public EnemyBear(int level)
        {
            this.Name = "Медведь";
            this.PowerAttack = 20 + level * level / 2;
            this.IsLive = true;
            this.Health = 98 + 2 * level * level;
            this.Accuracy = 0.85f;
            this.MoneyReward = 15 + 2 * level;
            this.SkillReward = GetSkill(30, 80, 12, level);
            this.Mimicry = false;
            this.Type = EnemyType.Bear;
        }

        /// <summary>
        /// Создание монстров Медведь
        /// </summary>
        /// <param name="playerLevel">Уровень игрока</param>
        /// <returns>Лист Медведей</returns>
        public static List<Enemy> CreateEnemyBear(int playerLevel)
        {
            int level = Program.Random.Next(1, 101);

            if (level <= 70) return new List<Enemy> { new EnemyBear(playerLevel) };
            else if (level <= 80) return new List<Enemy> { new EnemyBear(playerLevel + 1) };
            else if (level <= 90) return new List<Enemy> { new EnemyBear(playerLevel - 1) };
            else if (level <= 95) return new List<Enemy> { new EnemyBear(playerLevel + 2) };
            else if (level <= 98) return new List<Enemy> { new EnemyBear(playerLevel - 2) };
            return new List<Enemy> { new EnemyBear(playerLevel + 3) };

        }
    }
}
