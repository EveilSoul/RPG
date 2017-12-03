using System.Collections.Generic;
namespace TheGame
{
    public class Wizard : Player
    {
        public Wizard(string name, ObjectStructures.Position position)
        {
            this.TravelSkill = 0;
            this.BattleSkill = 0;
            this.MaxHealth = 200;
            this.CurrentHealth = MaxHealth;
            this.MaxMana = 150;
            this.Money = 200;
            this.PowerAttack = 8;
            this.Level = 1;
            this.Type = PlayerType.Wizard;
            this.SwordAccuracy = 0.7f;
            this.BowAccuracy = 0.5f;
            this.MagicAccuracy = 0.9f;
            this.MagicLevel = 2;
            this.CurrentMana = this.MaxMana;

            this.Armor = new ObjectStructures.ArmorComplect();

            this.Swords = new List<Sword>();
            this.AddSword(Program.Swords[1]);

            this.Spells = new List<Spell>
            {
                Program.Spells[0]
            };

            this.Name = name;
            this.Position = position;

        }


    }
}