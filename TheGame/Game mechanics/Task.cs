using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Task
    {
        // Тип монстра
        public Enemy.EnemyType EnemyType;
        // Количество монстров, которых нужно убить
        public int EnemyCount;
        // Количество убитых монстров
        public int EnemyCountDied;
        // Награда в монетах
        public int MoneyReward;
        // Награда в опыте
        public int SkillReward;

        /// <summary>
        /// Получение награды за выполнение задания
        /// </summary>
        /// <param name="min">Минимум</param>
        /// <param name="max">Максимум</param>
        /// <returns>Награда</returns>
        private int GetReward(int min, int max) => 
            Program.Random.Next(min * this.EnemyCount, max * this.EnemyCount);

        /// <summary>
        /// Создание задания
        /// </summary>
        /// <param name="playerLevel">Количество монстров зависит от уровня персонажа</param>
        public Task(int playerLevel)
        {
            int rand = Program.Random.Next(1, 10);

            switch (rand)
            {
                case 1:
                    this.EnemyType = Enemy.EnemyType.Wolf;
                    this.EnemyCount = Program.Random.Next(10, 15 + playerLevel);
                    this.EnemyCountDied = 0;
                    if (Program.Random.Next() % 2 == 0)
                        this.MoneyReward = GetReward(10, 30);
                    else this.SkillReward = GetReward(15, 40);
                    break;
                case 2:
                    this.EnemyType = Enemy.EnemyType.Goblin;
                    this.EnemyCount = Program.Random.Next(7, 12 + playerLevel);
                    this.EnemyCountDied = 0;
                    if (Program.Random.Next() % 2 == 0)
                        this.MoneyReward = GetReward(12, 28);
                    else this.SkillReward = GetReward(15, 35);
                    break;
                case 3:
                    this.EnemyType = Enemy.EnemyType.Bear;
                    this.EnemyCount = Program.Random.Next(5, 10 + playerLevel);
                    this.EnemyCountDied = 0;
                    if (Program.Random.Next() % 2 == 0)
                        this.MoneyReward = GetReward(18, 40);
                    else this.SkillReward = GetReward(20, 45);
                    break;
                case 4:
                    this.EnemyType = Enemy.EnemyType.Ork;
                    this.EnemyCount = Program.Random.Next(10, 15 + playerLevel);
                    this.EnemyCountDied = 0;
                    if (Program.Random.Next() % 2 == 0)
                        this.MoneyReward = GetReward(20, 45);
                    else this.SkillReward = GetReward(25, 45);
                    break;
                case 5:
                    this.EnemyType = Enemy.EnemyType.Griffin;
                    this.EnemyCount = Program.Random.Next(3, 8 + playerLevel);
                    this.EnemyCountDied = 0;
                    if (Program.Random.Next() % 2 == 0)
                        this.MoneyReward = GetReward(60, 80);
                    else this.SkillReward = GetReward(60, 90);
                    break;
                case 6:
                    this.EnemyType = Enemy.EnemyType.Triton;
                    this.EnemyCount = Program.Random.Next(4, 7 + playerLevel);
                    this.EnemyCountDied = 0;
                    if (Program.Random.Next() % 2 == 0)
                        this.MoneyReward = GetReward(55, 85);
                    else this.SkillReward = GetReward(55, 90);
                    break;
                case 7:
                    this.EnemyType = Enemy.EnemyType.Bandit;
                    this.EnemyCount = Program.Random.Next(10, 17 + playerLevel);
                    this.EnemyCountDied = 0;
                    if (Program.Random.Next() % 2 == 0)
                        this.MoneyReward = GetReward(22, 35);
                    else this.SkillReward = GetReward(30, 40);
                    break;
                case 8:
                    this.EnemyType = Enemy.EnemyType.DarkKnight;
                    this.EnemyCount = Program.Random.Next(3, 8 + playerLevel);
                    this.EnemyCountDied = 0;
                    if (Program.Random.Next() % 2 == 0)
                        this.MoneyReward = GetReward(60, 80);
                    else this.SkillReward = GetReward(50, 90);
                    break;
                case 9:
                    this.EnemyType = Enemy.EnemyType.Golem;
                    this.EnemyCount = Program.Random.Next(4, 10 + playerLevel);
                    this.EnemyCountDied = 0;
                    if (Program.Random.Next() % 2 == 0)
                        this.MoneyReward = GetReward(65, 90);
                    else this.SkillReward = GetReward(70, 85);
                    break;
            }
        }

        /// <summary>
        /// Проверка на то, выполнено ли задание
        /// </summary>
        /// <param name="player">Игрок</param>
        public static void CheckTask(Player player)
        {
            foreach (var task in player.Tasks)
            {
                if (task.Item2.EnemyCountDied >= task.Item2.EnemyCount)
                {
                    Console.Clear();
                    Console.WriteLine("Вы выполнили задание на убийство {0} {1}. \n" +
                    "Возвращайтесь в город с позицией ({2};{3}) чтобы забрать награду", 
                    task.Item2.EnemyCount, task.Item2.EnemyType, task.Item1.X, task.Item1.Y);
                    Console.ReadKey();
                }
            }
            Window.PrintMap(Window.Map);
        }

        public override string ToString()
        {
            if (this.SkillReward == 0)
                return String.Format("Задание даст {0} монет за победу {1} противников {2}", this.MoneyReward, this.EnemyCount, this.EnemyType);
            else return String.Format("Задание даст {0} опыта за победу {1} противников {2}", this.SkillReward, this.EnemyCount, this.EnemyType);
        }

        public string GetStatistic() =>
            this.ToString() + String.Format("\nОсталось победить {0} противников", this.EnemyCount - this.EnemyCountDied);
    }
}
