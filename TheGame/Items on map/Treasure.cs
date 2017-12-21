using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TheGame
{
    class Treasure
    {
        // Позиция клада
        public MainGameStructures.Position Position;
        // Нападут ли на игрока монстры
        public bool IsEnemy;
        // Последняя позиция генерации клада
        public static MainGameStructures.Position TheLastTreasurePosition;
        // Существование клада
        public static bool TreasureExist;
        // Лист с монстрами
        public List<Enemy> enemy;

        /// <summary>
        /// Генерация позиции клада
        /// </summary>
        /// <param name="playerPosition">Позиция игрока</param>
        /// <returns>Позиция клада</returns>
        public MainGameStructures.Position TreasureGenerationPosition(MainGameStructures.Position playerPosition)
        {
            var treasurePosition = new MainGameStructures.Position
            {
                X = Program.Random.Next(playerPosition.X - Window.MapSizeX / 3,
                playerPosition.X + Window.MapSizeX / 3),
                Y = Program.Random.Next(playerPosition.Y - Window.MapSizeY / 3,
                playerPosition.Y + Window.MapSizeY / 3)
            };

            return treasurePosition;
        }

        /// <summary>
        /// Создание клада (позиции, монстров)
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="enemyPosition">Позиция монстро(для того, чтобы не было конфликтов с городами)</param>
        public Treasure(Player player, MainGameStructures.Position enemyPosition)
        {
            this.Position = TreasureGenerationPosition(player.Position);
            TreasureExist = true;

            bool exictPosition = false;
            do
            {
                exictPosition = false;
                foreach (var city in Program.Cities)
                {
                    if (city.Position.X == this.Position.X && city.Position.Y == this.Position.Y)
                    {
                        this.Position = TreasureGenerationPosition(player.Position);
                        exictPosition = true;
                        break;
                    }
                }
                if (enemyPosition.X==this.Position.X && enemyPosition.Y==this.Position.Y)
                {
                    this.Position = TreasureGenerationPosition(player.Position);
                    exictPosition = true;
                }
            } while (exictPosition);

            var rand = Program.Random.Next(0, 101 - (player.Level * (int)Math.Sqrt(player.Level) <= 100 ? player.Level * (int)Math.Sqrt(player.Level) : 1));
            if (rand < 80) this.IsEnemy = true;
            else this.IsEnemy = false;

            this.enemy = CreateEnemyForTreasure(player.Level);

        }

        /// <summary>
        /// Проверка столкновения игрока и клада
        /// </summary>
        /// <param name="player">Игрок</param>
        public void CkeckPlayer(Player player)
        {
            if (TreasureExist && player.Position.X == this.Position.X && player.Position.Y == this.Position.Y)
            {
                if (this.IsEnemy) Battle.GoBattle(player, this.enemy);
                player.AddMoney(GetReward(player.Level));
                Thread.Sleep(1000);
                Window.ClearMap(Window.Map, Window.TreasureSymble);
                TreasureExist = false;
            }
        }

        /// <summary>
        /// Создание монстров в зависимости от уровня игрока
        /// </summary>
        /// <param name="playerLevel">Уровень игрока</param>
        /// <returns>Лист с монстрами</returns>
        public List<Enemy> CreateEnemyForTreasure(int playerLevel)
        {
            int rand = Program.Random.Next(1 + Enemy.LowerBorderEnemyGeneration - 3*(playerLevel/2), 151 + Enemy.SupremeBorderEnemyGeneration + 5*(playerLevel/2));
            TreasureExist = true;
            rand %= 300;
            playerLevel++;

            if (rand < 1 + Enemy.LowerBorderEnemyGeneration) return EnemyMix.CreateEnemyMix(playerLevel);
            if (rand > 150 + Enemy.SupremeBorderEnemyGeneration) return EnemyDragon.CreateEnemyDragon(playerLevel);
            if (rand > 0 && rand <= 50) return EnemyBear.CreateEnemyBear(playerLevel);
            if (rand > 50 && rand <= 100) return EnemyOrk.CreateEnemyOrk(playerLevel); 
            if (rand > 100 && rand <= 130) return EnemyGriffin.CreateGriffin(playerLevel); 
            if (rand > 130 && rand <= 150) return EnemyTriton.CreateEnemyTriton(playerLevel); 
            if (rand > 150 && rand <= 200) return EnemyBandit.CreateEnemyBandit(playerLevel); 
            if (rand > 200 && rand <= 250) return EnemyDarkKnight.CreateEnemyDarkKnight(playerLevel);
            return EnemyGolem.CreateEnemyGolem(playerLevel);

        }

        /// <summary>
        /// Награда за клад
        /// </summary>
        /// <param name="playerLevel">Уровень игрока</param>
        /// <returns>Количество монет</returns>
        public int GetReward(int playerLevel)
        {
            var reward = Program.Random.Next(80 + playerLevel * playerLevel, 150 + playerLevel * playerLevel * playerLevel);
            Console.Clear();
            Console.WriteLine("Вы получили {0} монет.", reward);
            return reward;
        }

        /// <summary>
        /// Проверка того, можно ли создавать новых монстров
        /// </summary>
        /// <param name="lastPosition">Предыдущая позиция генерации клада</param>
        /// <param name="newPosition">Текущая позиция</param>
        /// <returns>Можно ли генерировать</returns>
        public bool MayNewTreasure(MainGameStructures.Position lastPosition, MainGameStructures.Position newPosition)
        {
            return (int)(Math.Sqrt((lastPosition.X - newPosition.X) * (lastPosition.X - newPosition.X) +
                (newPosition.Y - lastPosition.Y) * (newPosition.Y - lastPosition.Y))) > 30;
        }
    }
}
