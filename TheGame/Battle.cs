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

        public static void GoBattle(Player player)
        {
            if (MayNewBattle(theLastBattlePosition, player.Position) || !TheBattleWas)
            {
                if (!IsEnemy || Enemy.IsEnemyFar(enemyPosition, player.Position))
                {
                    enemyPosition = Enemy.EnemyGenerationPosition(player.Position);
                    IsEnemy = true;
                }
                else
                {
                    if (Enemy.IsEnemyNear(enemyPosition, player.Position))
                    {

                        var enemy = Enemy.CreateEnemy(player.Level);
                        while (Enemy.IsEnemyLive(enemy) && player.CurrentHealth > 0)
                        {
                            Window.PrintEnemy(enemy);
                            Console.WriteLine(player.CurrentHealth);


                            int index = 0; //ChoiceWeapons, use player.GetCharacteristics, later choice enemy
                            var bow = player.Bow;
                            var sword = player.GetSword(index);
                            var spell = player.GetSpell(index);
                            //нужен метод для выбора типа оружия для атаки и переопределение индекса
                            var numberOnEnemyAtsck = player.Attack(
                                enemy.Count, player.SelectType(), bow, spell, sword, index);
                            
                            Enemy.OnEnemyAtack(enemy, numberOnEnemyAtsck);
                            
                            Window.PrintOnEnemyAtack();
                            Window.PrintEnemy(enemy);

                            for (int i = 0; i < enemy.Count; i++)
                                player.ApplyDamage(enemy[i].EnemyAttack());

                           
                            if (Enemy.IsEnemyLive(enemy))
                            {
                                Window.PrintEnemyAtack();
                                Window.PrintEnemy(enemy);
                            }
                        }

                        Window.ClearMap();
                        Window.IsBattle = false;

                        TheBattleWas = true;
                        IsEnemy = false;
                        theLastBattlePosition = player.Position;
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
