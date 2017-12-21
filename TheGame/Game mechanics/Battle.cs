using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    /// <summary>
    /// Класс для битвы
    /// </summary>
    class Battle
    {
        /// <summary>
        /// Выбор врагов, которых будем бить
        /// </summary>
        /// <returns> Массив с индексами врагов, которым будет наноситься урон </returns>
        public static int[] GetEnemyIndexs()
        {
            Console.WriteLine("Введите индексы противников через пробел");
            var indexs = Console.ReadLine().Split(' ');
            var result = new int[indexs.Length];
            for (int i = 0; i < indexs.Length; i++)
                result[i] = Program.Parse(indexs[i], 0);
            return result;
        }

        /// <summary>
        /// Собственно, сама битва
        /// </summary>
        /// <param name="player"> Игрок </param>
        /// <param name="enemy"> Лист с противниками </param>
        public static void GoBattle(Player player, List<Enemy> enemy)
        {
            // Определяем награду игрока
            var reward = Enemy.GetReward(enemy);
            // Смотрим, есть ли среди противников кто-то, на кого он брал задание
            CheckTasks(enemy, player);

            while (Enemy.IsEnemyLive(enemy) && player.IsLive)
            {
                Window.PrintEnemy(enemy);
                Window.PrintBattleCharacteristic(player, enemy);
                player.AddMana(player.Level);
                // определяем действие игрока
                var enemyDamage = SelectPlayerAction(player, enemy.Count);
                // проверка на целостность оружия
                player.CheckWeapons();
                Window.PrintEnemyAtack();

                // противники наносят урон
                ApplyDamageForPlayer(player, enemyDamage, enemy);

                // если игрок защитился, переходим к следующему ходу
                if (enemyDamage.Length == 0)
                    continue;

                // если игрок одновременно защищается и атакует, переопределяем атаку
                if (enemyDamage[0] == -1)
                {
                    enemyDamage = PlayerWeaponsAttack(player, enemy.Count);
                    for (int i = 0; i < enemyDamage.Length; i++)
                        enemyDamage[i] = (int)(enemyDamage[i] * Program.Random.NextDouble() / 2 + 0.5);
                }

                for (int i = 0; i < enemy.Count; i++)
                    enemy[i].OnEnemyAtack(enemy[i], enemyDamage[i]);

                // проверяем, есть ли жертвы среди противников
                Enemy.CheckEnemyDie(enemy);
                Window.PrintOnEnemyAtack();
            }

            // очищаем карту
            Window.ClearMap(Window.BattleMap, Window.EnemySymble);

            // если игрок выжил, то даем ему награду - честно заработал
            if (player.IsLive)
                GetRewardForPlayer(player, reward.Item1, reward.Item2);
        }

        /// <summary>
        /// Нанесение игроку урона
        /// </summary>
        /// <param name="player"> Игрок </param>
        /// <param name="enemyDamage"> Массив с уроном </param>
        /// <param name="enemy"> Противник </param>
        private static void ApplyDamageForPlayer(Player player, int[] enemyDamage, List<Enemy> enemy)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                if (enemyDamage.Length == 0)
                {
                    // Если игрок решил защититься
                    player.UseProtection(enemy[i].EnemyAttack());
                }
                else if (enemyDamage[0] == -1)
                {
                    // Игрок совмещает атаку и защиту
                    player.UseProtection(enemy[i].EnemyAttack(), true);
                }
                else player.ApplyDamage(enemy[i].EnemyAttack());
                // Последнее - игрок не защищается, а только атакует
            }
        }

        // Проверка, есть ли среди противников тот, за кем охотится игрок
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

        // Игрок получает награду - опыт и деньги
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

        /// <summary>
        /// Атака оружием игрока. Если во время выбораоружия ввести 333 - вызываем супер-атаку
        /// </summary>
        /// <param name="player"> Игрок </param>
        /// <param name="enemyCount"> Количество противников </param>
        /// <returns> Массив с уроном для противников </returns>
        public static int[] PlayerWeaponsAttack(Player player, int enemyCount)
        {
            var type = player.SelectType();
            Window.PrintArray(player.GetCharacteristicsOfWeapons(type));
            int index = Program.Parse(Console.ReadLine(), 0);
            if (index == 333)
                return player.SuperAttack(enemyCount);
            /* Мы выбрали какой-либо тип оружия, но метод атаки является обобщенным,
             * он принимает в себя не один вид оружия, а сразу все три.
             * И в зависимости от выбранного типа атакует этим оружием. */
            var bow = player.Bow;
            var sword = player.GetSword(index < player.Swords.Count ? index : 0);
            var spell = player.GetSpell(index < player.Spells.Count ? index : 0);
            // массив с индексами противников, которых атакуем
            var enInd = type == Weapons.WeaponsType.Sword ? GetEnemyIndexs() : new[] { 0 };

            return player.Attack(enemyCount, type, bow, spell, sword, enInd);
        }

        // выбор действия игрока
        public static int[] SelectPlayerAction(Player player, int enemyCount)
        {
            int choise = player.SelectAtionInBattle();
            switch (choise)
            {
                case 1:
                    // атака во всю силу
                    return PlayerWeaponsAttack(player, enemyCount);
                case 2:
                    // защита
                    return new int[0];
                case 3:
                    // совмещение атаки и защиты
                    return new int[] { -1 };
            }
            return null;
        }
    }
}
