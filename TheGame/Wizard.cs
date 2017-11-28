using System;
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
            this.Accuracy = 0.8f;
            this.Weapons = new System.Collections.Generic.List<ObjectStructures.Weapons>();

            this.Name = name;
            this.Position = position;

        }
    }
}