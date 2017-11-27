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
            public int Health;
            public int PowerAttack;
            public int Mana;
            public int Cost;
            public float Strength;
            public int CountImpact;
            public int MinLevelToUse;
            public string Descriotion;
        }
    }
}
