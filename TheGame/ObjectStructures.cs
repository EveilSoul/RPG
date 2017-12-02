using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public partial class ObjectStructures
    {
        public struct Position
        {
            public int X;
            public int Y;
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
        }

        public struct Armor
        {
            public string Name;
            public int Health;
            public int Cost;
            public float Strength;
            public string Description;

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

            return armor;
        }

    }
}

