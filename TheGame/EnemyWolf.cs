using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class EnemyWolf : Enemy
    {
        public EnemyWolf()
        {

            this.Name = "Wolf";
            this.PowerAttack = 10;
            this.IsLive = true;
            this.Health = 50;
            
        }

        public static Enemy[] CreateEnemyWolf()
        {
            int count = Program.Random.Next(3, 6);
            var enemyes = new Enemy[count];
            for (int i = 0; i < count; i++)
            {
                enemyes[i] = new EnemyWolf();
            }
            return enemyes;
        }
    }
}
