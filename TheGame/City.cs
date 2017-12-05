using System;
using System.Collections.Generic;
namespace TheGame
{
    public class City
    {
        public string Name;
        public ObjectStructures.Position Position;

        List<string> Places = new List<string>();

        public City(string name, ObjectStructures.Position position)
        {
            this.Name = name;
            this.Position = position;
            Places.Add("Магазин для покупки брони");
            Places.Add("Магазин для продажи брони");
            Places.Add("Ремонт брони");
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

        public void Welcome(Player player)
        {
            Console.WriteLine("Добро пожаловать в {0}, {1}", this.Name, player.Name);
            int i = 0;
            foreach (var place in Places)
                Console.WriteLine("{1}: {0}", ++i, place);
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
                case ConsoleKey.Escape:
                    return;
            }
            Welcome(player);
        }

        public void ArmorShopPay(Player player)
        {
            int i = 0;
            Console.WriteLine("Здравствуйте, {0}. \n У вас имеется: {1} монет", player.Name, player.Money);
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
            Console.WriteLine("Здравствуйте, {0}", player.Name);
            Console.WriteLine("Вы точно хотите продать вашу броню? Y / N \n Вы получите {0} монет", player.Armor.GetRealCost());
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Y:
                    player.AddMoney(player.Armor.GetRealCost());
                    player.AddArmor(new ObjectStructures.ArmorComplect());
                    Console.WriteLine("Вы успешно продали свой комплект брони. Ваш баланс: {0}", player.Money);
                    return;
                case ConsoleKey.N:
                    return;
            }
            ArmorShopSell(player);
        }

        public void ForgeArmor(Player player)
        {
            Console.WriteLine("Вы можете восстановить {0} единиц брони по стоимости 1 монета за {1} брони.",
                player.Armor.GetHealthToAdd(), player.Armor.GetOnHPCost());
            Console.WriteLine("Введите число единиц, которое вы хотите восстановить.");
            int additionCount = int.Parse(Console.ReadLine());
            if (player.GiveMoney(additionCount/player.Armor.GetOnHPCost()))
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
            var armor = Program.Armor[int.Parse(Console.ReadLine()) - 1];

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