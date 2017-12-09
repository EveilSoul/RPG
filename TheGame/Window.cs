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
        public static int WindowSizeX = 31;
        public static int WindowSizeY = 15;
        public static char[,] Map = new char[WindowSizeY, WindowSizeX];
        public static char[,] BattleMap = new char[WindowSizeY, WindowSizeX];
        public static char PlayerSymble = '@';
        public static char EnemySymble = '*';
        public static char CitySymble = '#';
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
            for (int i = 0; i < WindowSizeY; i++)
            {
                for (int j = 0; j < WindowSizeX; j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }


        }

        public static void PrintMovePlayerOnMap(int moveX, int moveY)
        {
            Console.Clear();
            var tempChar = Map[(WindowSizeY - 1) / 2 + moveY, (WindowSizeX - 1) / 2 + moveX];
            Map[(WindowSizeY - 1) / 2 + moveY, (WindowSizeX - 1) / 2 + moveX] = PlayerSymble;
            PrintMap(Map);
            Map[(WindowSizeY - 1) / 2 + moveY, (WindowSizeX - 1) / 2 + moveX] = tempChar;
        }


        public static void DrowEnemyBattle(int enemyCount, List<Enemy> enemy)
        {
            int count = 0;

            for (int i = -1 * (enemyCount - 1); i < enemyCount; i += 2)
            {
                BattleMap[EnemyGeneration, WindowSizeX / 2 + i] = EnemySymble;

                enemy[count].Position = new ObjectStructures.Position { X = EnemyGeneration, Y = WindowSizeX / 2 + i };
                count++;
            }


            BattleMap[WindowSizeY / 2, WindowSizeX / 2] = PlayerSymble;
        }

        public static void PrintEnemy(List<Enemy> enemy)
        {
            if (!IsBattle)
            {
                EnemyCount = enemy.Count;
                DrowEnemyBattle(EnemyCount, enemy);
                IsBattle = true;
            }


            for (int i = 0; i < WindowSizeX; i++)
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

            //test
            for (int i = 0; i < enemy.Count; i++)
            {
                Console.Write("{0} ", enemy[i].Health);
            }
            Console.WriteLine();

        }


        public static void PrintOnEnemyAtack()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < WindowSizeY; i++)
            {
                if (i > 4) Console.ForegroundColor = ConsoleColor.Black;
                for (int j = 0; j < WindowSizeX; j++)
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
            for (int i = 0; i < WindowSizeY; i++)
            {
                if (i > 4) Console.ForegroundColor = ConsoleColor.Red;
                for (int j = 0; j < WindowSizeX; j++)
                {
                    Console.Write(BattleMap[i, j]);
                }
                Console.WriteLine();
            }
            Thread.Sleep(400);


            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

        }

        public static void ClearMap(char[,] map, char symbleDelete)
        {
            for (int i = 0; i < WindowSizeY; i++)
            {
                for (int j = 0; j < WindowSizeX; j++)
                {
                    if (map[i, j] == symbleDelete) map[i, j] = ' ';
                }
            }
        }

        public static void DrowCity(ObjectStructures.Position cityPosition, ObjectStructures.Position playerPosition)
        {
            ClearMap(Map, CitySymble);
            Map[WindowSizeY / 2 + playerPosition.Y - cityPosition.Y, WindowSizeX / 2 - playerPosition.X + cityPosition.X] =CitySymble;
        }

        public static void DrowEnemy(ObjectStructures.Position enemyPosition, ObjectStructures.Position playerPosition,
            int moveX = 0, int moveY = 0)
        {
            ClearMap(Map, EnemySymble);
            if (WindowSizeY / 2 + playerPosition.Y - enemyPosition.Y + moveY>=0 
                && WindowSizeY / 2 + playerPosition.Y - enemyPosition.Y + moveY<=WindowSizeY
                && WindowSizeX / 2 - playerPosition.X + enemyPosition.X + moveX>=0
                && WindowSizeX / 2 - playerPosition.X + enemyPosition.X + moveX<=WindowSizeX)
            Map[WindowSizeY / 2 + playerPosition.Y - enemyPosition.Y + moveY, WindowSizeX / 2 - playerPosition.X + enemyPosition.X + moveX] = EnemySymble;
        }
        
    }
}
