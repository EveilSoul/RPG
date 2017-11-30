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

            int? choice = int.Parse(Console.ReadLine()) - 1;

            Console.Clear();

            if (choice != null && choice >= 0 && choice < 3)
                return (Player.PlayerType)choice;
            else return SelectType();
        }

        static string GetName()
        {
            Console.WriteLine("Введите ваше имя");
            return Console.ReadLine();
        }

        public static ObjectStructures.Position GetPosition()
        {
            var random = new Random();
            return new ObjectStructures.Position { X = random.Next(-10, 11), Y = random.Next(-10, 11) };
        }

        static void Main()
        {
            int moveX = 0, moveY = 0;

            Path = Environment.CurrentDirectory;
            var swords = GetSwords(Path + @"\TextFiles\Swords");
            


            switch (SelectType())
            {
                case Player.PlayerType.Warrior:
                    Warrior warrior = new Warrior(GetName(), GetPosition());
                    break;
                case Player.PlayerType.Ranger:
                    Ranger ranger = new Ranger(GetName(), GetPosition());
                    break;
            }

            Player player = new Player();

            Window.DrowWindow();
            Window.ClearAndDrow(player, moveX, moveY);

            while (true)
            {
                var ch = Console.ReadKey(true).Key;


                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        player.Walk(0, 1);
                        moveY--;
                        Window.ClearAndDrow(player, moveX, moveY);
                        break;
                    case ConsoleKey.DownArrow:
                        player.Walk(0, -1);
                        moveY++;
                        Window.ClearAndDrow(player, moveX, moveY);
                        break;
                    case ConsoleKey.LeftArrow:
                        player.Walk(-1, 0);
                        moveX--;
                        Window.ClearAndDrow(player, moveX, moveY);
                        break;
                    case ConsoleKey.RightArrow:
                        player.Walk(1, 0);
                        moveX++;
                        Window.ClearAndDrow(player, moveX, moveY);
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

                if (moveX == Window.WindowSizeX / 2 || moveY == Window.WindowSizeY / 2)
                {
                    moveX = 0;
                    moveY = 0;
                    Window.ClearAndDrow(player, moveX, moveY);
                }
            }

            Console.ReadKey();
        }
    }
}
