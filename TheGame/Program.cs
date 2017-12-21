using System;
using System.Collections.Generic;
using System.IO;

namespace TheGame
{
    /// <summary>
    /// Содержит основные методы для подготовки к игре
    /// </summary>
    class Program
    {
        // Текущая директория
        public static string Path;
        // Один рандом на всю программу
        public static Random Random;

        // Все комплекты брони в игре
        public static List<ArmorComplect> ArmorComplects;
        // Все мечи в игре
        public static List<Sword> Swords;
        // Все луки в игре
        public static List<Bow> Bows;
        // Все заклинания в игре
        public static List<Spell> Spells;
        // Все города в игре
        public static List<City> Cities;
        // Все защитные предметы/заклинания
        public static List<Protection> ProtectionThings;

        // Файлы со всеми мечами
        public static string[] NamesSwords = { @"\baseWarriorSword.txt", @"\baseWizardKnife.txt", @"\baseStilet.txt",
        @"\basePoleaxe.txt", @"\middleElderSword.txt", @"\oneHitSword.txt", @"\DarkPoleaxe.txt", @"\dragonSlayerSword.txt", @"\FireGodSword.txt"};
        // Файлы со всеми луками
        public static string[] NamesBows = { @"\baseRangerBow.txt", @"\middleRangerBow.txt", @"\hunterBow.txt", @"\legendaryBow.txt" };
        // Файлы со всей броней
        public static string[,] NamesArmor = {
            { @"\baseWarriorHeadArmor.txt", @"\baseWarriorBodyArmor.txt", @"\baseWarriorArmsArmor.txt", @"\baseWarriorLeggsArmor.txt", @"\baseWarriorBootsArmor.txt" },
            { @"\baseRangerHeadArmor.txt", @"\baseRangerBodyArmor.txt", @"\baseRangerArmsArmor.txt", @"\baseRangerLeggsArmor.txt", @"\baseRangerBootsArmor.txt"  },
            { @"\middleIronHeadArmor.txt", @"\middleIronBodyArmor.txt", @"\middleIronArmsArmor.txt", @"\middleIronLeggsArmor.txt", @"\middleIronBootsArmor.txt" }
        };
        // Файлы со всеми заклинаниями
        public static string[] NamesSpells = { @"\baseWizardAtackingSpell.txt", @"\basePointAttack.txt", @"\middleThunderAttack.txt",
        @"\baseWarriorMultiplyAttack.txt", @"\dragonsFlame.txt", @"\LightStars.txt"};
        // Названия всех существующих в игре городов
        public static string[] CitiesNames = { "Неаполь", "Лордерон", "Царьград", "Омск", "Тартарос", "Лондон",
            "Штормград", "Мордор", "Эребор", "Минас Тирит", "Солнечный колодец", "Троя", "Тортуга", "Кингстон",
            "Барбадос", "Сицилия", "Монако", "Осло", "Стокгольм", "Брандтео", "Элеонор", "Венеция", "Флоренция",
            "Валинор", "Ильмарин", "Чертог Мандоса", "Антананариву", "Рейкьявик"};
        // Файлы со всеми защитными средствами
        public static string[] ProtectionNames = { "/baseProtection.txt", "/magicProtection.txt", "/middleProtection.txt" };

        static List<Protection> GetProtection()
        {
            List<Protection> result = new List<Protection>();
            foreach (var name in ProtectionNames)
                result.Add(new Protection(name));
            return result;
        }

        // Заполнения листа со всеми мечами
        static List<Sword> GetSwords(string path)
        {
            var result = new List<Sword>();
            for (int i = 0; i < NamesSwords.Length; i++)
                result.Add(Sword.GetWeaponsFromFile(path + NamesSwords[i]));
            return result;
        }

        // Заполнение листа со всеми луками
        static List<Bow> GetBows(string path)
        {
            var result = new List<Bow>();
            for (int i = 0; i < NamesBows.Length; i++)
                result.Add(Bow.GetBowFromFile(path + NamesBows[i]));
            return result;
        }

