using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void DrowWindow()
        {
            Console.SetWindowSize(WindowSizeX + 1, WindowSizeY + 10);
            Console.SetBufferSize(WindowSizeX + 1, WindowSizeY + 10);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void DrowMap()
        {
            for (int i = 0; i < WindowSizeY; i++)
            {
                for (int j = 0; j < WindowSizeX; j++)
                {
                    Console.Write(Map[i,j]);
                }
                Console.WriteLine();
            }
            
        }

        public static void ClearAndDrow(int moveX, int moveY)
        {
            Console.Clear();
            Map[(WindowSizeY - 1) / 2 + moveY,(WindowSizeX - 1) / 2 + moveX] = PlayerSymble;
            DrowMap();
            Map[(WindowSizeY - 1) / 2 + moveY, (WindowSizeX - 1) / 2 + moveX] = ' ';
        }

       
        public static void PringEnemy(int enemyCount, Enemy[] enemy)
        {
            int count = 0;
            
                for (int i = -1*(enemyCount-1); i < enemyCount; i+=2)
                {
                    Map[EnemyGeneration, WindowSizeX / 2 + i] = EnemySymble;

                    enemy[count].Position = new ObjectStructures.Position { X = EnemyGeneration, Y = WindowSizeX / 2 + i };
                }
            
            
            Map[WindowSizeY / 2, WindowSizeX / 2] = PlayerSymble;
            Console.Clear();
            DrowMap();
        }

        public static void ClearMap()
        {
            for (int i = 0; i < WindowSizeY; i++)
            {
                for(int j = 0; j < WindowSizeX; j++)
                {
                    Map[i, j] = ' ';
                }
            }
        }

    }
}