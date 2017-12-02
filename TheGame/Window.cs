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

        public static void DrowWindow()
        {
            Console.SetWindowSize(WindowSizeX + 1, WindowSizeY + 10);
            Console.SetBufferSize(WindowSizeX + 1, WindowSizeY + 10);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void DrowMap(int moveX = 0, int moveY = 0)
        {
            for (int i = 0; i < WindowSizeY; i++)
            {
                for (int j = 0; j < WindowSizeX; j++)
                {
                    if (i == (WindowSizeY - 1)/2 + moveY && j == (WindowSizeX - 1)/2 + moveX)
                        Console.Write(PlayerSymble);
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
            
        }

        public static void ClearAndDrow(int moveX, int moveY)
        {
            Console.Clear();
            Window.DrowMap(moveX, moveY);
        }

       
        public static void PringEnemy(int enemyCount)
        {
            
            var enemy = new int[enemyCount];
            int count = 0;
            bool isEnemy = false;
            for (int i = 0; i < enemyCount; i++)
            {
                enemy[i] = Program.Random.Next((WindowSizeX - 1) / 2 - 5, (WindowSizeX - 1) / 2 + 6);
            }

            for (int i = 0; i < WindowSizeY; i++)
            {
                for (int j = 0; j < WindowSizeX; j++)
                {
                    if (count < enemyCount && ((i == WindowSizeY / 2 - (enemyCount + 1) / 2) || isEnemy) && j == enemy[count] && i != WindowSizeY / 2)
                    {
                        Console.Write(EnemySymble);
                        count++;
                        isEnemy = true;
                        break;
                    }
                    else if (i == (WindowSizeY - 1) / 2 && j == (WindowSizeX - 1) / 2)
                        Console.Write(PlayerSymble);
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }

        }

    }
}