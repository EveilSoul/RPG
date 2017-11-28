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
            this.Inventory = new object[10];
            this.Level = 1;
            this.Type = PlayerType.Ranger;
            this.Accuracy = 0.8f;

            this.Name = name;
            this.Position = position;
        }
    }
}