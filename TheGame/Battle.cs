using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Battle
    {
        public bool IsEnemy = false;
        public bool theBattleWas = false;
        public ObjectStructures.Position enemyPosition = new ObjectStructures.Position { X = 0, Y = 0 };
        public ObjectStructures.Position theLastButtlePosition = new ObjectStructures.Position { X = 0, Y = 0 };

        public void GoBattle(ObjectStructures.Position playerPosition)
        {
            if (!this.IsEnemy && MayNewBattle(this.theLastButtlePosition, playerPosition))
            {
                enemyPosition = Enemy.EnemyGenerationPosition(playerPosition);
            }
            else
            {
                if (Enemy.IsEnemyNear(enemyPosition, playerPosition))
                {
                    //BATLE
                    this.theBattleWas = true;
                    this.IsEnemy = false;
                    this.theLastButtlePosition = playerPosition;

                }
            }
        }

        public static bool MayNewBattle(ObjectStructures.Position lastPosition, ObjectStructures.Position newPosition)
        {
            return (int)(Math.Sqrt((lastPosition.X - newPosition.X) * (lastPosition.X - newPosition.X) +
                (newPosition.Y - lastPosition.Y) * (newPosition.Y - lastPosition.Y))) > 30;
        }

    }
}
