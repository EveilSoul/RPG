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
            this.Weapons = new System.Collections.Generic.List<ObjectStructures.Weapons>
            {
                ObjectStructures.GetWeaponsFromFile(System.Environment.CurrentDirectory + @"\TextFiles\Swords\baseWarriorSword.txt")
            };

            this.Name = name;
            this.Position = position;
        }
    }
}