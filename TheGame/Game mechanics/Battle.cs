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
        public static bool TheFirstBattleWas = false;
        public static bool TheTreasureBattleWas = false;

        public static int[] GetEnemyIndexs()
        {
            Console.WriteLine("Введите индексы противников через пробел");
            var indexs = Console.ReadLine().Split(' ');
            var result = new int[indexs.Length];
            for (int i = 0; i < indexs.Length; i++)
                result[i] = Program.Parse(indexs[i], 0);
            return result;
        }

        public static void GoBattle(Player player, List<Enemy> enemy)
        {
            var reward = Enemy.GetReward(enemy);
            while (Enemy.IsEnemyLive(enemy) && player.IsLive)
            {
                Window.PrintEnemy(enemy);
                Window.PrintBattleCharacteristic(player, enemy);
                var numberOnEnemyAtsck = PlayerAttack(player, enemy.Count);

                for (int i = 0; i < enemy.Count; i++)
                    enemy[i].OnEnemyAtack(enemy[i], numberOnEnemyAtsck[i]);
                
                Enemy.CheckEnemyDie(enemy);
                Window.PrintOnEnemyAtack();
                Window.PrintEnemy(enemy);

                for (int i = 0; i < enemy.Count; i++)
                    player.ApplyDamage(enemy[i].EnemyAttack());
            }

            Window.ClearMap(Window.BattleMap, Window.EnemySymble);
            Window.ClearMap(Window.Map, Window.EnemySymble);
            Window.ClearMap(Window.Map, Window.TreasureSymble);
            Window.IsBattle = false;
            TheBattleWas = true;
            TheFirstBattleWas = true;
            TheTreasureBattleWas = true;
            IsEnemy = false;
            Enemy.EnemyExist = false;
            Treasure.EnemyExist = false; //metod

            if (player.IsLive)
            {
                player.AddMoney(reward.Item1);
                player.BattleSkill += reward.Item2;
                while (player.NextLevelBorder <= player.BattleSkill)
                    player.ChangeBattleLevel(); //metod
            }
            
        }

        public static bool MayNewBattle(ObjectStructures.Position lastPosition, ObjectStructures.Position newPosition)
        {
            return (int)(Math.Sqrt((lastPosition.X - newPosition.X) * (lastPosition.X - newPosition.X) +
                (newPosition.Y - lastPosition.Y) * (newPosition.Y - lastPosition.Y))) > 10;
        }

        public static int[] PlayerAttack(Player player, int enemyCount)
        {
            var type = player.SelectType();
            Window.PrintArray(player.GetCharacteristicsOfWeapons(type));
            int index = Program.Parse(Console.ReadLine(), 0);
            if (index == 333)
                return player.SuperAttack(enemyCount);
            var bow = player.Bow;
            var sword = player.GetSword(index < player.Swords.Count ? index : 0);
            var spell = player.GetSpell(index < player.Spells.Count ? index : 0);
            var enInd = type == Weapons.WeaponsType.Sword ? GetEnemyIndexs() : new[] { 0 };
            return player.Attack(enemyCount, type, bow, spell, sword, enInd);
        }
    }
}
