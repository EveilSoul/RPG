using System.Collections.Generic;
namespace TheGame
{
    class Wizard : Player
    {
        public Wizard(string name, ObjectStructures.Position position) : base(name, position)
        {
            this.BattleSkill = 0;

            this.MaxHealth = 200;
            this.MaxMana = 150;
            this.PowerAttack = 8;

            this.Type = PlayerType.Wizard;

            this.SwordSkill = 0.7f;
            this.BowSkill = 0.5f;
            this.MagicSkill = 0.9f;

            this.CurrentMana = this.MaxMana;
            this.CurrentHealth = this.MaxHealth;

            this.AddSword(Program.Swords[1]);

            this.Spells = new List<Spell> { Program.Spells[0] };
        }

        public override int[] SuperAttack(int enemyCount)
        {
            int[] result = new int[enemyCount];
            this.CurrentMana -= 29 + this.Level * this.Level * this.Level;
            Window.PrintArray(GetCharacteristicsOfWeapons(Weapons.WeaponsType.Spell));
            int index1 = Program.Parse(System.Console.ReadLine(), 0, Spells.Count - 1);
            Window.PrintArray(GetCharacteristicsOfWeapons(Weapons.WeaponsType.Sword));
            int index2 = Program.Parse(System.Console.ReadLine(), 0, Swords.Count - 1);
            if (this.CurrentMana >= 0)
            {
                result = SimpleSpellAttack(enemyCount, this.Spells[index1], 0);
                for (int i = 0; i < enemyCount; i++)
                {
                    result[i] += SimpleSwordAttack(enemyCount, Swords[index2], 0, 0, 0, 0, 0, 0, 0)[0];
                    result[i] *= (int)System.Math.Sqrt(Level) + 3 / Level;
                }
            }
            else CurrentMana += 29 * this.Level * this.Level * this.Level;
            return result;
        }
    }
}