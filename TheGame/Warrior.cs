using System.Collections.Generic;

namespace TheGame
{
    public class Warrior : Player
    {
        public Warrior(string name, ObjectStructures.Position position)
        {
            this.TravelSkill = 0;
            this.BattleSkill = 10;
            this.MaxHealth = 250;
            this.CurrentHealth = MaxHealth;
            this.MaxMana = 80;
            this.Money = 200;
            this.PowerAttack = 10;
            this.Level = 1;
            this.Type = PlayerType.Warrior;
            this.SwordAccuracy = 0.8f;
            this.BowAccuracy = 0.4f;
            this.MagicAccuracy = 0.7f;
            this.CurrentMana = this.MaxMana;

            this.Swords = new List<Sword>();
            this.AddSword(Program.Swords[0]);

            this.Spells = new List<Spell> { Program.Spells[3] };

            this.Armor = new ObjectStructures.ArmorComplect();
            this.AddArmor(Program.Armor[0]);

            this.Name = name;
            this.Position = position;
        }
    }
}