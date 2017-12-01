using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class EnemyGoblin : Enemy
    {
        public EnemyGoblin()
        {
            this.Name = "Goblin";
            this.PowerAttack = 20;
            this.IsLive = true;
            this.Health = 50;
        }

        public static Enemy[] CreateEnemyGoblin()
        {
            int count = Program.Random.Next(2, 4);
            var enemyes = new Enemy[count];
            for (int i = 0; i < count; i++)
            {
                enemyes[i] = new EnemyGoblin();
            }
            return enemyes;
        }
    }
}
