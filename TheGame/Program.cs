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
            Path = Environment.CurrentDirectory;

            var name = GetName();
            var position = GetPosition();

            switch (SelectType())
            {
                case Player.PlayerType.Warrior:
                    Warrior warrior = new Warrior(name, position);
                    warrior.JoinGame();
                    break;
                case Player.PlayerType.Ranger:
                    Ranger ranger = new Ranger(name, position);
                    ranger.JoinGame();
                    break;
                case Player.PlayerType.Wizard:
                    Wizard wizard = new Wizard(name, position);
                    wizard.JoinGame();
                    break;
            }

            Console.ReadKey();
        }
    }
}
