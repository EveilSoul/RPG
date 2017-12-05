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
                        Window.PrintDangerous();

                        var enemy = Enemy.CreateEnemy(1);
                        while (Enemy.IsEnemyLive(enemy))
                        {
                            Window.PrintEnemy(enemy.Length, enemy);
                            


                            int index = 0; //ChoiceWeapons, use player.GetCharacteristics, later choice enemy
                            var bow = player.Bow;
                            var sword = player.GetSword(index);
                            var spell = player.GetSpell(index);
                            //нужен метод для выбора типа оружия для атаки и переопределение индекса
                            var numberOnEnemyAtsck = player.Attack(
                                enemy.Length, player.SelectType(), bow, spell, sword, index);
                            /* Для примера того, как это может еще работать
                             * var numberOnEnemyAtsck = player.Attack(
                                enemy.Length, Weapons.WeaponsType.Bow, bow, null, null, 0);

                             *  var numberOnEnemyAtsck = player.Attack(
                                enemy.Length, Weapons.WeaponsType.Spell, null, spell, null, 0);
                                либо в конце индекс(ы) противников

                             *  var numberOnEnemyAtsck = player.Attack(
                                enemy.Length, Weapons.WeaponsType.Sword, null, null, sword, index);
                             */
                            
                            Enemy.OnEnemyAtack(enemy, numberOnEnemyAtsck);
                            
                            //вывод атак(нужна проверка живы ли мостры и игрок)
                            Window.PrintOnEnemyAtack();
                            Window.PrintEnemy(enemy.Length, enemy);
                            Window.PrintEnemyAtack();
                            Window.PrintEnemy(enemy.Length, enemy);
                        }

                        Window.ClearMap();

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
