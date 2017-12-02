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
        public static string Path;
        public static Random Random;
        public static List<ObjectStructures.ArmorComplect> Armor;
        public static List<ObjectStructures.Weapons> Swords;
        public static List<Spell> Spells;

        public static string[] NamesSwords = { @"\baseWarriorSword.txt" };
        public static string[,] NamesArmor = {
            { @"\baseWarriorHeadArmor.txt", @"\baseWarriorBodyArmor.txt", @"\baseWarriorArmsArmor.txt", @"\baseWarriorLeggsArmor.txt", @"\baseWarriorBootsArmor.txt" }
        };
        public static string[] NamesSpells = { @"\baseWizardAtackingSpell.txt" };

        static List<ObjectStructures.Weapons> GetSwords(string path)
        {
            var result = new List<ObjectStructures.Weapons>();
            for (int i = 0; i < NamesSwords.Length; i++)
                result.Add(ObjectStructures.GetWeaponsFromFile(path + NamesSwords[i]));
            return result;
        }

        static List<Spell> GetSpells(string path)
        {
            var result = new List<Spell>();
            for (int i = 0; i < NamesSpells.Length; i++)
                result.Add(new Spell(path + NamesSpells[i]));
            return result;
        }

        static List<ObjectStructures.ArmorComplect> GetArmor(string path)
        {
            var result = new List<ObjectStructures.ArmorComplect>();
            int countComplect = NamesArmor.GetLength(0);
            for (int i = 0; i < countComplect; i++)
            {
                var tempComplect = new ObjectStructures.ArmorComplect();
                tempComplect.Head = ObjectStructures.GetArmorFromFile(path + NamesArmor[i, 0]);
                tempComplect.Body = ObjectStructures.GetArmorFromFile(path + NamesArmor[i, 1]);
                tempComplect.Arms = ObjectStructures.GetArmorFromFile(path + NamesArmor[i, 2]);
                tempComplect.Leggs = ObjectStructures.GetArmorFromFile(path + NamesArmor[i, 3]);
                tempComplect.Boots = ObjectStructures.GetArmorFromFile(path + NamesArmor[i, 4]);
                result.Add(tempComplect);
            }
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

        public static ObjectStructures.Position GetPosition()
        {
            return new ObjectStructures.Position { X = Random.Next(-10, 11), Y = Random.Next(-10, 11) };
        }

        static void Main()
        {

            Random = new Random();
            Path = Environment.CurrentDirectory;

            Armor = GetArmor(Path + @"\TextFiles\Armor");
            Spells = GetSpells(Path + @"\TextFiles\Spells");
            Swords = GetSwords(Path + @"\TextFiles\Swords");

            BeginGame();

            Console.ReadKey();
        }

        static void BeginGame()
        {
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
        }
    }
}
