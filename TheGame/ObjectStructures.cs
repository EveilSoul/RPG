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

        public struct ArmorComplect
        {
            public Armor Head;
            public Armor Body;
            public Armor Arms;
            public Armor Leggs;
            public Armor Boots;
        }

        public struct Armor
        {
            public string Name;
            public int Health;
            public int Cost;
            public float Strength;
            public string Description;
        }

        public struct Weapons
        {
            public string Name;
            public float Health;
            public int PowerAttack;
            public int Mana;
            public int Cost;
            public float Strength;
            public int CountImpact;
            public int MinLevelToUse;
            public string Descriotion;

            public int Attack()
            {
                if (this.Health > 0)
                {
                    this.Health -= (1 - this.Strength) / 2;
                }
                else Die();
                return PowerAttack;
            }

            private void Die()
            {
                this.Descriotion = null;
            }
        }

        public static Weapons GetWeaponsFromFile(string file)
        {
            var characteristics = System.IO.File.ReadAllLines(file);
            Weapons weapons = new Weapons();

            weapons.Name = characteristics[0];
            weapons.Health = int.Parse(characteristics[1]);
            weapons.PowerAttack = int.Parse(characteristics[2]);
            weapons.Mana = int.Parse(characteristics[3]);
            weapons.Cost = int.Parse(characteristics[4]);
            weapons.Strength = float.Parse(characteristics[5]);
            weapons.CountImpact = int.Parse(characteristics[6]);
            weapons.MinLevelToUse = int.Parse(characteristics[7]);

            for (int i = 8; i < characteristics.Length; i++)
                weapons.Descriotion += characteristics[i] + "\n";

            return weapons;
        }
    }
}
