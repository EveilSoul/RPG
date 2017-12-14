using System;
using System.Collections.Generic;
namespace TheGame
{
    class City
    {
        public string Name;
        public ObjectStructures.Position Position;
        private int OneCoinCountHP;
        public Dictionary<string, float> RealCosts;

        List<string> Places = new List<string>();

        public City(string name, ObjectStructures.Position position)
        {
            RealCosts = new Dictionary<string, float>();
            SetCost();
            OneCoinCountHP = 2;
            this.Name = name;
            this.Position = position;
            Places.Add("Магазин для покупки брони");
            Places.Add("Магазин для продажи брони");
            Places.Add("Ремонт брони");
            Places.Add("Лечебница");
            Places.Add("Магазин мечей");
            Places.Add("Библиотека");
            Places.Add("Магазин луков");
            Places.Add("Аптека");
        }

        private void SetCost()
        {
            RealCosts.Add("sword", Program.Random.Next(50, 150) / 100f);
            RealCosts.Add("bow", Program.Random.Next(50, 150) / 100f);
            RealCosts.Add("armor", Program.Random.Next(50, 150) / 100f);
            RealCosts.Add("spell", Program.Random.Next(50, 150) / 100f);
            RealCosts.Add("health", Program.Random.Next(50, 400) / 100f);
        }

        public static void CheckPlayer(Player player)
        {
            foreach (var t in Program.Cities)
                t.CityNear(player);
        }

        public static List<ObjectStructures.Position> IsSityNear(ObjectStructures.Position playerPosition)
        {
            var cities = new List<ObjectStructures.Position>();

            foreach (var t in Program.Cities)
                if (Math.Abs(playerPosition.X - t.Position.X) <= Window.MapSizeX / 2 &&
                    Math.Abs(playerPosition.Y - t.Position.Y) <= Window.MapSizeY / 2)
                    cities.Add(t.Position);
            return cities;
        }

        public void CityNear(Player player)
        {
            if (Program.GetDistance(this.Position, player.Position) < 1)
            {
                Welcome(player);
            }
        }

        public void Hospital(Player player)
        {
            Console.WriteLine("Ваш баланс: {0}", player.Money);
            Console.WriteLine("Вы можете восстановить {0} здоровья", player.MaxHealth - player.CurrentHealth);
            Console.WriteLine("Цена: 1 монета за {0} единиц здоровья", this.OneCoinCountHP * RealCosts["health"]);
            Console.WriteLine("Сколько вы хотите восстановить?");
            int hp = Program.Parse(Console.ReadLine());
            if (player.GiveMoney(hp / (int)(this.OneCoinCountHP * RealCosts["health"])))
                player.AddHP(hp);
            Console.WriteLine("Ваше здоровье теперь составляет {0}", player.CurrentHealth);
        }

        public void Welcome(Player player)
        {
            if (!player.IsLive) return;
            Console.Clear();
            Console.WriteLine("Добро пожаловать в {0}", this.Name);
            Console.WriteLine("Ваш баланс: {0}", player.Money);
            int i = 0;
            foreach (var place in Places)
                Console.WriteLine("{0}: {1}", ++i, place);
            if (Select(player))
                Welcome(player);
        }

        private bool Select(Player player)
        {
            Console.WriteLine("Куда бы вы хотели отправиться?");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    ArmorShopPay(player);
                    break;
                case ConsoleKey.D2:
                    ArmorShopSell(player);
                    break;
                case ConsoleKey.D3:
                    ForgeArmor(player);
                    break;
                case ConsoleKey.D4:
                    Hospital(player);
                    break;
                case ConsoleKey.D5:
                    SwordStore(player);
                    break;
                case ConsoleKey.D6:
                    Libriary(player);
                    break;
                case ConsoleKey.D7:
                    BowStore(player);
                    break;
                case ConsoleKey.D8:
                    ByeMedicineKit(player);
                    break;
                case ConsoleKey.Escape:
                    return false;
            }
            return true;
        }

