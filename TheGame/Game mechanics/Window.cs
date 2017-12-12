using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TheGame
{
    class Window
    {
        public static int MapSizeX = 31;
        public static int MapSizeY = 15;
        public static int WindowSizeX = 61;
        public static int WindowSizeY = 18;
        public static char[,] Map = new char[MapSizeY, MapSizeX];
        public static char[,] BattleMap = new char[MapSizeY, MapSizeX];
        public static char PlayerSymble = '@';
        public static char EnemySymble = '*';
        public static char CitySymble = '#';
        public static char TreasureSymble = '$';
        public static int EnemyGeneration = 4;
        public static int EnemyCount;
        public static bool IsBattle = false;

        

        public static void PrintArray(string[] array)
        {
            foreach (var t in array)
                if (t != null)
                    Console.WriteLine(t);
        }

        public static void DrowWindow()
        {
            Console.SetWindowSize(WindowSizeX + 1, WindowSizeY + 10);
            Console.SetBufferSize(WindowSizeX + 1, WindowSizeY + 10);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
        }

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
            Console.WriteLine("Навык магии {0:0.0000}", player.MagicAccuracy);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("Позиция: ({0}, {1})", player.Position.X, player.Position.Y);
            Console.SetCursorPosition(MapSizeX + 1, i++);
            Console.WriteLine("До следующего уровня {0}", player.NextLevelBorder - player.BattleSkill);
            Console.SetCursorPosition(0, MapSizeY + 1);
        }

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
        
        public static void PrintEnemy(List<Enemy> enemy)
        {
            if (!IsBattle)
            {
                EnemyCount = enemy.Count;
                DrowEnemyBattle(EnemyCount, enemy);
                IsBattle = true;
            }


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
            Console.WriteLine("Здоровье врагов:");
            for (int i = 0; i < enemy.Count; i++)
            {
                Console.Write("{0} ",enemy[i].Health);
            }
            Console.WriteLine();
        }

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

        public static void PrintMovePlayerOnMap(int moveX, int moveY)
        {
            Console.Clear();
            var tempChar = Map[(MapSizeY - 1) / 2 + moveY, (MapSizeX - 1) / 2 + moveX];
            Map[(MapSizeY - 1) / 2 + moveY, (MapSizeX - 1) / 2 + moveX] = PlayerSymble;
            PrintMap(Map);
            Map[(MapSizeY - 1) / 2 + moveY, (MapSizeX - 1) / 2 + moveX] = tempChar;
            Console.WriteLine("--------------------------------");
        }

        public static void DrowCity(List<ObjectStructures.Position> cityPosition, ObjectStructures.Position playerPosition)
        {
            ClearMap(Map, CitySymble);
            for (int i=0;i<cityPosition.Count;i++)
            Map[MapSizeY / 2 - playerPosition.Y + cityPosition[i].Y, MapSizeX / 2 - playerPosition.X + cityPosition[i].X] = CitySymble;
        }

        public static void DrowEnemy(ObjectStructures.Position enemyPosition, ObjectStructures.Position playerPosition,
            int moveX = 0, int moveY = 0)
        {
            ClearMap(Map, EnemySymble);
            if (MapSizeY / 2 - playerPosition.Y + enemyPosition.Y + moveY >= 0
                && MapSizeY / 2 - playerPosition.Y + enemyPosition.Y + moveY < MapSizeY
                && MapSizeX / 2 - playerPosition.X + enemyPosition.X + moveX >= 0
                && MapSizeX / 2 - playerPosition.X + enemyPosition.X + moveX < MapSizeX)

                Map[MapSizeY / 2 - playerPosition.Y + enemyPosition.Y + moveY, MapSizeX / 2 - playerPosition.X + enemyPosition.X + moveX] = EnemySymble;
        }

        public static void DrowTreasure(ObjectStructures.Position treasurePosition, ObjectStructures.Position playerPosition,
            int moveX = 0, int moveY = 0)
        {
            ClearMap(Map, TreasureSymble);
            if (MapSizeY / 2 - playerPosition.Y + treasurePosition.Y + moveY >= 0
                && MapSizeY / 2 - playerPosition.Y + treasurePosition.Y + moveY < MapSizeY
                && MapSizeX / 2 - playerPosition.X + treasurePosition.X + moveX >= 0
                && MapSizeX / 2 - playerPosition.X + treasurePosition.X + moveX < MapSizeX)

                Map[MapSizeY / 2 - playerPosition.Y + treasurePosition.Y + moveY, MapSizeX / 2 - playerPosition.X + treasurePosition.X + moveX] = TreasureSymble;
        }

        public static void DrowEnemyBattle(int enemyCount, List<Enemy> enemy)
        {
            int count = 0;

            for (int i = -1 * (enemyCount - 1); i < enemyCount; i += 2)
            {
                BattleMap[EnemyGeneration, MapSizeX / 2 + i] = EnemySymble;

                enemy[count].Position = new ObjectStructures.Position { X = EnemyGeneration, Y = MapSizeX / 2 + i };
                count++;
            }


            BattleMap[MapSizeY / 2, MapSizeX / 2] = PlayerSymble;
        }

        public static void ClearMap(char[,] map, char symbleDelete)
        {
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
