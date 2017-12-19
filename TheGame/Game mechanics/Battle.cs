using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Battle
    {
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
            CheckTasks(enemy, player);
            while (Enemy.IsEnemyLive(enemy) && player.IsLive)
            {
                Window.PrintEnemy(enemy);
                Window.PrintBattleCharacteristic(player, enemy);
                player.AddMana(player.Level);
                var numberOnEnemyAtsck = PlayerAttack(player, enemy.Count);

                for (int i = 0; i < enemy.Count; i++)
                    enemy[i].OnEnemyAtack(enemy[i], numberOnEnemyAtsck[i]);

                Enemy.CheckEnemyDie(enemy);
                Window.PrintOnEnemyAtack();
                Window.PrintEnemyAtack();

                for (int i = 0; i < enemy.Count; i++)
                    player.ApplyDamage(enemy[i].EnemyAttack());
            }

            Window.ClearMap(Window.BattleMap, Window.EnemySymble);

            if (player.IsLive)
                GetRewardForPlayer(player, reward.Item1, reward.Item2);
        }

        public static void CheckTasks(List<Enemy> enemy, Player player)
        {
            foreach (var e in enemy)
            {
                foreach (var task in player.Tasks)
                {
                    if (e.Type == task.Item2.EnemyType)
                        task.Item2.EnemyCountDied++;
                }
            }
        }

        public static void GetRewardForPlayer(Player player, int money, int skill)
        {
            player.AddMoney(money);
            player.BattleSkill += skill;
            while (player.NextLevelBorder <= player.BattleSkill)
            {
                Enemy.ChangeEnemyBorder(player.Level);
                player.ChangeBattleLevel();
            }
            Task.CheckTask(player);
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
