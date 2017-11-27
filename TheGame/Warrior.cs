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
            this.Inventory = new object[10];
            this.Level = 1;
            this.Type = PlayerType.Warrior;
            this.Accuracy = 0.8f;

            this.Name = name;
            this.Position = position;
        }
    }
}