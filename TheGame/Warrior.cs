using System.Collections.Generic;

namespace TheGame
{
    public class Warrior : Player
    {
        public Warrior(string name, ObjectStructures.Position position) : base(name, position)
        {
            this.TravelSkill = 0;
            this.BattleSkill = 10;

            this.MaxHealth = 250;
            this.MaxMana = 80;
            this.PowerAttack = 10;

            this.Type = PlayerType.Warrior;

            this.SwordSkill = 0.8f;
            this.BowSkill = 0.4f;
            this.MagicAccuracy = 0.7f;

            this.CurrentHealth = this.MaxHealth;
            this.CurrentMana = this.MaxMana;

            this.AddSword(Program.Swords[0]);
            this.Spells.Add(Program.Spells[3]);

            this.AddArmor(Program.Armor[0]);
        }
    }
}