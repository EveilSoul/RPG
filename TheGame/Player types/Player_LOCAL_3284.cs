using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public class Player
    {
        //Сколько очков необходимо набрать для перехода на следующий уровень
        public int NextLevelBorder;
        //Имя персонажа
        public string Name;
        //Текущее здоровье
        public int CurrentHealth;
        //Максимальное здоровье
        public int MaxHealth;
        //Сила атаки без оружия
        public int PowerAttack;
        //Максимальная мана
        public int MaxMana;
        //Текущая мана - используется в заклинаниях
        public int CurrentMana;
        //Баланс
        public int Money;
        //Опыт сражений
        public int BattleSkill;
        //Тип персонажа
        public PlayerType Type;
        //Уровень, изначально 1
        public int Level;
        //Навык обращения с мечом
        public float SwordSkill;
        //Навык стрельбы из лука
        public float BowSkill;
        //Навык владения магией
        public float MagicSkill;
        //Уровень магии
        public int MagicLevel;
        //Необходима для совершения хода
        public bool IsLive;

        public enum PlayerType
        {
            Warrior,
            Ranger,
            Wizard
        };

        //Аптечки хранятся здесь
        public List<ObjectStructures.MedicineKit> MedicineKits;
        //Позиция персонажа в виде (x,y)
        public ObjectStructures.Position Position;
        //Комплект брони
        public ObjectStructures.ArmorComplect Armor;
        //Все мечи
        public List<Sword> Swords;
        //Лук (при наличии)
        public Bow Bow;
        //Все заклинания
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
            //устанавливаем границу для следующего уровня
            this.NextLevelBorder += 150 * (int)Math.Sqrt(this.Level);
            //вычисляем временную величину увеличения
            int increase = (int)Math.Sqrt(this.BattleSkill) + 60 / (this.BattleSkill / 10);
            //Здоровье у всех персонажей увеличивается равномерно,
            //а остальные характеристики в зависимости от типа
            this.MaxHealth += increase;

            switch (this.Type)
            {
                case PlayerType.Ranger:
                    this.MaxMana += increase;
                    this.PowerAttack += (2 * increase) / 3;
                    this.BowSkill += 0.001f * this.Level;
                    this.SwordSkill += 0.003f * this.Level;
                    this.MagicSkill += 0.003f * this.Level;
                    if (this.Level % 3 == 0)
                        this.MagicLevel++;
                    break;
                case PlayerType.Warrior:
                    this.MaxMana += increase / 2;
                    this.PowerAttack += increase;
                    this.SwordSkill += 0.001f * this.Level;
                    this.MagicSkill += 0.003f * this.Level;
                    this.BowSkill += 0.005f * this.Level;
                    if (this.Level % 4 == 0)
                        this.MagicLevel++;
                    break;
                case PlayerType.Wizard:
                    this.MaxMana += 2 * increase;
                    this.PowerAttack += increase / 2;
                    this.MagicSkill += 0.001f * this.Level;
                    this.SwordSkill += 0.003f * this.Level;
                    this.BowSkill += 0.005f * this.Level;
                    if (this.Level % 2 == 0)
                        this.MagicLevel++;
                    break;
            }
            //устанавливаем текущее здоровье и ману максимальными
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
        /// <returns>Массив с уроном для врагов</returns>
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
        /// <returns>Есть ли у персонажа нужное количество маны</returns>
        private bool HaveMana(int mana)
        {
            this.CurrentMana -= mana;

            if (this.CurrentMana >= 0)
                return true;
            else this.CurrentMana += mana;

            return false;
        }

        /// <summary>
        /// Нанесение атаки врагу, в зависимости от выбранного оружия увеличиваем навык владения
        /// </summary>
        /// <param name="countEnemy">количество противников</param>
        /// <param name="weapons">тип оружия</param>
        /// <param name="bow">лук, при наличии</param>
        /// <param name="spell">заклинание, при наличии</param>
        /// <param name="sword">меч, при наличии</param>
        /// <param name="nums">индексы противников, которых надо атаковать</param>
        /// <returns>Массив с уроном для противников</returns>
        public int[] Attack(int countEnemy, Weapons.WeaponsType weapons, 
            Bow bow = null, Spell spell = null, Sword sword = null, params int[] nums)
        {
            switch (weapons)
            {
                case Weapons.WeaponsType.Bow:
                    this.BowSkill += 0.0001f;
                    return SimpleBowAttack(countEnemy, bow);
                case Weapons.WeaponsType.Spell:
                    this.MagicSkill += 0.0001f;
                    return SimpleSpellAttack(countEnemy, spell, nums);
                case Weapons.WeaponsType.Sword:
                    this.SwordSkill += 0.0001f;
                    return SimpleSwordAttack(countEnemy, sword, nums);
            }
            return new int[0];
        }

        /// <summary>
        /// Простая атака магией
        /// </summary>
        /// <param name="countEmemy">Количество противников</param>
        /// <param name="spell">Выбранное заклинание</param>
        /// <param name="nums">Индексы противников, которых надо атаковать</param>
        /// <returns>Массив с уроном для противников</returns>
        public int[] SimpleSpellAttack(int countEmemy, Spell spell, params int[] nums)
        {
            int[] res = spell.JoinSpell(countEmemy, nums);
            if (HaveMana(spell.Mana))
                for (int i = 0; i < res.Length; i++)
                {
                    if (Program.Random.NextDouble() > this.MagicSkill)
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

        /// <summary>
        /// Простая атака мечом
        /// </summary>
        /// <param name="countEnemy">количество противников</param>
        /// <param name="sword">меч, которым совершаем атаку</param>
        /// <param name="number">индексы противников для атаки</param>
        /// <returns>массив с уроном для врагов</returns>
        public int[] SimpleSwordAttack(int countEnemy, Sword sword, params int[] number)
        {
            int[] result = new int[countEnemy];
            int damage = sword.Attack();

            for (int i = 0; i < sword.CountImpact; i++)
            {
                double luck = Program.Random.NextDouble();
                if (i == number.Length)
                    break;
                if (luck <= this.SwordSkill)
                    result[number[i]] = damage;
                if (luck % 5 != 0)
                    result[number[i]] += this.PowerAttack;
                result[number[i]] += Program.Random.Next(-5, 6);
                if (result[number[i]] < 0) result[number[i]] = 0;
            }
            return result;
        }
        
        /// <summary>
        /// Простая атака луком
        /// </summary>
        /// <param name="countEnemy">Количество противников</param>
        /// <param name="bow">Лук, которым атакуем</param>
        /// <returns>Массив с уроном для противников</returns>
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

        /// <summary>
        /// Забираем у игрока деньги, если они есть
        /// </summary>
        /// <param name="count">Количество монет, которые нужно взять</param>
        /// <returns>Возвращаем, есть ли у игрока нужные деньги</returns>
        public bool GiveMoney(int count)
        {
            this.Money -= count;
            if (Money >= 0)
                return true;
            this.Money += count;
            return false;
        }

        //Добавряем деньги игроку
        public void AddMoney(int count) => this.Money += count;

        //Надеваем на игрока комплект брони
        public void AddArmor(ObjectStructures.ArmorComplect armor)
        {
            this.MaxMana -= this.Armor.GetMana();
            this.Armor = armor;
            this.MaxMana += armor.GetMana();
            this.CurrentMana = this.MaxMana;
        }

        //Добавляем меч в инвентарь
        public void AddSword(Sword sword)
        {
            if (sword.MinLevelToUse <= this.Level)
            {
                this.Swords.Add(sword);
                this.MaxMana += sword.Mana;
            }
        }

        /// <summary>
        /// Пробуем изучить определенное заклинание
        /// </summary>
        /// <param name="spell">Собственно, заклинание</param>
        /// <param name="cost">Коэффицент стоимости</param>
        /// <returns>Успешность изучения</returns>
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

        //Возвращает массив из строк - характеристики всего оружия определенного типа
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

        //Выбираем тип заклинания и возвращаем его
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

        //Возвращаем меч по индексу
        public Sword GetSword(int index)
        {
            if (index >= 0 && index < this.Swords.Count)
                return this.Swords[index];
            return new Sword();
        }

        //Возвращаем заклинание по индексу
        public Spell GetSpell(int index)
        {
            if (index >= 0 && index < this.Spells.Count)
                return this.Spells[index];
            return null;
        }

        //Восстанавливаем CurrentHealth на заданное число единиц
        public void AddHP(int count)
        {
            this.CurrentHealth += count;
            if (this.CurrentHealth > this.MaxHealth)
                this.CurrentHealth = this.MaxHealth;
        }

        //Используем одну из аптечек в инвентаре
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
