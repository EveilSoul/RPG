using System.Collections.Generic;
namespace TheGame
{
    public class Ranger : Player
    {
        public Ranger(string name, ObjectStructures.Position position)
        {
            this.TravelSkill = 10;
            this.BattleSkill = 0;
            this.Health = 220;
            this.Mana = 160;
            this.Money = 200;
            this.PowerAttack = 10;
            this.Level = 1;
            this.Type = PlayerType.Ranger;
            this.SwordAccuracy = 0.75f;
            this.BowAccuracy = 0.9f;
            this.MagicAccuracy = 0.8f;

            this.Name = name;
            this.Position = position;
            this.PlayerAttacks = GetAttacks();
            this.Weapons = new System.Collections.Generic.List<ObjectStructures.Weapons>
            {
                ObjectStructures.GetWeaponsFromFile(System.Environment.CurrentDirectory + @"\TextFiles\Swords\baseRangerBow.txt") 
            };
        }

        List<Attacks> GetAttacks()
        {
            var result = new List<Attacks>();
            result.Add(SimpleBowAttack);

            return result;
        }

        public int[] SimpleBowAttack(int countEnemy, params int[] nums)
        {
            int[] result = new int[countEnemy];

            for (int i = 0; i < countEnemy; i++)
            {
                if (Program.Random.NextDouble() <= this.BowAccuracy)
                    result[i] += Weapons[0].Attack();
                if (Program.Random.NextDouble() <= this.SwordAccuracy)
                    result[i] += this.PowerAttack;
                result[i] += Program.Random.Next(-5, 6);
                if (result[i] < 0) result[i] = 0;
            }

            return result;
        }
    }
}