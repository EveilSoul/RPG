using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public class Player
    {
        public int NextLevelBorder;
        public string Name;
        public int CurrentHealth;
        public int MaxHealth;
        public int PowerAttack;
        public int MaxMana;
        public int CurrentMana;
        public int Money;
        public int BattleSkill;
        public int TravelSkill;
        public PlayerType Type;
        public int Level;
        public float SwordSkill;
        public float BowSkill;
        public float MagicAccuracy;
        public int MagicLevel = 1;
        public bool IsLive;
        public List<ObjectStructures.MedicineKit> MedicineKits;
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

        public Player(string name, ObjectStructures.Position position)
        {
            this.Name = name;
            this.Position = position;
            this.Level = 1;
            this.MagicLevel = 1;
            this.Money = 200;
            this.NextLevelBorder = 150;

            this.Swords = new List<Sword>();
            this.Armor = new ObjectStructures.ArmorComplect();
            this.Spells = new List<Spell>();
            this.MedicineKits = new List<ObjectStructures.MedicineKit>()
            {
                new ObjectStructures.MedicineKit{ HpToAdd = 50 },
                new ObjectStructures.MedicineKit{ HpToAdd = 50 },
                new ObjectStructures.MedicineKit{ HpToAdd = 50 },
                new ObjectStructures.MedicineKit{ HpToAdd = 50 },
                new ObjectStructures.MedicineKit{ HpToAdd = 50 },
                new ObjectStructures.MedicineKit{ HpToAdd = 150 }
            };
            this.IsLive = true;
        }

        /// <summary>
        /// Перемещение
        /// </summary>
        /// <param name="x">По оси X</param>
        /// <param name="y">По оси Y</param>
        public void Walk(int x, int y)
        {
            this.Position.X += x;
            this.Position.Y += y;
        }

        /// <summary>
        /// Повышение уровня персонажа
        /// </summary>
        public void ChangeBattleLevel()
        {
            this.Level++;
            this.NextLevelBorder += 150 * (int)Math.Sqrt(this.Level);
            int increase = (int)Math.Sqrt(this.BattleSkill) + 500 / this.BattleSkill;
            this.MaxHealth += increase;
            switch (this.Type)
            {
                case PlayerType.Ranger:
                    this.MaxMana += increase;
                    this.PowerAttack += (2 * increase) / 3;
                    this.BowSkill += 0.001f * this.Level;
                    this.SwordSkill += 0.005f * this.Level;
                    this.MagicAccuracy += 0.005f * this.Level;
                    if (this.Level % 3 == 0)
                        this.MagicLevel++;
                    break;
                case PlayerType.Warrior:
                    this.MaxMana += increase / 2;
                    this.PowerAttack += increase;
                    this.SwordSkill += 0.001f * this.Level;
                    this.MagicAccuracy += 0.005f * this.Level;
                    this.BowSkill += 0.007f * this.Level;
                    if (this.Level % 4 == 0)
                        this.MagicLevel++;
                    break;
                case PlayerType.Wizard:
                    this.MaxMana += 2 * increase;
                    this.PowerAttack += increase / 2;
                    this.MagicAccuracy += 0.001f * this.Level;
                    this.SwordSkill += 0.005f * this.Level;
                    this.BowSkill += 0.007f * this.Level;
                    if (this.Level % 2 == 0)
                        this.MagicLevel++;
                    break;
            }
            this.CurrentHealth = this.MaxHealth;
            this.CurrentMana = this.MaxMana;
        }

        /// <summary>
        /// Метод для начала игры
        /// </summary>
        public void JoinGame()
        {
            int moveX = 0, moveY = 0;
            Window.DrowWindow();

            var city = City.IsSityNear(this.Position);
            Window.DrowCity(city, this.Position);

            var treasure = new Treasure(this);
            Window.DrowTreasure(treasure.Position, this.Position);
            Treasure.TheLastTreasurePosition = treasure.Position;

            //Item1-позиция монстров, Item2-лист монстров
            var enemy = Enemy.CreateEnemy(this.Level, this.Position);
            Enemy.TheLastEnemyPosition = enemy.Item1;
            if (Enemy.EnemyExist && !enemy.Item2[0].Mimicry) Window.DrowEnemy(enemy.Item1, this.Position);

            while (this.IsLive)
            {
                Enemy.CheckEnemy(enemy.Item2, this, enemy.Item1);
                if (Battle.MayNewBattle(Enemy.TheLastEnemyPosition, this.Position))
                {
                    enemy = Enemy.CreateEnemy(this.Level, this.Position);
                    if (Enemy.EnemyExist && !enemy.Item2[0].Mimicry)
                        Window.DrowEnemy(enemy.Item1, this.Position, moveX, moveY);

                    Battle.TheBattleWas = false;
                    Enemy.TheLastEnemyPosition = enemy.Item1;
                }

                City.CheckPlayer(this);

                treasure.CkeckTreasure(this);

                if (treasure.MayNewTreasure(Treasure.TheLastTreasurePosition, treasure.Position))
                {
                    treasure = new Treasure(this);
                    Window.DrowTreasure(treasure.Position, this.Position, moveX, moveY);
                    Treasure.TheLastTreasurePosition = treasure.Position;
                }


                //если произошел выход за границу карты
                if (Math.Abs(moveX) == Window.MapSizeX / 2 || Math.Abs(moveY) == Window.MapSizeY / 2)
                {
                    if (Enemy.EnemyExist && !enemy.Item2[0].Mimicry)
                        Window.DrowEnemy(enemy.Item1, this.Position);
                    //else
                    //{
                    //    Window.ClearMap(Window.Map, Window.EnemySymble);
                    //}

                    
                    Window.DrowTreasure(treasure.Position, this.Position);


                    city = City.IsSityNear(this.Position);
                    Window.DrowCity(city, this.Position);
                    moveX = 0;
                    moveY = 0;
                    Window.PrintMovePlayerOnMap(moveX, moveY);

                }

                Window.PrintMovePlayerOnMap(moveX, moveY);
                Window.PrintCharacteristic(this);
                KeyDown(Console.ReadKey(true).Key, ref moveX, ref moveY);
            }
        }

        /// <summary>
        /// Обработка нажатий на клавиатуру
        /// </summary>
        /// <param name="ch">Нажатая клавиша</param>
        /// <param name="moveX">Сдвиг по оси X от центра</param>
        /// <param name="moveY">Сдвиг по оси Y от центра</param>
        public void KeyDown(ConsoleKey ch, ref int moveX, ref int moveY)
        {
            switch (ch)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    this.Walk(0, -1);
                    moveY--;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    this.Walk(0, 1);
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
                case ConsoleKey.H:
                    //help
                    break;
                case ConsoleKey.F1:
                    UseMedicineKit();
                    break;
            }
        }
        /// <summary>
        /// Метод для супер-атаки
        /// </summary>
        /// <param name="enemyCount">Количество врагов</param>
        /// <returns></returns>
        public virtual int[] SuperAttack(int enemyCount)
        {
            return new int[enemyCount];
        }

        /// <summary>
        /// Нанесение урона персонажу
        /// </summary>
        /// <param name="damage">Количество урона</param>
        public void ApplyDamage(int damage)
        {
            var t = Program.Random.Next(0, 5);
            Console.WriteLine(t);
            damage = this.Armor.Protect(damage, t);
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
                this.IsLive = false;
        }

        /// <summary>
        /// Проверяет, есть ли заданное количество маны у персонажа и забирает ее
        /// </summary>
        /// <param name="mana">Сколько маны нужно потратить</param>
        /// <returns></returns>
        private bool HaveMana(int mana)
        {
            this.CurrentMana -= mana;

            if (this.CurrentMana >= 0)
                return true;
            else this.CurrentMana += mana;

            return false;
        }

        /// <summary>
        /// Нанесение атаки врагу
        /// </summary>
        /// <param name="countEnemy"></param>
        /// <param name="weapons"></param>
        /// <param name="bow"></param>
        /// <param name="spell"></param>
        /// <param name="sword"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int[] Attack(int countEnemy,
            Weapons.WeaponsType weapons, Bow bow = null, Spell spell = null, Sword sword = null, params int[] nums)
        {
            switch (weapons)
            {
                case Weapons.WeaponsType.Bow:
                    this.BowSkill += 0.0001f;
                    return SimpleBowAttack(countEnemy, bow);
                case Weapons.WeaponsType.Spell:
                    this.MagicAccuracy += 0.0001f;
                    return SimpleSpellAttack(countEnemy, spell, nums);
                case Weapons.WeaponsType.Sword:
                    this.SwordSkill += 0.0001f;
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
                    if (Program.Random.NextDouble() <= this.SwordSkill)
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
                if (luck <= this.SwordSkill)
                    result[number[i]] = damage;
                if (luck % 5 != 0)
                    result[number[i]] += this.PowerAttack;
                result[number[i]] += Program.Random.Next(-5, 6);
                if (result[number[i]] < 0) result[number[i]] = 0;
            }
            return result;
        }

        public int[] SimpleBowAttack(int countEnemy, Bow bow)
        {
            int[] result = new int[countEnemy];

            for (int i = 0; i < countEnemy; i++)
            {
                if (Program.Random.NextDouble() <= this.BowSkill)
                    result[i] += bow.Attack();
                if (Program.Random.NextDouble() <= this.SwordSkill)
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

        public bool GiveMoney(int count)
        {
            this.Money -= count;
            if (Money >= 0)
                return true;
            this.Money += count;
            return false;
        }

        public void AddMoney(int count) => this.Money += count;

        public void AddArmor(ObjectStructures.ArmorComplect armor)
        {
            this.MaxMana -= this.Armor.GetMana();
            this.Armor = armor;
            this.MaxMana += armor.GetMana();
            this.CurrentMana = this.MaxMana;
        }

        public void AddSword(Sword sword)
        {
            if (sword.MinLevelToUse <= this.Level)
            {
                this.Swords.Add(sword);
                this.MaxMana += sword.Mana;
            }
        }

        public bool LearnSpell(Spell spell, float cost = 1)
        {
            if (spell.MinLevelToUse <= this.MagicLevel && (int)(spell.Cost * cost) <= this.Money)
            {
                this.Money -= (int)(spell.Cost * cost);
                this.Spells.Add(spell);
                return true;
            }
            else return false;
        }

        public string[] GetCharacteristicsOfWeapons(Weapons.WeaponsType type)
        {
            switch (type)
            {
                case Weapons.WeaponsType.Bow:
                    return this.Bow.GetCharacteristics();
                case Weapons.WeaponsType.Sword:
                    List<string> result = new List<string>();
                    foreach (var t in this.Swords)
                        result.AddRange(t.GetCharacteristics().ToList());
                    return result.ToArray();
                case Weapons.WeaponsType.Spell:
                    List<string> res = new List<string>();
                    foreach (var t in this.Spells)
                        res.AddRange(t.GetCharacteristics().ToList());
                    return res.ToArray();
            }
            return null;
        }

        public Weapons.WeaponsType SelectType()
        {
            if (this.Swords.Count != 0)
                Console.WriteLine("1: Меч");
            if (this.Bow != null)
                Console.WriteLine("2: Лук");
            if (this.Spells.Count != 0)
                Console.WriteLine("3. Заклинание");
            var t = Console.ReadLine();
            return (Weapons.WeaponsType)(Program.Parse(t != String.Empty ? t : "1") - 1);
        }

        public Sword GetSword(int index)
        {
            if (index >= 0 && index < this.Swords.Count)
                return this.Swords[index];
            return null;
        }

        public Spell GetSpell(int index)
        {
            if (index >= 0 && index < this.Spells.Count)
                return this.Spells[index];
            return null;
        }

        public void AddHP(int count)
        {
            this.CurrentHealth += count;
            if (this.CurrentHealth > this.MaxHealth)
                this.CurrentHealth = this.MaxHealth;
        }

        public void UseMedicineKit()
        {
            if (this.MedicineKits.Count != 0)
            {
                for (int i = 0; i < this.MedicineKits.Count; i++)
                    Console.WriteLine("{0} восстановит {1}", i + 1, this.MedicineKits[i].HpToAdd);
                Console.WriteLine("Введите номер аптечки, 0 для выхода");
                int index = Program.Parse(Console.ReadLine(), 0, this.MedicineKits.Count) - 1;
                if (index == -1)
                    return;
                this.MedicineKits[index].JoinKit(this);
                this.MedicineKits.RemoveAt(index);
            }
        }
    }
}
