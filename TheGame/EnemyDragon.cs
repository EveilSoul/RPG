using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class EnemyDragon : Enemy
    {
        EnemyDragon()
        {
            this.Name = "Dragon";
            this.PowerAttack = 150;
            this.IsLive = true;
            this.Health = 500;
        }

        public static Enemy[] CreateEnemyDragon()
        {
            return new Enemy[] { new EnemyDragon() };
        }
    }
}
