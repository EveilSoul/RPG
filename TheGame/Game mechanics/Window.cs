﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TheGame
{
    class Window
    {
        // ширина карты
        public static int MapSizeX = 31;
        // высота карты
        public static int MapSizeY = 15;
        // ширина окна
        public static int WindowSizeX = 61;
        // высота окна
        public static int WindowSizeY = 18;
        // мирная карта
        public static char[,] Map = new char[MapSizeY, MapSizeX];
        // карта во время боя
        public static char[,] BattleMap = new char[MapSizeY, MapSizeX];
        // символ игрока
        public static char PlayerSymble = '@';
        // символ монстра
        public static char EnemySymble = '*';
        // символ города
        public static char CitySymble = '#';
        // символ клада
        public static char TreasureSymble = '$';
        // координата Х для генерации монстров на боевой карте
        public static int EnemyGeneration = 4;
        // количество монстров
        public static int EnemyCount;




        public static void PrintArray(string[] array)
        {
            if (array != null)
                foreach (var t in array)
                    if (t != null)
                        Console.WriteLine(t);
        }

        /// <summary>
        /// Задается размер и цвет консоли
        /// </summary>
        public static void DrowWindow(ConsoleColor back = ConsoleColor.DarkGreen, ConsoleColor front = ConsoleColor.Black)
        {
            Console.Clear();
            Console.SetWindowSize(WindowSizeX + 1, WindowSizeY + 10);
            Console.SetBufferSize(WindowSizeX + 1, WindowSizeY + 10);
            Console.BackgroundColor = back;
            Console.ForegroundColor = front;
        }

        /// <summary>
        /// Вывод карты на консоль
        /// </summary>
        /// <param name="map">карта, которую нужно вывести</param>
        public static void PrintMap(char[,] map)
        {
            for (int i = 0; i < MapSizeY; i++)
            {
                for (int j = 0; j < MapSizeX; j++)
                {
                    Console.Write(map[i, j]);
                    if (j == MapSizeX - 1) Console.Write('|');
                }
                Console.WriteLine();
            }


        }

        /// <summary>
        /// Вывод характеристик персонажа на консоль при перемещении по миру
        /// </summary>
        /// <param name="player">игрок</param>
        public static void PrintCharacteristic(Player player)
        {
            int i = 0;
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Ваше имя: {0}", player.Name);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Текущее здоровье: {0}", player.CurrentHealth);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Максимальное здоровье: {0}", player.MaxHealth);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Текущая мана: {0}", player.CurrentMana);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Максимальная мана: {0}", player.MaxMana);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Мощность атаки: {0}", player.PowerAttack);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Баланс: {0}", player.Money);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Уровень: {0}", player.Level);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Уровень магии {0}", player.MagicLevel);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Опыт сражений {0}", player.BattleSkill);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Навык меча {0:0.0000}", player.SwordSkill);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Навык лука {0:0.0000}", player.BowSkill);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Навык магии {0:0.0000}", player.MagicSkill);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Позиция: ({0}, {1})", player.Position.X, player.Position.Y);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("До следующего уровня {0}", player.NextLevelBorder - player.BattleSkill);
            Console.SetCursorPosition(0, MapSizeY + 1);
        }

        /// <summary>
        /// Вывод характеристик во время битвы
        /// </summary>
        /// <param name="player">игрок</param>
        /// <param name="enemy">монстры</param>
        public static void PrintBattleCharacteristic(Player player, List<Enemy> enemy)
        {
            int i = 0;
            Console.SetCursorPosition(MapSizeX + 1, ++i);
            Console.WriteLine("Ваше здоровье: {0}", player.CurrentHealth);
            Console.SetCursorPosition(MapSizeX + 1, ++i);
            Console.WriteLine("Ваша мана: {0}", player.CurrentMana);
            Console.SetCursorPosition(MapSizeX + 1, ++i);
            Console.WriteLine("Сила атаки: {0}", player.PowerAttack);
            Console.SetCursorPosition(MapSizeX + 1, ++i);
            Console.WriteLine("-----------------------------");
            Console.SetCursorPosition(MapSizeX + 1, ++i);
            Console.WriteLine("Название монстров: {0}", enemy[0].Name);
            Console.SetCursorPosition(MapSizeX + 1, ++i);
            Console.WriteLine("Сила атакаки: ");
            for (int j = 0; j < enemy.Count; j++)
            {
                Console.SetCursorPosition(MapSizeX + 1, 7 + j);
                Console.WriteLine("{0}: {1}", j + 1, enemy[j].PowerAttack);
            }
            Console.SetCursorPosition(0, MapSizeY + 3);

        }

        /// <summary>
        /// Вывод монстров на боевую карту и изменение их количества после того, как кого то из них убивают
        /// </summary>
        /// <param name="enemy">монстры</param>
        public static void PrintEnemy(List<Enemy> enemy)
        {
            EnemyCount = enemy.Count;
            DrowEnemyBattle(EnemyCount, enemy);

            for (int i = 0; i < MapSizeX; i++)
            {
                if (BattleMap[EnemyGeneration, i] == EnemySymble)
                {
                    bool isEnemy = false;
                    for (int j = 0; j < enemy.Count; j++)
                    {
                        if (enemy[j].Position.Y == i)
                        {
                            isEnemy = true;
                            break;
                        }
                    }
                    if (!isEnemy) BattleMap[EnemyGeneration, i] = ' ';
                }
            }
            Console.Clear();
            PrintMap(BattleMap);
            Console.WriteLine("--------------------------------");
            PrintEnemyHealth(enemy);
        }

        public static void PrintEnemyHealth(List<Enemy> enemy)
        {
            Console.WriteLine("Здоровье врагов:");
            int length = 0;
            for (int i = 0; i < enemy.Count; i++)
            {
                length += enemy[i].Health.ToString().Length + 1;
                if (length <= 31)
                    Console.Write("{0} ", enemy[i].Health);
                else
                {
                    Console.WriteLine();
                    Console.Write("{0} ", enemy[i].Health);
                    length = 0;
                }

            }
            Console.WriteLine();
        }

        /// <summary>
        /// Вывод атаки на монстра
        /// </summary>
        public static void PrintOnEnemyAtack()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < MapSizeY; i++)
            {
                if (i > 4) Console.ForegroundColor = ConsoleColor.Black;
                for (int j = 0; j < MapSizeX; j++)
                {
                    Console.Write(BattleMap[i, j]);
                }
                Console.WriteLine();
            }
            Thread.Sleep(400);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// ВЫвод атаки монстра
        /// </summary>
        public static void PrintEnemyAtack()
        {

            Console.Clear();
            for (int i = 0; i < MapSizeY; i++)
            {
                if (i > 4) Console.ForegroundColor = ConsoleColor.Red;
                for (int j = 0; j < MapSizeX; j++)
                {
                    Console.Write(BattleMap[i, j]);
                }
                Console.WriteLine();
            }
            Thread.Sleep(400);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Вывод передвижения игрока по карте
        /// </summary>
        /// <param name="moveX">сдвиг по оси Х относительно середины карты</param>
        /// <param name="moveY">сдвиг по оси Y относительно середины карты</param>
        public static void PrintMovePlayerOnMap(int moveX, int moveY)
        {
            Console.Clear();
            var tempChar = Map[(MapSizeY - 1) / 2 + moveY, (MapSizeX - 1) / 2 + moveX];
            Map[(MapSizeY - 1) / 2 + moveY, (MapSizeX - 1) / 2 + moveX] = PlayerSymble;
            PrintMap(Map);
            Map[(MapSizeY - 1) / 2 + moveY, (MapSizeX - 1) / 2 + moveX] = tempChar;
            Console.WriteLine("--------------------------------");
        }

        /// <summary>
        /// Отрисовка городов на карте
        /// </summary>
        /// <param name="cityPosition">позиции городов</param>
        /// <param name="playerPosition">позиция игрока</param>
        public static void DrowCity(List<MainGameStructures.Position> cityPosition, MainGameStructures.Position playerPosition)
        {
            ClearMap(Map, CitySymble);
            for (int i = 0; i < cityPosition.Count; i++)
                Map[MapSizeY / 2 - playerPosition.Y + cityPosition[i].Y, MapSizeX / 2 - playerPosition.X + cityPosition[i].X] = CitySymble;
        }

        /// <summary>
        /// Отрисовка врагов на карте
        /// </summary>
        /// <param name="enemyPosition">позиция врагов</param>
        /// <param name="playerPosition">позиция игрока</param>
        /// <param name="moveX">сдвиг по оси Х относительно середины карты</param>
        /// <param name="moveY">сдвиг по оси У относительно середины карты</param>
        public static void DrowEnemy(MainGameStructures.Position enemyPosition, MainGameStructures.Position playerPosition,
            int moveX = 0, int moveY = 0)
        {
            ClearMap(Map, EnemySymble);
            if (MapSizeY / 2 - playerPosition.Y + enemyPosition.Y + moveY >= 0
                && MapSizeY / 2 - playerPosition.Y + enemyPosition.Y + moveY < MapSizeY
                && MapSizeX / 2 - playerPosition.X + enemyPosition.X + moveX >= 0
                && MapSizeX / 2 - playerPosition.X + enemyPosition.X + moveX < MapSizeX)

                Map[MapSizeY / 2 - playerPosition.Y + enemyPosition.Y + moveY, MapSizeX / 2 - playerPosition.X + enemyPosition.X + moveX] = EnemySymble;
        }

        /// <summary>
        /// Отрисовка кладов на карте
        /// </summary>
        /// <param name="treasurePosition">позиция клада</param>
        /// <param name="playerPosition">позиция игрока</param>
        /// <param name="moveX">сдвиг по оси Х относительно середины карты</param>
        /// <param name="moveY">сдвиг по оси У относительно середины карты</param>
        public static void DrowTreasure(MainGameStructures.Position treasurePosition, MainGameStructures.Position playerPosition,
            int moveX = 0, int moveY = 0)
        {
            ClearMap(Map, TreasureSymble);
            if (MapSizeY / 2 - playerPosition.Y + treasurePosition.Y + moveY >= 0
                && MapSizeY / 2 - playerPosition.Y + treasurePosition.Y + moveY < MapSizeY
                && MapSizeX / 2 - playerPosition.X + treasurePosition.X + moveX >= 0
                && MapSizeX / 2 - playerPosition.X + treasurePosition.X + moveX < MapSizeX)

                Map[MapSizeY / 2 - playerPosition.Y + treasurePosition.Y + moveY, MapSizeX / 2 - playerPosition.X + treasurePosition.X + moveX] = TreasureSymble;
        }

        /// <summary>
        ///  Отрисовка монстров во время битвы
        /// </summary>
        /// <param name="enemyCount">количество монстров</param>
        /// <param name="enemy">монстры</param>
        public static void DrowEnemyBattle(int enemyCount, List<Enemy> enemy)
        {
            int count = 0;

            for (int i = -1 * (enemyCount - 1); i < enemyCount; i += 2)
            {
                BattleMap[EnemyGeneration, MapSizeX / 2 + i] = EnemySymble;

                enemy[count].Position = new MainGameStructures.Position { X = EnemyGeneration, Y = MapSizeX / 2 + i };
                count++;
            }
            BattleMap[MapSizeY / 2, MapSizeX / 2] = PlayerSymble;
        }

        /// <summary>
        /// удаление с заданной карты заданных символов
        /// </summary>
        /// <param name="map">карта</param>
        /// <param name="symbleDelete">символ</param>
        public static void ClearMap(char[,] map, char symbleDelete)
        {
            if (symbleDelete == EnemySymble) Console.BackgroundColor = ConsoleColor.DarkGreen;
            for (int i = 0; i < MapSizeY; i++)
            {
                for (int j = 0; j < MapSizeX; j++)
                {
                    if (map[i, j] == symbleDelete) map[i, j] = ' ';
                }
            }
        }

    }
}
