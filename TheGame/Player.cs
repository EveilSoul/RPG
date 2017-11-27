using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public class Player
    {
        public string Name;
        public int Health;
        public int PowerAttack;
        public int Mana;
        public int Money;
        public int BattleSkill;
        public int TravelSkill;
        public PlayerType Type;
        public int Level;
        public float Accuracy;

        public enum PlayerType
        {
            Warrior,
            Ranger,
            Wizard
        };

        public ObjectStructures.Position Position;
        public ObjectStructures.ArmorComplect Armor;
        public Object[] Inventory;

        public char PlayerSymble = '@';

        public void Walk(int x, int y)
        {
            this.Position.X += x;
            this.Position.Y += y;
        }

        protected void ChangeBattleLevel()
        {
            int increase = (int)Math.Sqrt(this.BattleSkill) + 500 / this.BattleSkill;

            this.Health += increase;

            switch (this.Type)
            {
                case PlayerType.Ranger:
                    this.Mana += increase;
                    this.PowerAttack += (2 * increase) / 3;
                    break;
                case PlayerType.Warrior:
                    this.Mana += increase / 2;
                    this.PowerAttack += increase;
                    break;
                case PlayerType.Wizard:
                    this.Mana += 2 * increase;
                    this.PowerAttack += increase / 2;
                    break;
            }
        }
    }
}
