using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Battle
    {
        public static bool IsEnemy = false;
        public static bool TheBattleWas = false;
        public static ObjectStructures.Position enemyPosition = new ObjectStructures.Position { X = 0, Y = 0 };
        public static ObjectStructures.Position theLastBattlePosition = new ObjectStructures.Position { X = 0, Y = 0 };

        public static void GoBattle(ObjectStructures.Position playerPosition)
        {
            if (MayNewBattle(theLastBattlePosition, playerPosition) || !TheBattleWas)
            {
                if (!IsEnemy || Enemy.IsEnemyFar(enemyPosition, playerPosition))
                {
                    enemyPosition = Enemy.EnemyGenerationPosition(playerPosition);
                    IsEnemy = true;
                }
                else
                {
                    if (Enemy.IsEnemyNear(enemyPosition, playerPosition))
                    {
                        //Window.PrintDangerous();
                        
                        var enemy = Enemy.CreateEnemy(1);
                        while (Enemy.IsEnemyLive(enemy))
                        {
                            Window.PringEnemy(enemy.Length, enemy);

                            var numberOnEnemyAtsck = new int[enemy.Length];
                            for (int i = 0; i < numberOnEnemyAtsck.Length; i++)
                            {
                                numberOnEnemyAtsck[i] = i;
                            }

                            Enemy.OnEnemyAtack(10, enemy, numberOnEnemyAtsck);
                            Console.ReadLine();
                        }

                        Window.ClearMap();

                        
                        TheBattleWas = true;
                        IsEnemy = false;
                        theLastBattlePosition = playerPosition;

                    }
                }
            
            }
        }

        public static bool MayNewBattle(ObjectStructures.Position lastPosition, ObjectStructures.Position newPosition)
        {
            return (int)(Math.Sqrt((lastPosition.X - newPosition.X) * (lastPosition.X - newPosition.X) +
                (newPosition.Y - lastPosition.Y) * (newPosition.Y - lastPosition.Y))) > 10;
        }

    }
}
