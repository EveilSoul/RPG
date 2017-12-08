using System;
using System.Collections.Generic;
namespace TheGame
{
    public class City
    {
        public string Name;
        public ObjectStructures.Position Position;
        private int OneCoinCountHP;

        List<string> Places = new List<string>();

        public City(string name, ObjectStructures.Position position)
        {
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
        }

        public static void CheckPlayer(Player player)
        {
            foreach (var t in Program.Cities)
                t.IsCityNear(player);
        }

        public void IsCityNear(Player player)
        {
            if (Program.GetDistance(this.Position, player.Position) < 2)
            {
                Welcome(player);
            }
        }

        public void Hospital(Player player)
        {
            Console.WriteLine("Ваш баланс: {0}", player.Money);
            Console.WriteLine("Вы можете восстановить {0} здоровья", player.MaxHealth - player.CurrentHealth);
            Console.WriteLine("Цена: 1 монета за {0} единиц здоровья", this.OneCoinCountHP);
            Console.WriteLine("Сколько вы хотите восстановить?");
            int hp = int.Parse(Console.ReadLine());
            if (player.GiveMoney(hp / this.OneCoinCountHP))
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
                case ConsoleKey.Escape:
                    return;
            }
            Welcome(player);
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
                WriteCharacteristics(Program.Swords[i].GetCharacteristics(true));
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Y:
                        if (player.Level >= Program.Swords[i].MinLevelToUse &&
                            player.GiveMoney(Program.Swords[i].Cost))
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
                WriteCharacteristics(Program.Spells[i].GetCharacteristics(true));
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Y:
                        if (player.LearnSpell(Program.Spells[i]))
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
            for (int i = 0; i<Program.Bows.Count; i++)
            {
                Console.Clear();
                WriteCharacteristics(Program.Bows[i].GetCharacteristics(true));
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Y:
                        if (player.Level >= Program.Bows[i].MinLevelToUse &&
                            player.GiveMoney(Program.Bows[i].Cost))
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

            if (armor.GetCost() == 0) return;

            if (player.GiveMoney(armor.GetCost()))
            {
                player.AddArmor(armor);
                Console.WriteLine("вы купили комплект брони");
            }
            else Console.WriteLine("у вас недостаточно средств для покупки");
        }

        public void ArmorShopSell(Player player)
        {
            HelloPlayer(player);
            Console.WriteLine("Вы точно хотите продать вашу броню? \nY / N \n Вы получите {0} монет", player.Armor.GetRealCost());
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Y:
                    player.AddMoney(player.Armor.GetRealCost());
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
                player.Armor.GetHealthToAdd(), player.Armor.GetOnHPCost());
            Console.WriteLine("Введите число единиц, которое вы хотите восстановить.");
            int additionCount = Program.Parse(Console.ReadLine());
            if (player.GiveMoney(additionCount / player.Armor.GetOnHPCost()))
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
            var armor = Program.Armor[Program.Parse(Console.ReadLine()) - 1];

            WriteCharacteristics(armor.GetCharacteristics());
            Console.WriteLine("Итогo:{0}", armor.GetCost());
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
    }
}