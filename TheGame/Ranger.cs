using System.Collections.Generic;
namespace TheGame
{
    public class Ranger : Player
    {
        public Ranger(string name, ObjectStructures.Position position) : base(name, position)
        {
            this.TravelSkill = 10;
            this.BattleSkill = 0;

            this.MaxHealth = 220;
            this.MaxMana = 160;
            this.PowerAttack = 10;

            this.Type = PlayerType.Ranger;

            this.SwordSkill = 0.75f;
            this.BowSkill = 0.9f;
            this.MagicAccuracy = 0.8f;

            this.CurrentMana = this.MaxMana;
            this.CurrentHealth = this.MaxHealth;

            this.AddSword(Program.Swords[2]);
            this.Bow = Program.Bows[0];
            this.Spells.Add(Program.Spells[0]);

            this.AddArmor(Program.Armor[1]);
        }
    }
}