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
            this.Weapons = new System.Collections.Generic.List<ObjectStructures.Weapons>
            {
                ObjectStructures.GetWeaponsFromFile(System.Environment.CurrentDirectory + @"\TextFiles\Swords\baseRangerBow.txt")
            };
        }
    }
}