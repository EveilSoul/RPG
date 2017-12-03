using System.Collections.Generic;
namespace TheGame
{
    public class Ranger : Player
    {
        public Ranger(string name, ObjectStructures.Position position)
        {
            this.TravelSkill = 10;
            this.BattleSkill = 0;
            this.MaxHealth = 220;
            this.CurrentHealth = MaxHealth;
            this.MaxMana = 160;
            this.Money = 200;
            this.PowerAttack = 10;
            this.Level = 1;
            this.Type = PlayerType.Ranger;
            this.SwordAccuracy = 0.75f;
            this.BowAccuracy = 0.9f;
            this.MagicAccuracy = 0.8f;
            this.CurrentMana = this.MaxMana;

            this.Name = name;
            this.Position = position;

            this.Swords = new List<Sword>();
            this.AddSword(Program.Swords[2]);

            this.Spells = new List<Spell>();
            this.Bow = Program.Bows[0];

            this.Armor = new ObjectStructures.ArmorComplect();
            this.AddArmor(Program.Armor[1]);
        }
    }
}