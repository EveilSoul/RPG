﻿namespace TheGame
{
    public class Spell
    {
        public string Name;
        public int Mana;
        public int Level;
        public string Description;
        public TypeOfSpell Type;

        public int CountImpact;
        public int Damage;
        public float Accuracy;

        public int CountOfProtect;
        public float Protect;

        public enum TypeOfSpell
        {
            Defence,
            Attacking
        };

        public Spell(string name, int mana, int level, string description, 
            TypeOfSpell type, params float[] param)
        {
            this.Name = name;
            this.Mana = mana;
            this.Level = level;
            this.Description = description;
            this.Type = type;

            switch (type)
            {
                case TypeOfSpell.Attacking:
                    this.CountImpact = (int)param[0];
                    this.Damage = (int)param[1];
                    this.Accuracy = param[2];
                    break;
                case TypeOfSpell.Defence:
                    this.CountOfProtect = (int)param[0];
                    this.Protect = param[1];
                    break;
            }
        }
    }
}