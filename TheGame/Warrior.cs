using System.Collections.Generic;

namespace TheGame
{
    public class Warrior : Player
    {
        public Warrior(string name, ObjectStructures.Position position)
        {
            this.TravelSkill = 0;
            this.BattleSkill = 10;
            this.Health = 250;
            this.Mana = 80;
            this.Money = 200;
            this.PowerAttack = 10;
            this.Level = 1;
            this.Type = PlayerType.Warrior;
            this.SwordAccuracy = 0.8f;
            this.BowAccuracy = 0.4f;
            this.MagicAccuracy = 0.7f;
            this.PlayerAttacks = GetAttacks();

            this.Weapons = new List<ObjectStructures.Weapons>
            {
                Program.Swords[0]
                //ObjectStructures.GetWeaponsFromFile(Program.Path + @"\TextFiles\Swords\baseWarriorSword.txt")
            };
            this.Armor = Program.Armor[0];

            this.Name = name;
            this.Position = position;
        }

        private List<Attacks> GetAttacks()
        {
            List<Attacks> result = new List<Attacks>();
            result.Add(SimpleSwordAttack);
            //Add attacks there, if you create it
            return result;
        }

        public int[] SimpleSwordAttack(int countEnemy, params int[] number)
        {
            int[] result = new int[countEnemy];
            int damage = this.Weapons[0].Attack();

            double luck = Program.Random.NextDouble();
            if (luck <= this.SwordAccuracy)
                result[number[0]] = damage + Program.Random.Next(-5,6);
            if (luck % 5 != 0)
                result[number[0]] += this.PowerAttack;
            return result;
        }
    }
}