﻿using System.Collections.Generic;
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

            this.AddArmor(Program.Armor[1]);
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
                    result[i] += this.Bow.Attack();
                    result[i] += this.Bow.Attack();
                    result[i] += this.SimpleSwordAttack(1, Swords[index], 0, 0)[0];
                }
            }
            else CurrentMana += 29 * this.Level * this.Level;
            return result;
        }
    }
}