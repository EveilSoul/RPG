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
        public static char PlayerSymble = '@';
        public static char EnemySymble = '*';
        private static char[,] Map = new char[WindowSizeY, WindowSizeX];
        public static int EnemyGeneration = 4;
        public static bool IsBattle = false;
        public static int EnemyCount;

        public static void PrintArray(string[] array)
        {
            foreach(var t in array)
                Console.WriteLine(t);
        }

        public static void DrowWindow()
        {
            Console.SetWindowSize(WindowSizeX + 1, WindowSizeY + 10);
            Console.SetBufferSize(WindowSizeX + 1, WindowSizeY + 10);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void PrintMap()
        {
            for (int i = 0; i < WindowSizeY; i++)
            {
                for (int j = 0; j < WindowSizeX; j++)
                {
                    Console.Write(Map[i, j]);
                }
                Console.WriteLine();
            }


        }

        public static void PrintMovePlayerOnMap(int moveX, int moveY)
        {
            Console.Clear();
            Map[(WindowSizeY - 1) / 2 + moveY, (WindowSizeX - 1) / 2 + moveX] = PlayerSymble;
            PrintMap();
            Map[(WindowSizeY - 1) / 2 + moveY, (WindowSizeX - 1) / 2 + moveX] = ' ';
        }


        public static void DrowEnemy(int enemyCount, List<Enemy> enemy)
        {
            int count = 0;

            for (int i = -1 * (enemyCount - 1); i < enemyCount; i += 2)
            {
                Map[EnemyGeneration, WindowSizeX / 2 + i] = EnemySymble;

                enemy[count].Position = new ObjectStructures.Position { X = EnemyGeneration, Y = WindowSizeX / 2 + i };
                count++;
            }


            Map[WindowSizeY / 2, WindowSizeX / 2] = PlayerSymble;
        }

        public static void PrintEnemy(List<Enemy> enemy)
        {
            if (!IsBattle)
            {
                EnemyCount = enemy.Count;
                DrowEnemy(EnemyCount, enemy);
                IsBattle = true;
            }


            for (int i = 0; i < WindowSizeX; i++)
            {
                if (Map[EnemyGeneration, i] == EnemySymble)
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
                    if (!isEnemy) Map[EnemyGeneration, i] = ' ';
                }
            }
            Console.Clear();
            PrintMap();

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
                        Console.Write(Map[i, j]);
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
                        Console.Write(Map[i, j]);
                    }
                    Console.WriteLine();
                }
                Thread.Sleep(400);
            

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

        }

        public static void ClearMap()
        {
            for (int i = 0; i < WindowSizeY; i++)
            {
                for (int j = 0; j < WindowSizeX; j++)
                {
                    Map[i, j] = ' ';
                }
            }
        }

    }
}
