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
            this.Mana = 200;
            this.Money = 200;
            this.PowerAttack = 8;
            this.Level = 1;
            this.Type = PlayerType.Wizard;
            this.SwordAccuracy = 0.7f;
            this.BowAccuracy = 0.5f;
            this.MagicAccuracy = 0.9f;
            this.MagicLevel = 2;
            this.Weapons = new System.Collections.Generic.List<ObjectStructures.Weapons>();
            this.PlayerAttacks = GetAttacks();
            this.Spells = new List<Spell>
            {
                Program.Spells[0]
            };

            this.Name = name;
            this.Position = position;

        }

        private List<Attacks> GetAttacks()
        {
            List<Attacks> result = new List<Attacks>();
            result.Add(SimpleSpellAttack);

            return result;
        }

        public int[] SimpleSpellAttack(int countEmemy, params int[] nums)
        {
            int[] res = this.Spells[0].JoinSpell(countEmemy, nums);
            for (int i = 0; i < res.Length; i++)
            {
                if (Program.Random.NextDouble() <= this.SwordAccuracy)
                    res[i] += this.PowerAttack;
                res[i] += Program.Random.Next(-5, 6);
                if (res[i] < 0) res[i] = 0;
            }
            return res;
        }
    }
}