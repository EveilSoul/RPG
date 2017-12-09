using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public class ObjectStructures
    {
        public struct Position
        {
            public int X;
            public int Y;
        }

        public struct MedicineKit
        {
            public int HpToAdd;

            public void JoinKit(Player player) =>
                player.AddHP(HpToAdd);
        }

        public struct ArmorComplect
        {
            public Armor Head;
            public Armor Body;
            public Armor Arms;
            public Armor Leggs;
            public Armor Boots;

            public int Protect(int damage, int num)
            {
                switch (num)
                {
                    case 0:
                        damage = this.Head.Protect(damage);
                        break;
                    case 1:
                        damage = this.Body.Protect(damage);
                        break;
                    case 2:
                        damage = this.Arms.Protect(damage);
                        break;
                    case 3:
                        damage = this.Leggs.Protect(damage);
                        break;
                    case 4:
                        damage = this.Boots.Protect(damage);
                        break;
                }
                return damage;
            }

            public string[] GetCharacteristics()
            {
                var res = this.Head.GetCharacteristics().ToList<string>();
                res.AddRange(this.Body.GetCharacteristics());
                res.AddRange(this.Arms.GetCharacteristics());
                res.AddRange(this.Leggs.GetCharacteristics());
                res.AddRange(this.Boots.GetCharacteristics());
                return res.ToArray<string>();
            }
            public int GetCost() =>
                this.Head.Cost + this.Body.Cost + this.Arms.Cost + this.Leggs.Cost + this.Boots.Cost;
            public int GetMana() =>
                this.Head.Mana + this.Body.Mana + this.Arms.Mana + this.Leggs.Mana + this.Boots.Mana;
            public int GetRealCost() =>
                this.Head.GetCost() + this.Body.GetCost() + this.Arms.GetCost() + this.Leggs.GetCost() + this.Boots.GetCost();
            public int GetHealthToAdd() =>
                this.Head.GetHealthToAdd() + this.Body.GetHealthToAdd() + this.Arms.GetHealthToAdd() + this.Leggs.GetHealthToAdd() + this.Boots.GetHealthToAdd();
            public int GetOnHPCost() =>
                this.Body.MaxHealth / this.Body.Cost;
            public void Repair(int health)
            {
                var hp = this.Body.Repair(health);
                hp = this.Leggs.Repair(hp);
                hp = this.Head.Repair(hp);
                hp = this.Arms.Repair(hp);
                hp = this.Boots.Repair(hp);
            }
        }

        public struct Armor
        {
            public int MaxHealth;
            public string Name;
            public int Health;
            public int Cost;
            public float Strength;
            public int Mana;
            public string Description;

            public int GetCost() =>
                this.Cost * this.Health / (this.MaxHealth != 0 ? this.MaxHealth : 1);

            public int GetHealthToAdd() => this.MaxHealth - this.Health;

            public int Repair(int hp)
            {
                this.Health += hp;
                if (this.Health > this.MaxHealth)
                {
                    hp = this.Health - this.MaxHealth;
                    this.Health = this.MaxHealth;
                    return hp;
                }
                return 0;
            }

            public int Protect(int damage)
            {
                damage = (int)(damage * (1 - this.Strength));
                this.Health -= (this.Strength == 0) ? 0 : damage / 5;
                if (this.Health <= 0)
                {
                    this.Strength = 0;
                    this.Health = 0;
                }
                return (int)damage;
            }

            public string[] GetCharacteristics()
            {
                return new string[]{ String.Format("Название: {0}", this.Name),
                    String.Format("Количество жизней: {0}", this.Health),
                    String.Format("Стоимость: {0}", this.Cost),
                    String.Format("Прочность: {0}", this.Strength),
                    this.Description + '\n'};
            }
        }

        public static Armor GetArmorFromFile(string file)
        {
            var characteristics = System.IO.File.ReadAllLines(file);
            Armor armor = new Armor();

            armor.Name = characteristics[0];
            armor.Health = int.Parse(characteristics[1]);
            armor.Cost = int.Parse(characteristics[2]);
            armor.Strength = float.Parse(characteristics[3]);
            armor.Description = characteristics[4];

            if (characteristics.Length != 5)
                armor.Mana = int.Parse(characteristics[5]);

            armor.MaxHealth = armor.Health;
            return armor;
        }

    }
}

