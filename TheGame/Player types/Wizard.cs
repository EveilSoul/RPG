using System.Collections.Generic;
namespace TheGame
{
    /// <summary>
    /// Класс мага
    /// </summary>
    class Wizard : Player
    {
        // Волшебник имеет среднее здоровье и атаку, но зато у него много маны
        public Wizard(string name, MainGameStructures.Position position) : base(name, position)
        {
            this.BattleSkill = 0;

            this.MaxHealth = 200;
            this.MaxMana = 250;
            this.PowerAttack = 8;

            this.Type = PlayerType.Wizard;

            this.SwordSkill = 0.7f;
            this.BowSkill = 0.5f;
            this.MagicSkill = 0.9f;

            this.CurrentMana = this.MaxMana;
            this.CurrentHealth = this.MaxHealth;

            this.AddSword(Program.Swords[1]);
            this.TryOnProtection(Program.ProtectionThings[1]);
            this.Spells = new List<Spell> { Program.Spells[0] };
        }

        /// <summary>
        /// Супер-атака мага, удар мечом и заклинанием, но усиленный в несколько раз
        /// </summary>
        /// <param name="enemyCount"> количество противников </param>
        /// <returns> массив с уроном </returns>
        public override int[] SuperAttack(int enemyCount)
        {
            int[] result = new int[enemyCount];
            this.CurrentMana -= 29 + this.Level * this.Level * this.Level;

            // выбираем, чем будем атаковать
            Window.PrintArray(GetCharacteristicsOfWeapons(Weapons.WeaponsType.Spell));
            int index1 = Program.Parse(System.Console.ReadLine(), 0, Spells.Count - 1);
            Window.PrintArray(GetCharacteristicsOfWeapons(Weapons.WeaponsType.Sword));
            int index2 = Program.Parse(System.Console.ReadLine(), 0, Swords.Count - 1);

            // атакуем, если можем
            if (this.CurrentMana >= 0)
            {
                result = this.Spells[index1].JoinSpell(enemyCount,0);
                for (int i = 0; i < enemyCount; i++)
                {
                    result[i] += SimpleSwordAttack(enemyCount, Swords[index2], 0, 0, 0, 0, 0, 0, 0)[0];
                    if (this.Bow != null)
                        result[i] += this.Bow.Attack();
                    else result[i] += SimpleSwordAttack(enemyCount, Swords[index2], 0, 0, 0, 0, 0, 0, 0)[0];
                }
            }
            else CurrentMana += 29 * this.Level * this.Level;

            return result;
        }
    }
}