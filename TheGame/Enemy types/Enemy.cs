using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Enemy
    {
        // Имя монстра
        public string Name;
        // Жив ли монстр
        public bool IsLive;
        // Сила атаки
        public int PowerAttack;
        // Здоровье
        public int Health;
        // Точность попадания
        public float Accuracy = 0.9f;
        // Позиция
        public ObjectStructures.Position Position;
        // Количество денег за убийство монстра
        public int MoneyReward = 5;
        // Количество скила за убийство монстра
        public int SkillReward = 10;
        // Скртный ли монстр
        public bool Mimicry;
        // Последняя позиция генерации монстров
        public static ObjectStructures.Position TheLastEnemyPosition;
        // Существование монстров
        public static bool EnemyExist = false;
        // Нижняя граница диапазона создания монстров
        public static int LowerBorderEnemyGeneration = 0;
        // Верхняя граница диапазона создания монстров
        public static int SupremeBorderEnemyGeneration = 0;

        public static int GetSkill(int offset, int divider, int minValue, int level) =>
            (int)Math.Pow(30 - level, 2) / divider + minValue;

        /// <summary>
        /// Получение награды за монстров
        /// </summary>
        /// <param name="enemy">лист монстров</param>
        /// <returns>Кортеж из количества монет и количества скилла</returns>
        public static Tuple<int, int> GetReward(List<Enemy> enemy)
        {
            int money = 0;
            int skill = 0;
            foreach (var e in enemy)
            {
                money += e.MoneyReward;
                skill += e.SkillReward;
            }
            return Tuple.Create(money, skill);
        }

        /// <summary>
        /// Атака на монстра
        /// </summary>
        /// <param name="enemy">монстр</param>
        /// <param name="damageAtackEnemy">урон,который ему наносится</param>
        public virtual void OnEnemyAtack(Enemy enemy, int damageAtackEnemy)
        {
            enemy.Health -= damageAtackEnemy;
        }

        /// <summary>
        /// Монстр атакует
        /// </summary>
        /// <returns>сила атаки</returns>
        public int EnemyAttack()
        {
            if (Program.Random.NextDouble() <= this.Accuracy && this.IsLive)
                return this.PowerAttack + Program.Random.Next(-3, 4);
            else return 0;
        }

        /// <summary>
        /// Удаление из листа монтров, чье здоровье меньше 0
        /// </summary>
        /// <param name="enemy">лист монстров</param>
        public static void CheckEnemyDie(List<Enemy> enemy)
        {
            for (int i = enemy.Count - 1; i >= 0; i--)
                if (enemy[i].Health <= 0)
                    enemy.RemoveAt(i);
        }

        /// <summary>
        /// Проверка того, жив ли еще хоть один монстр
        /// </summary>
        /// <param name="enemy">лист монстров</param>
        public static bool IsEnemyLive(List<Enemy> enemy)
        {
            bool result = false;
            for (int i = 0; i < enemy.Count; i++)
            {
                result = result || enemy[i].IsLive;
            }
            return result;
        }

        /// <summary>
        /// Генерация монстра относительно позиции игрока на карте
        /// </summary>
        /// <param name="playerPosition">позиция игрока</param>
        /// <returns>позиция монстра</returns>
        public static ObjectStructures.Position EnemyGenerationPosition(ObjectStructures.Position playerPosition)
        {
            var enemyPosition = new ObjectStructures.Position
            {
                X = Program.Random.Next(playerPosition.X - Window.MapSizeX / 2,
                playerPosition.X + Window.MapSizeX / 2),
                Y = Program.Random.Next(playerPosition.Y - Window.MapSizeY / 2,
                playerPosition.Y + Window.MapSizeY / 2)
            };
            return enemyPosition;
        }

        /// <summary>
        /// Проверка того, находится ли игрок в зоне поражения невидимых монстров
        /// </summary>
        /// <param name="enemyPosition">позиция монстров</param>
        /// <param name="playerPosition">позиция игрока</param>
        /// <returns>Находится ли игрок в зоне поражения</returns>
        public static bool IsEnemyNear(ObjectStructures.Position enemyPosition, ObjectStructures.Position playerPosition)
        {
            return Math.Abs(enemyPosition.X - playerPosition.X) <= Window.MapSizeX / 4 &&
                Math.Abs(enemyPosition.Y - playerPosition.Y) <= Window.MapSizeY / 4;
        }

        /// <summary>
        /// Создание монстров в зависимости от уровня игрока
        /// </summary>
        /// <param name="playerLevel">Уровень игрока</param>
        /// <param name="playerPosition">Позиция игрока</param>
        /// <returns>позиция монстров и лист с ними</returns>
        public static Tuple<ObjectStructures.Position, List<Enemy>> CreateEnemy(int playerLevel, ObjectStructures.Position playerPosition)
        {
            EnemyExist = true;
            var enemyPosition = EnemyGenerationPosition(playerPosition);
            bool exictPosition = false;
            do
            {
                exictPosition = false;
                foreach (var city in Program.Cities)
                {
                    if (city.Position.X == enemyPosition.X && city.Position.Y == enemyPosition.Y)
                    {
                        enemyPosition = EnemyGenerationPosition(playerPosition);
                        exictPosition = true;
                        break;
                    }
                }
            } while (exictPosition);

            //увеличивается вероятность выпадения дракона и смешаных монстров в зависиости от уровня
            int rand = Program.Random.Next(1 + LowerBorderEnemyGeneration - 2*playerLevel, 151 + SupremeBorderEnemyGeneration + 3*playerLevel);
            rand %= 400;

            if (rand < 1 + LowerBorderEnemyGeneration) return Tuple.Create(enemyPosition, EnemyMix.CreateEnemyMix(playerLevel));
            if (rand > 150 + SupremeBorderEnemyGeneration) return Tuple.Create(enemyPosition, EnemyDragon.CreateEnemyDragon(playerLevel));
            if (rand >= 1 && rand <= 50) return Tuple.Create(enemyPosition, EnemyWolf.CreateEnemyWolf(playerLevel));
            if (rand > 50 && rand <= 100) return Tuple.Create(enemyPosition, EnemyGoblin.CreateEnemyGoblin(playerLevel));
            if (rand > 100 && rand <= 150) return Tuple.Create(enemyPosition, EnemyBear.CreateEnemyBear(playerLevel));
            if (rand > 150 && rand <= 200) return Tuple.Create(enemyPosition, EnemyOrk.CreateEnemyOrk(playerLevel)); 
            if (rand > 200 && rand <= 220) return Tuple.Create(enemyPosition, EnemyGriffin.CreateGriffin(playerLevel)); 
            if (rand > 220 && rand <= 250) return Tuple.Create(enemyPosition, EnemyTriton.CreateEnemyTriton(playerLevel));
            if (rand > 250 && rand <= 300) return Tuple.Create(enemyPosition, EnemyBandit.CreateEnemyBandit(playerLevel));
            if (rand > 300 && rand <= 350) return Tuple.Create(enemyPosition, EnemyDarkKnight.CreateEnemyDarkKnight(playerLevel));
            return Tuple.Create(enemyPosition, EnemyGolem.CreateEnemyGolem(playerLevel));
        }

        /// <summary>
        /// Проверка столкновения игрока и монстров
        /// </summary>
        /// <param name="enemy">монстры</param>
        /// <param name="player">игрок</param>
        /// <param name="enemyPosition">позиция монстров</param>
        public static void CheckPlayer(List<Enemy> enemy, Player player, ObjectStructures.Position enemyPosition)
        {
            if (EnemyExist && enemyPosition.X == player.Position.X && enemyPosition.Y == player.Position.Y && !enemy[0].Mimicry)
            {
                Battle.GoBattle(player, enemy);
                Window.ClearMap(Window.Map, Window.EnemySymble);
                Enemy.EnemyExist = false;
            }
            else if (EnemyExist && IsEnemyNear(enemyPosition, player.Position) && enemy[0].Mimicry)
            {
                Battle.GoBattle(player, enemy);
                Enemy.EnemyExist = false;
            }
        }

        /// <summary>
        /// Можно ли создавать нового монтра (достаточно ли далеко отошли от предыдущего)
        /// </summary>
        /// <param name="lastPosition">предыдущая позиция</param>
        /// <param name="newPosition">текущая позиция</param>
        /// <returns>Можно ли генерировать</returns>
        public static bool MayNewEnemy(ObjectStructures.Position lastPosition, ObjectStructures.Position newPosition)
        {
            return (int)(Math.Sqrt((lastPosition.X - newPosition.X) * (lastPosition.X - newPosition.X) +
                (newPosition.Y - lastPosition.Y) * (newPosition.Y - lastPosition.Y))) > 15;
        }

        public static void ChangeEnemyBorder(int playerLevel)
        {
            LowerBorderEnemyGeneration += (int)(45 / Math.Sqrt(playerLevel));
            SupremeBorderEnemyGeneration += (int)(45 / Math.Sqrt(playerLevel));
        }

    }
}
