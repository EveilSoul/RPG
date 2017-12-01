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
        public float SwordAccuracy;
        public float BowAccuracy;
        public float MagicAccuracy;

        public delegate int[] Attacks(int countEnemy, params int[] nums);

        public enum PlayerType
        {
            Warrior,
            Ranger,
            Wizard
        };

        public List<ObjectStructures.Weapons> Weapons;
        public ObjectStructures.Position Position;
        public ObjectStructures.ArmorComplect Armor;
        public List<Attacks> PlayerAttacks;
            
        public static char PlayerSymble = '@';

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

        public void JoinGame()
        {
            int moveX = 0, moveY = 0;
            Window.DrowWindow();
            while (true)
            {
                KeyDown(Console.ReadKey(true).Key, ref moveX, ref moveY);
                //ApplyDamage(15);
                Window.ClearAndDrow(moveX, moveY);

                if (Math.Abs(moveX) == Window.WindowSizeX / 2 || Math.Abs(moveY) == Window.WindowSizeY / 2)
                {
                    moveX = 0;
                    moveY = 0;
                    Window.ClearAndDrow(moveX, moveY);
                }
                DrawChracteristics();

                
            }
        }

        public void KeyDown(ConsoleKey ch, ref int moveX, ref int moveY)
        {
            switch (ch)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    this.Walk(0, 1);
                    moveY--;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    this.Walk(0, -1);
                    moveY++;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    this.Walk(-1, 0);
                    moveX--;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    this.Walk(1, 0);
                    moveX++;
                    break;
                case ConsoleKey.I:
                    //info
                    break;
                case ConsoleKey.H:
                    //help
                    break;
                case ConsoleKey.F1:
                    //save
                    break;
                case ConsoleKey.F2:
                    //load game
                    break;
            }
        }

        public void DrawChracteristics()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Health: {0}", this.Health);
            Console.WriteLine("Level: {0}", this.Level);
            Console.WriteLine("Position: ({0};{1})", this.Position.X, this.Position.Y);
        }

        public void ApplyDamage(int damage)
        {
            var t = Program.Random.Next(0, 5);
            Console.WriteLine(t);
            damage = this.Armor.Protect(damage, t);
            Health -= damage;
        }
    }
}
