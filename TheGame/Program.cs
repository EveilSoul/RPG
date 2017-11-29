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
        static string[] Swords = { @"\baseWarriorSword.txt" };
        static string Path;

        static List<ObjectStructures.Weapons> GetSwords(string path)
        {
            var result = new List<ObjectStructures.Weapons>();
            for (int i = 0; i < Swords.Length; i++)
                result.Add(ObjectStructures.GetWeaponsFromFile(path + Swords[i]));
            return result;
        }

        static Player.PlayerType SelectType()
        {
            Console.WriteLine(File.ReadAllText(Path + @"\TextFiles\types.txt"));

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
            Path = Environment.CurrentDirectory;
            var swords = GetSwords(Path + @"\TextFiles\Swords");
            Warrior warrior = new Warrior(GetName(), new ObjectStructures.Position { X = 0, Y = 0 });
            //Ranger ranger = new Ranger();
            //Wizard wizard = new Wizard();
            Console.WriteLine(SelectType());
            Console.SetWindowSize(45, 15);
            Console.SetBufferSize(45, 15);
            while (true)
            {
                var ch = Console.ReadKey(true).Key;
                Console.Clear();
                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        //wizard.Walk(0, 1);
                        //Console.WriteLine("up");

                        //for (int i = 0; i < 15; i++)
                        //{
                        //    for (int j = 0; j < 15; j++)
                        //    {
                        //        Console.Write("_");
                        //        if (j != 14)
                        //            Console.Write(" ");
                        //    }
                        //    if (i!=14)
                        //    Console.WriteLine();
                        //}

                        break;
                    case ConsoleKey.DownArrow:
                        //wizard.Walk(0, -1);
                        Console.WriteLine("down");
                        break;
                    case ConsoleKey.LeftArrow:
                        //wizard.Walk(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        //wizard.Walk(1, 0);
                        break;
                    case ConsoleKey.I:
                        //info
                        break;
                    case ConsoleKey.H:
                        //help
                        break;
                    case ConsoleKey.F1:
                        //save
                        break;
                    case ConsoleKey.F2:
                        //load game
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
