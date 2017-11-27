using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Program
    {
        static Player.PlayerType SelectType()
        {
            Console.WriteLine(File.ReadAllText("types.txt"));

            int choice = int.Parse(Console.ReadLine()) - 1;

            Console.Clear();

            if (choice >= 0 && choice < 3)
                return (Player.PlayerType)choice;
            else return SelectType();
        }

        static string GetName()
        {
            Console.WriteLine("Введите ваше имя");
            return Console.ReadLine();
        }

        static void Main()
        {
            //Warrior warrior = new Warrior();
            Ranger ranger = new Ranger();
            Wizard wizard = new Wizard();
            Console.WriteLine(SelectType());
            while (true)
            {
                var ch = Console.ReadKey(true).Key;
                Console.Clear();
                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        wizard.Walk(0, 1);
                        break;
                    case ConsoleKey.DownArrow:
                        wizard.Walk(0, -1);
                        break;
                    case ConsoleKey.LeftArrow:
                        wizard.Walk(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        wizard.Walk(1, 0);
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
