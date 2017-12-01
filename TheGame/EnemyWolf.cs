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

            this.Name = "Wolf";
            this.PowerAttack = 10;
            this.IsLive = true;
            this.Health = 50;
            
        }

        public static Enemy[] CreateEnemyWolf(int playerLevel)
        {
            int count = Program.Random.Next(3, 6);
            int level = Program.Random.Next(1, 101);
            var enemyes = new Enemy[count];
            for (int i = 0; i < count; i++)
            {
                //??????????? все правильно?
                if (level<=70) enemyes[i] = new EnemyWolf(playerLevel);
                if (level > 70 && level <= 80) enemyes[i] = new EnemyWolf(playerLevel + 1);
                if (level > 80 && level <= 90) enemyes[i] = new EnemyWolf(playerLevel - 1);
                if (level > 90 && level <= 95) enemyes[i] = new EnemyWolf(playerLevel + 2);
                if (level > 95 && level <= 98) enemyes[i] = new EnemyWolf(playerLevel - 2);
                else enemyes[i] = new EnemyWolf(playerLevel + 3);
            }
            return enemyes;
        }
    }
}