        private void SwordStore(Player player)
        {
            HelloPlayer(player);
            Console.WriteLine("Мы представим вам весь наш ассортимент.\n" +
                "Нажмите Y для покупки\n" +
                "N, чтобы перейти к следующему\n" +
                "Esc для выхода");
            for (int i = 0; i < Program.Swords.Count; i++)
            {
                Console.Clear();
                WriteCharacteristics(Program.Swords[i].GetCharacteristics(true, RealCosts["sword"]));
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Y:
                        if (player.Level >= Program.Swords[i].MinLevelToUse &&
                            player.GiveMoney((int)((Program.Swords[i].Cost * RealCosts["sword"]))))
                        {
                            Console.WriteLine("Вы приобрели {0}", Program.Swords[i].Name);
                            player.AddSword(Program.Swords[i]);
                            return;
                        }
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        private void Libriary(Player player)
        {
            HelloPlayer(player);
            Console.WriteLine("Мы представим вам все заклинания.\n" +
            "Нажмите Y для покупки\n" +
            "N, чтобы перейти к следующему\n" +
            "Esc для выхода");
            for (int i = 0; i < Program.Spells.Count; i++)
            {
                Console.Clear();
                WriteCharacteristics(Program.Spells[i].GetCharacteristics(true, RealCosts["spell"]));
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Y:
                        if (player.LearnSpell(Program.Spells[i], RealCosts["spell"]))
                            Console.WriteLine("Вы изучили {0}", Program.Spells[i].Name);
                        return;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        private void BowStore(Player player)
        {
            HelloPlayer(player);
            Console.WriteLine("Мы представим вам ассортимент луков.\n" +
            "Нажмите Y для покупки\n" +
            "N, чтобы перейти к следующему\n" +
            "Esc для выхода");
            for (int i = 0; i < Program.Bows.Count; i++)
            {
                Console.Clear();
                WriteCharacteristics(Program.Bows[i].GetCharacteristics(true, RealCosts["bow"]));
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Y:
                        if (player.Level >= Program.Bows[i].MinLevelToUse &&
                            player.GiveMoney((int)(Program.Bows[i].Cost * RealCosts["bow"])))
                        {
                            Console.WriteLine("Вы приобрели {0}", Program.Bows[i].Name);
                            player.Bow = Program.Bows[i];
                            return;
                        }
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        private void HelloPlayer(Player player) =>
            Console.WriteLine("Здравствуйте, {0}. \nУ вас имеется: {1} монет", player.Name, player.Money);

        public void ArmorShopPay(Player player)
        {
            HelloPlayer(player);
            int i = 0;
            foreach (var t in Program.Armor)
                Console.WriteLine("{5}: {0}, {1}, {2}, {3}, {4}", t.Head.Name, t.Body.Name, t.Arms.Name, t.Leggs.Name, t.Boots.Name, ++i);

            Console.WriteLine();
            ObjectStructures.ArmorComplect armor = GetChoice();

            if (armor.GetCost(RealCosts["armor"]) == 0) return;

            if (player.GiveMoney(armor.GetCost(RealCosts["armor"])))
            {
                player.AddArmor(armor);
                Console.WriteLine("вы купили комплект брони");
            }
            else Console.WriteLine("у вас недостаточно средств для покупки");
        }

        public void ArmorShopSell(Player player)
        {
            HelloPlayer(player);
            Console.WriteLine("Вы точно хотите продать вашу броню? \nY / N \n Вы получите {0} монет", player.Armor.GetRealCost(RealCosts["armor"]));
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Y:
                    player.AddMoney(player.Armor.GetRealCost(RealCosts["armor"]));
                    player.AddArmor(new ObjectStructures.ArmorComplect());
                    Console.WriteLine("Вы успешно продали свой комплект брони.\nВаш баланс: {0}", player.Money);
                    return;
                case ConsoleKey.N:
                    return;
            }
            ArmorShopSell(player);
        }

        public void ForgeArmor(Player player)
        {
            HelloPlayer(player);
            Console.WriteLine("Вы можете восстановить {0} единиц брони по стоимости 1 монета за {1} брони.",
                player.Armor.GetHealthToAdd(), player.Armor.GetOnHPCost() * RealCosts["armor"]);
            Console.WriteLine("Введите число единиц, которое вы хотите восстановить.");
            int additionCount = Program.Parse(Console.ReadLine(), 0, 1e18);
            if (player.GiveMoney((int)(additionCount / (player.Armor.GetOnHPCost() * RealCosts["armor"]))))
            {
                player.Armor.Repair(additionCount);
                Console.WriteLine("Вы восстановили {0} единиц", additionCount);
                WriteCharacteristics(player.Armor.GetCharacteristics());
            }
            else Console.WriteLine("У вас недостаточно средств");
        }

        public void WriteCharacteristics(string[] original)
        {
            foreach (var t in original)
                Console.WriteLine(t);
        }

        private ObjectStructures.ArmorComplect GetChoice()
        {
            Console.WriteLine("Введите номер интересующего вас товара");
            var armor = Program.Armor[Program.Parse(Console.ReadLine(), 1, Program.Armor.Count) - 1];

            WriteCharacteristics(armor.GetCharacteristics());
            Console.WriteLine("Итогo:{0}", armor.GetCost(RealCosts["armor"]));
            Console.WriteLine("Если хотите купить, нажмите Y. Если нет, то N");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Y:
                    return armor;
                case ConsoleKey.N:
                    return new ObjectStructures.ArmorComplect();
            }
            return GetChoice();
        }

        private void ByeMedicineKit(Player player)
        {
            HelloPlayer(player);
            Console.WriteLine("Вы можете купить стандартную аптечку:\n50 единиц здоровья за 30 монет");
            Console.WriteLine("Но вы можете заказать с любым числом здоровья");
            Console.WriteLine("Стоить будет это 1.6 hp за 1 монету");
            Console.WriteLine("S для стандартной,\nN для своей,\nEsc для выхода");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.S:
                    ObjectStructures.MedicineKit medicineKit = new ObjectStructures.MedicineKit { HpToAdd = 50 };
                    if (player.GiveMoney(30))
                        player.MedicineKits.Add(medicineKit);
                    break;
                case ConsoleKey.N:
                    Console.WriteLine("Введите нужную емкость");
                    int t = Program.Parse(Console.ReadLine());
                    if (player.GiveMoney((int)(t / 1.6)))
                        player.MedicineKits.Add(new ObjectStructures.MedicineKit { HpToAdd = t });
                    break;
                case ConsoleKey.Escape:
                    return;
            }
        }
    }
}