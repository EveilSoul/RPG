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
        public int MaxMana;
        public int CurrentMana;
        public int Money;
        public int BattleSkill;
        public int TravelSkill;
        public PlayerType Type;
        public int Level;
        public float SwordAccuracy;
        public float BowAccuracy;
        public float MagicAccuracy;
        public int MagicLevel = 1;

        public delegate int[] Attacks(int countEnemy, int ingexOfWeapons, params int[] nums);

        public enum PlayerType
        {
            Warrior,
            Ranger,
            Wizard
        };

        public ObjectStructures.Position Position;
        public ObjectStructures.ArmorComplect Armor;
        public List<Sword> Swords;
        public Bow Bow;
        public List<Spell> Spells;

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
                    this.MaxMana += increase;
                    this.PowerAttack += (2 * increase) / 3;
                    break;
                case PlayerType.Warrior:
                    this.MaxMana += increase / 2;
                    this.PowerAttack += increase;
                    break;
                case PlayerType.Wizard:
                    this.MaxMana += 2 * increase;
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
                Window.ClearAndDrow(moveX, moveY);

                if (Math.Abs(moveX) == Window.WindowSizeX / 2 || Math.Abs(moveY) == Window.WindowSizeY / 2)
                {
                    moveX = 0;
                    moveY = 0;
                    Window.ClearAndDrow(moveX, moveY);

                }
                DrawChracteristics();
                KeyDown(Console.ReadKey(true).Key, ref moveX, ref moveY);
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

        private bool HaveMana(int mana)
        {
            this.CurrentMana -= mana;

            if (this.CurrentMana >= 0)
                return true;
            else this.CurrentMana += mana;

            return false;
        }

        public int[] Attack(int countEnemy,
            Weapons weapons, Bow bow = null, Spell spell = null, Sword sword = null, params int[] nums)
        {
            switch (weapons.TypeOfWeapons)
            {
                case Weapons.WeaponsType.Bow:
                    return SimpleBowAttack(countEnemy, bow);
                case Weapons.WeaponsType.Spell:
                    return SimpleSpellAttack(countEnemy, spell, nums);
                case Weapons.WeaponsType.Sword:
                    return SimpleSwordAttack(countEnemy, sword, nums);
            }
            return new int[0];
        }

        public int[] SimpleSpellAttack(int countEmemy, Spell spell, params int[] nums)
        {
            int[] res = spell.JoinSpell(countEmemy, nums);
            if (HaveMana(spell.Mana))
                for (int i = 0; i < res.Length; i++)
                {
                    if (Program.Random.NextDouble() > this.MagicAccuracy)
                        res[i] = 0;
                    if (Program.Random.NextDouble() <= this.SwordAccuracy)
                    {
                        res[i] += this.PowerAttack;
                        if (this.Type != PlayerType.Wizard)
                        {
                            res[i] -= (int)(Program.Random.NextDouble() * PowerAttack);
                        }
                    }
                    res[i] += Program.Random.Next(-5, 6);
                    if (res[i] < 0) res[i] = 0;
                }
            return res;
        }

        public int[] SimpleSwordAttack(int countEnemy, Sword sword, params int[] number)
        {
            int[] result = new int[countEnemy];
            int damage = sword.Attack();

            for (int i = 0; i < sword.CountImpact; i++)
            {
                double luck = Program.Random.NextDouble();
                if (luck <= this.SwordAccuracy)
                    result[number[i]] = damage;
                if (luck % 5 != 0)
                    result[number[i]] += this.PowerAttack;
                result[number[i]] += Program.Random.Next(-5, 6);
                if (result[i] < 0) result[i] = 0;
            }
            return result;
        }

        public int[] SimpleBowAttack(int countEnemy, Bow bow)
        {
            int[] result = new int[countEnemy];

            for (int i = 0; i < countEnemy; i++)
            {
                if (Program.Random.NextDouble() <= this.BowAccuracy)
                    result[i] += bow.Attack();
                if (Program.Random.NextDouble() <= this.SwordAccuracy)
                {
                    if (this.Type == PlayerType.Ranger)
                        result[i] += this.PowerAttack;
                    else
                        result[i] += this.PowerAttack / (this.Level + 1);
                }
                result[i] += Program.Random.Next(-5, 6);
                if (result[i] < 0) result[i] = 0;
            }

            return result;
        }
    }
}
