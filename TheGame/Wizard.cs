using System.Collections.Generic;
namespace TheGame
{
    public class Wizard : Player
    {
        public Wizard(string name, ObjectStructures.Position position) : base(name, position)
        {
            this.TravelSkill = 0;
            this.BattleSkill = 0;

            this.MaxHealth = 200;
            this.MaxMana = 150;
            this.PowerAttack = 8;

            this.Type = PlayerType.Wizard;

            this.SwordAccuracy = 0.7f;
            this.BowAccuracy = 0.5f;
            this.MagicAccuracy = 0.9f;

            this.CurrentMana = this.MaxMana;
            this.CurrentHealth = this.MaxHealth;

            this.AddSword(Program.Swords[1]);

            this.Spells = new List<Spell> { Program.Spells[0] };
        }


    }
}