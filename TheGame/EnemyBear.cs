using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class EnemyBear : Enemy
    { 
        public EnemyBear()
        {
            this.Name = "Bear";
            this.PowerAttack = 20;
            this.IsLive = true;
            this.Health = 100;
        }

        public static Enemy[] CreateEnemyBear()
        {
            return new Enemy[] { new EnemyBear() };
        }
    }
}
