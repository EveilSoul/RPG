using System.Collections.Generic;
namespace TheGame
{
    public class Wizard : Player
    {
        public Wizard(string name, ObjectStructures.Position position)
        {
            this.TravelSkill = 0;
            this.BattleSkill = 0;
            this.Health = 200;
            this.MaxMana = 200;
            this.Money = 200;
            this.PowerAttack = 8;
            this.Level = 1;
            this.Type = PlayerType.Wizard;
            this.SwordAccuracy = 0.7f;
            this.BowAccuracy = 0.5f;
            this.MagicAccuracy = 0.9f;
            this.MagicLevel = 2;
            this.CurrentMana = this.MaxMana;

            this.Swords = new List<Sword>
            {
                Program.Swords[1]
            };
            this.Spells = new List<Spell>
            {
                Program.Spells[0]
            };

            this.Bow = Program.Bows[0];

            this.Name = name;
            this.Position = position;

        }


    }
}