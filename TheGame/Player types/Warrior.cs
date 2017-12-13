using System.Collections.Generic;

namespace TheGame
{
    public class Warrior : Player
    {
        public Warrior(string name, ObjectStructures.Position position) : base(name, position)
        {
            this.BattleSkill = 10;

            this.MaxHealth = 250;
            this.MaxMana = 80;
            this.PowerAttack = 10;

            this.Type = PlayerType.Warrior;

            this.SwordSkill = 0.8f;
            this.BowSkill = 0.4f;
            this.MagicSkill = 0.7f;

            this.CurrentHealth = this.MaxHealth;
            this.CurrentMana = this.MaxMana;

            this.AddSword(Program.Swords[0]);
            this.Spells.Add(Program.Spells[3]);

            this.AddArmor(Program.Armor[0]);
        }

        public override int[] SuperAttack(int enemyCount)
        {
            int[] result = new int[enemyCount];
            this.CurrentMana -= 29 + this.Level * this.Level;
            Window.PrintArray(GetCharacteristicsOfWeapons(Weapons.WeaponsType.Sword));
            int index = Program.Parse(System.Console.ReadLine(), 0, Swords.Count - 1);
            if (this.CurrentMana >= 0)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    result[i] += Swords[index].Attack();
                    result[i] += this.SimpleSwordAttack(1, Swords[index], 0, 0)[0];
                    result[i] += Program.Random.Next(0, this.Level * this.Level);
                }
            }
            else CurrentMana += 29 * this.Level * this.Level;
            return result;
        }
    }
}