        // Заполнение листа со всеми заклинаниями
        static List<Spell> GetSpells(string path)
        {
            var result = new List<Spell>();
            for (int i = 0; i < NamesSpells.Length; i++)
                result.Add(new Spell(path + NamesSpells[i]));
            return result;
        }

        // Заполнение всех комплектов брони
        static List<ArmorComplect> GetArmor(string path)
        {
            var result = new List<ArmorComplect>();
            int countComplect = NamesArmor.GetLength(0);
            for (int i = 0; i < countComplect; i++)
            {
                var tempComplect = new ArmorComplect();
                // Устанавливаем в строго определенном порядке
                tempComplect.Head = Armor.GetArmorFromFile(path + NamesArmor[i, 0]);
                tempComplect.Body = Armor.GetArmorFromFile(path + NamesArmor[i, 1]);
                tempComplect.Arms = Armor.GetArmorFromFile(path + NamesArmor[i, 2]);
                tempComplect.Leggs = Armor.GetArmorFromFile(path + NamesArmor[i, 3]);
                tempComplect.Boots = Armor.GetArmorFromFile(path + NamesArmor[i, 4]);
                result.Add(tempComplect);
            }
            return result;
        }

        /// <summary>
        /// Выбор типа персонажа
        /// </summary>
        /// <returns> Выбранный тип </returns>
        static Player.PlayerType SelectType()
        {
            // Выводим все возможные типы персонажа с их описанием
            Console.WriteLine(File.ReadAllText(Path + @"\TextFiles\types.txt"));

            int choice = Program.Parse(Console.ReadLine()) - 1;

            Console.Clear();

            if (choice >= 0 && choice < 3)
                return (Player.PlayerType)choice;
            else return SelectType();
        }

        // Записываем имя игрока
        static string GetName()
        {
            Console.WriteLine("Введите ваше имя");
            return Console.ReadLine();
        }

        // Создаем города на карте
        public static List<City> GetCities()
        {
            var result = new List<City>();
            foreach (var name in CitiesNames)
                result.Add(new City(name, GetRandomPosition(-75, 75)));
            return result;
        }

        // Находим расстояние между двумя позициями
        public static double GetDistance(MainGameStructures.Position first, MainGameStructures.Position second) =>
            Math.Sqrt(Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2));

        /// <summary>
        /// Валидация ввода
        /// </summary>
        /// <param name="str"> Строка для парсинга </param>
        /// <param name="min"> Минимальное возможное значение </param>
        /// <param name="max"> Максимальное возможное значение </param>
        /// <returns> Число, находящееся между минимальной и максимальной границей </returns>
        public static int Parse(string str, double min = -1e18, double max = 1e18)
        {
            try
            {
                int t = int.Parse(str);
                if (t < min)
                    t = (int)(Math.Ceiling(min));
                else if (t > max)
                    t = (int)(Math.Ceiling(max));
                return t;
            }
            catch (FormatException)
            {
                return (int)min;
            }
        }

        // Возвращает рандомно сгенерированную позицию в заданном диапазоне
        public static MainGameStructures.Position GetRandomPosition(int min = -10, int max = 11)=>
            new MainGameStructures.Position { X = Random.Next(min, max), Y = Random.Next(min, max) };

        static void Main()
        {
            Console.Title = "TheBestRPG";
            Random = new Random();
            Path = Environment.CurrentDirectory;

            ArmorComplects = GetArmor(Path + @"\TextFiles\Armor");
            Spells = GetSpells(Path + @"\TextFiles\Weapons\Spells");
            Swords = GetSwords(Path + @"\TextFiles\Weapons\Swords");
            Bows = GetBows(Path + @"\TextFiles\Weapons\Bows");
            ProtectionThings = GetProtection();
            Cities = GetCities();

            BeginGame();
            Console.Clear();

            Console.WriteLine("Игра завершена");
            Console.ReadKey();
        }

        // Метод для начала игры; создаем персонажа, а дальше игра переходит в него
        static void BeginGame()
        {
            var name = GetName();
            var position = GetRandomPosition();

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
