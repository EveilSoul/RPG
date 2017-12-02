﻿using System;
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
            this.Name = "Wolf";
            this.PowerAttack = 10 + level * level/2;
            this.IsLive = true;
            this.Health = 48 + 2*level*level;
            
        }

        public static Enemy[] CreateEnemyWolf(int playerLevel = 1)
        {
            int count = Program.Random.Next(3, 6);
            int level = Program.Random.Next(1, 101);
            var enemyes = new Enemy[count];
            for (int i = 0; i < count; i++)
            {
                if (level<=70) enemyes[i] = new EnemyWolf(playerLevel);
                else if (level <= 80) enemyes[i] = new EnemyWolf(playerLevel + 1);
                else if (level <= 90) enemyes[i] = new EnemyWolf(playerLevel - 1);
                else if (level <= 95) enemyes[i] = new EnemyWolf(playerLevel + 2);
                else if (level <= 98) enemyes[i] = new EnemyWolf(playerLevel - 2);
                else enemyes[i] = new EnemyWolf(playerLevel + 3);
            }
            return enemyes;
        }
    }
}