using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Task
    {
        public string EnemyName;
        public int EnemyCount;
        public int EnemyCountDied;
        public int Reward;


        public Task(int playerLevel)
        {
            int rand = Program.Random.Next(1, 10);

            switch (rand)
            {
                case 1:
                    this.EnemyName = "Wolf";
                    this.EnemyCount = Program.Random.Next(10, 15 + playerLevel);
                    this.EnemyCountDied = 0;
                    this.Reward = Program.Random.Next(10 * this.EnemyCount, 30 * this.EnemyCount);
                    break;
                case 2:
                    this.EnemyName = "Goblin";
                    this.EnemyCount = Program.Random.Next(7, 12 + playerLevel);
                    this.EnemyCountDied = 0;
                    this.Reward = Program.Random.Next(12 * this.EnemyCount, 28 * this.EnemyCount);
                    break;
                case 3:
                    this.EnemyName = "Bear";
                    this.EnemyCount = Program.Random.Next(5, 10 + playerLevel);
                    this.EnemyCountDied = 0;
                    this.Reward = Program.Random.Next(18 * this.EnemyCount, 40 * this.EnemyCount);
                    break;
                case 4:
                    this.EnemyName = "Ork";
                    this.EnemyCount = Program.Random.Next(10, 15 + playerLevel);
                    this.EnemyCountDied = 0;
                    this.Reward = Program.Random.Next(20 * this.EnemyCount, 45 * this.EnemyCount);
                    break;
                case 5:
                    this.EnemyName = "Griffin";
                    this.EnemyCount = Program.Random.Next(3, 8 + playerLevel);
                    this.EnemyCountDied = 0;
                    this.Reward = Program.Random.Next(60 * this.EnemyCount, 80 * this.EnemyCount);
                    break;
                case 6:
                    this.EnemyName = "Triton";
                    this.EnemyCount = Program.Random.Next(4, 7 + playerLevel);
                    this.EnemyCountDied = 0;
                    this.Reward = Program.Random.Next(55 * this.EnemyCount, 85 * this.EnemyCount);
                    break;
                case 7:
                    this.EnemyName = "Bandit";
                    this.EnemyCount = Program.Random.Next(10, 17 + playerLevel);
                    this.EnemyCountDied = 0;
                    this.Reward = Program.Random.Next(22 * this.EnemyCount, 30 * this.EnemyCount);
                    break;
                case 8:
                    this.EnemyName = "DarkKnight";
                    this.EnemyCount = Program.Random.Next(3, 8 + playerLevel);
                    this.EnemyCountDied = 0;
                    this.Reward = Program.Random.Next(60 * this.EnemyCount, 80 * this.EnemyCount);
                    break;
                case 9:
                    this.EnemyName = "Golem";
                    this.EnemyCount = Program.Random.Next(4, 10 + playerLevel);
                    this.EnemyCountDied = 0;
                    this.Reward = Program.Random.Next(65 * this.EnemyCount, 90 * this.EnemyCount);
                    break;

            }
        }

        public void CheckTask(Player player)
        {
            foreach (var task in player.Tasks)
            {
                if (task.Item2.EnemyCountDied == task.Item2.EnemyCount)
                {
                    Console.Clear();
                    Console.WriteLine("Вы выполнили задание на убийство {0} {1}. \n" +
                    "Возвращайтесь в город с позицией ({2};{3}) чтобы забрать награду", task.Item2.EnemyCount, task.Item2.EnemyName, task.Item1.X, task.Item1.Y);
                    Window.PrintMap(Window.Map);
                }
            }
        }

        public static Task Copy(Player player, Task task)
        {
            return new Task(player.Level)
            {
                EnemyName = task.EnemyName,
                EnemyCount = task.EnemyCount,
                EnemyCountDied=0,
                Reward=task.Reward
            };
        }
    }
}
