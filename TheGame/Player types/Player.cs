using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Player
    {
        // Сколько очков необходимо набрать для перехода на следующий уровень
        public int NextLevelBorder;
        // Имя персонажа
        public string Name;
        // Текущее здоровье
        public int CurrentHealth;
        // Максимальное здоровье
        public int MaxHealth;
        // Сила атаки без оружия
        public int PowerAttack;
        // Максимальная мана
        public int MaxMana;
        // Текущая мана - используется в заклинаниях
        public int CurrentMana;
        // Баланс
        public int Money;
        // Опыт сражений
        public int BattleSkill;
        // Тип персонажа
        public PlayerType Type;
        //Уровень, изначально 1
        public int Level;
        // Навык обращения с мечом
        public float SwordSkill;
        // Навык стрельбы из лука
        public float BowSkill;
        // Навык владения магией
        public float MagicSkill;
        // Уровень магии
        public int MagicLevel;
        // Необходима для совершения хода
        public bool IsLive;

        public enum PlayerType
        {
            Warrior,
            Ranger,
            Wizard
        };

        // Аптечки хранятся здесь
        public List<MainGameStructures.MedicineKit> MedicineKits;
        // Позиция персонажа в виде (x,y)
        public MainGameStructures.Position Position;
        // Комплект брони
        public ArmorComplect Armor;
        // Все мечи
        public List<Sword> Swords;
        // Лук (при наличии)
        public Bow Bow;
        // Все заклинания
        public List<Spell> Spells;
        // Защитный предмет
        public Protection Protection;
        // Задания
        public List<Tuple<MainGameStructures.Position, Task>> Tasks;

        // Базовые характеристики для любого типа
        public Player(string name, MainGameStructures.Position position)
        {
            this.Name = name;
            this.Position = position;
            this.Level = 1;
            this.MagicLevel = 1;
            this.Money = 200;
            this.NextLevelBorder = 150;

            this.Swords = new List<Sword>();
            this.Armor = new ArmorComplect();
            this.Spells = new List<Spell>();
            this.Tasks = new List<Tuple<MainGameStructures.Position, Task>>();

            this.MedicineKits = new List<MainGameStructures.MedicineKit>()
            {
                new MainGameStructures.MedicineKit{ HpToAdd = 50 },
                new MainGameStructures.MedicineKit{ HpToAdd = 50 },
                new MainGameStructures.MedicineKit{ HpToAdd = 50 },
                new MainGameStructures.MedicineKit{ HpToAdd = 50 },
                new MainGameStructures.MedicineKit{ HpToAdd = 50 },
                new MainGameStructures.MedicineKit{ HpToAdd = 150 }
            };
            this.IsLive = true;
        }

        /// <summary>
        /// Перемещение
        /// </summary>
        /// <param name="x"> По оси X </param>
        /// <param name="y"> По оси Y </param>
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

            //Item1-позиция монстров, Item2-лист монстров
            var enemy = Enemy.CreateEnemy(this.Level, this.Position);
            Enemy.TheLastEnemyPosition = enemy.Item1;
            if (Enemy.EnemyExist && !enemy.Item2[0].Mimicry) Window.DrowEnemy(enemy.Item1, this.Position, moveX, moveY);

            var treasure = new Treasure(this, enemy.Item1);
            Window.DrowTreasure(treasure.Position, this.Position, moveX, moveY);
            Treasure.TheLastTreasurePosition = treasure.Position;

            while (this.IsLive)
            {
                CheckAndCreateEnemy(ref enemy, moveX, moveY);
                City.CheckPlayer(this);
                CheckAndCreateTreasure(ref treasure, moveX, moveY, enemy.Item1);
                this.AddMana(this.Level);
                //если произошел выход за границу карты
                if (Math.Abs(moveX) == Window.MapSizeX / 2 || Math.Abs(moveY) == Window.MapSizeY / 2)
                {
                    DrowCityEnemyAndTreasure(city, treasure, enemy);
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
        /// Проверка на столкновение монстров и персонажа и начало боя при их столкновении
        /// </summary>
        /// <param name="enemy"> Монстры </param>
        /// <param name="moveX"> Сдвиг по оси Х относительно середины карты </param>
        /// <param name="moveY"> Сдвиг по оси У относительно середины карты </param>
        public void CheckAndCreateEnemy(ref Tuple<MainGameStructures.Position, List<Enemy>> enemy, int moveX, int moveY)
        {
            Enemy.CheckPlayer(enemy.Item2, this, enemy.Item1);
            if (Enemy.MayNewEnemy(Enemy.TheLastEnemyPosition, this.Position))
            {
                enemy = Enemy.CreateEnemy(this.Level, this.Position);
                if (!enemy.Item2[0].Mimicry)
                    Window.DrowEnemy(enemy.Item1, this.Position, moveX, moveY);

                Enemy.TheLastEnemyPosition = enemy.Item1;
            }
        }

        /// <summary>
        /// Проверка того, находится ли игрок на клетке с кладом и начало боя, если игрока заметили монстры
        /// </summary>
        /// <param name="treasure"> Клад </param>
        /// <param name="moveX"> Сдвиг по оси Х относительно середины карты </param>
        /// <param name="moveY"> Сдвиг по оси У относительно середины карты </param>
        /// <param name="enemyPosition"> Позиция монстра (чтобы избежать столкновения монстров и кладов) </param>
        public void CheckAndCreateTreasure(ref Treasure treasure, int moveX, int moveY, MainGameStructures.Position enemyPosition)
        {
            treasure.CkeckPlayer(this);
            if (treasure.MayNewTreasure(Treasure.TheLastTreasurePosition, this.Position))
            {
                treasure = new Treasure(this, enemyPosition);
                Window.DrowTreasure(treasure.Position, this.Position, moveX, moveY);
                Treasure.TheLastTreasurePosition = treasure.Position;
            }
        }

        /// <summary>
        /// Отрисовка на карте городов, монстров и кладов
        /// </summary>
        /// <param name="city"> Координаты городов </param>
        /// <param name="treasure"> Клад </param>
        /// <param name="enemy"> Монстры </param>
        public void DrowCityEnemyAndTreasure(List<MainGameStructures.Position> city, Treasure treasure,
            Tuple<MainGameStructures.Position, List<Enemy>> enemy)
        {
            if (Enemy.EnemyExist && !enemy.Item2[0].Mimicry)
                Window.DrowEnemy(enemy.Item1, this.Position);

            if (Treasure.TreasureExist)
                Window.DrowTreasure(treasure.Position, this.Position);

            city = City.IsSityNear(this.Position);
            Window.DrowCity(city, this.Position);
        }

        // Печатает основные игровые комбинации и подсказки
        public void PrintTips()
        {
            Console.WriteLine("Перемещание по стрелкам или WSAD\n" +
                "Города обозначаются #\n" +
                "Противники обозначены *\n" +
                "Клад обозначен $\n" +
                "F1 для активации аптечки\n" +
                "F2 для вывода состояния брони\n" +
                "F3 для вывода состояния защиты (при наличии)\n" +
                "F4 для вывода текущих заданий и статистики по ним\n" +
                "F5 для вывода статистики по одному из видов оружия\n" +
                "H для помощи");
            Console.ReadKey();
        }

        /// <summary>
        /// Обработка нажатий на клавиатуру
        /// </summary>
        /// <param name="ch"> Нажатая клавиша </param>
        /// <param name="moveX"> Сдвиг по оси X от центра </param>
        /// <param name="moveY"> Сдвиг по оси Y от центра </param>
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
                    PrintTips();
                    break;
                case ConsoleKey.F1:
                    // Использование аптечки
                    UseMedicineKit();
                    break;
                case ConsoleKey.F2:
                    // Вывод состояния брони
                    Window.PrintArray(this.Armor.GetStatistic());
                    Console.ReadKey();
                    break;
                case ConsoleKey.F3:
                    // Вывод состояния защиты
                    Window.PrintArray(this.Protection?.GetCharacteristics());
                    Console.ReadKey();
                    break;
                case ConsoleKey.T:
                case ConsoleKey.F4:
                    // Вывод статистики по заданиям
                    foreach (var t in Tasks)
                        Console.WriteLine(t.Item2.GetStatistic());
                    Console.ReadKey();
                    break;
                case ConsoleKey.F5:
                    // Вывод статистики по оружию
                    PrintWeaponsHealth(SelectType());
                    Console.ReadKey();
                    break;
            }
        }

        // Выводит изношенность определенного вида оружия
        private void PrintWeaponsHealth(Weapons.WeaponsType type)
        {
            if (type == Weapons.WeaponsType.Spell)
                Console.WriteLine("Заклинания не изнашиваются со временем");
            if (type == Weapons.WeaponsType.Bow)
                PrintHPofOneThing(this.Bow.Name, this.Bow.MaxHealth, this.Bow.CurrentHelth);
            if (type == Weapons.WeaponsType.Sword)
                foreach (var sword in this.Swords)
                    PrintHPofOneThing(sword.Name, sword.MaxHealth, sword.CurrentHelth);
        }

        // Печатает изношенность ровно одной вещи
        private void PrintHPofOneThing(string name, float maxHP, float currentHP) =>
                Console.WriteLine("{0}: целостность {1:0.0}%", name, (double)currentHP / maxHP * 100);

        /// <summary>
        /// Метод для супер-атаки
        /// </summary>
        /// <param name="enemyCount"> Количество врагов </param>
        /// <returns> Массив с уроном для врагов </returns>
        public virtual int[] SuperAttack(int enemyCount) =>
            new int[enemyCount];

        /// <summary>
        /// Нанесение урона персонажу
        /// </summary>
        /// <param name="damage"> Количество урона </param>
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
        /// <param name="mana"> Сколько маны нужно потратить </param>
        /// <returns> Есть ли у персонажа нужное количество маны </returns>
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
        /// <param name="countEnemy"> Количество противников </param>
        /// <param name="weapons"> Тип оружия </param>
        /// <param name="bow"> Лук, при наличии </param>
        /// <param name="spell"> Заклинание, при наличии </param>
        /// <param name="sword"> Меч, при наличии </param>
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
        /// <param name="countEmemy"> Количество противников </param>
        /// <param name="spell"> Выбранное заклинание </param>
        /// <param name="nums"> Индексы противников, которых надо атаковать </param>
        /// <returns> Массив с уроном для противников </returns>
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
        /// <param name="countEnemy"> Количество противников </param>
        /// <param name="sword"> Меч, которым совершаем атаку </param>
        /// <param name="number"> Индексы противников для атаки </param>
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
        /// <param name="countEnemy"> Количество противников </param>
        /// <param name="bow"> Лук, которым атакуем </param>
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
        /// <param name="count"> Количество монет, которые нужно взять </param>
        /// <returns> Возвращаем, есть ли у игрока нужные деньги </returns>
        public bool HaveMoney(int count)
        {
            this.Money -= count;
            if (Money >= 0)
                return true;
            this.Money += count;
            return false;
        }

        // Добавряем деньги игроку
        public void AddMoney(int count) => this.Money += count;

        // Надеваем на игрока комплект брони
        public void AddArmor(ArmorComplect armor)
        {
            this.MaxMana -= this.Armor.GetMana();
            this.Armor = armor;
            this.MaxMana += armor.GetMana();
            this.CurrentMana = this.MaxMana;
        }

        // Добавляем меч в инвентарь
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
        /// <param name="spell"> Собственно, заклинание </param>
        /// <param name="cost"> Коэффицент стоимости </param>
        /// <returns> Успешность изучения </returns>
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

        // Возвращает массив из строк - характеристики всего оружия определенного типа
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

        // Выбираем тип оружия и возвращаем его
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

        // Выбираем атаку или защиту, если у игрока есть защита
        public int SelectAtionInBattle()
        {
            if (this.Protection == null) return 1;
            Console.WriteLine("Вы можете: ");
            Console.WriteLine("1. Атаковать");
            Console.WriteLine("2. Использовать защиту");
            Console.WriteLine("3. Одновременно атаковать и защищаться\n(удар и защита будут хуже)");
            return Program.Parse(Console.ReadLine(), 1, 3);
        }

        // Возвращаем меч по индексу
        public Sword GetSword(int index)
        {
            if (index >= 0 && index < this.Swords.Count)
                return this.Swords[index];
            return new Sword();
        }

        // Возвращаем заклинание по индексу
        public Spell GetSpell(int index)
        {
            if (index >= 0 && index < this.Spells.Count)
                return this.Spells[index];
            return null;
        }

        // Увеличиваем CurrentMana на count
        public void AddMana(int count)
        {
            this.CurrentMana += count;
            if (this.CurrentMana > this.MaxMana)
                this.CurrentMana = this.MaxMana;
        }

        // Восстанавливаем CurrentHealth на заданное число единиц
        public void AddHP(int count)
        {
            this.CurrentHealth += count;
            if (this.CurrentHealth > this.MaxHealth)
                this.CurrentHealth = this.MaxHealth;
        }

        // Используем одну из аптечек в инвентаре
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

        /// <summary>
        /// Используем защиту
        /// </summary>
        /// <param name="damage"> Количество урона </param>
        /// <param name="attack"> Атакует ли игрок параллельно </param>
        public void UseProtection(int damage, bool attack = false)
        {
            if (this.Protection == null)
            {
                Console.WriteLine("У вас нет ни одного средства защиты");
                Console.ReadKey();
                return;
            }
            if (this.HaveMana(this.Protection.ApplicationCost))
            {
                // Определяем процент защиты при атаке
                float combine = (float)Math.Abs(Math.Sin(this.Level * Math.Log10(this.Level))) / 1.65f + 0.2f;
                damage = this.Protection.ApplyProtection(damage, attack ? combine : 1);
                this.ApplyDamage(damage);
            }
        }

        // Проверяем, цело ли оружие у игрока
        public void CheckWeapons()
        {
            for (int i = 0; i < this.Swords.Count; i++)
            {
                if (this.Swords[i].CurrentHelth <= 0)
                {
                    BreakWeapons(this.Swords[i].Name);
                    DeleteMana(this.Swords[i].Mana);
                    this.Swords.RemoveAt(i--);
                }
            }
            if (this.Protection?.CurrentHealth <= 0)
            {
                BreakWeapons(this.Protection.Name);
                DeleteMana(this.Protection.ManaToAdd);
                this.Protection = null;
            }
            if (this.Bow?.CurrentHelth <= 0)
            {
                BreakWeapons(this.Bow.Name);
                DeleteMana(this.Bow.Mana);
                this.Bow = null;
            }
        }

        // Удаляем часть маны
        private void DeleteMana(int count)
        {
            this.MaxMana -= count;
            this.CurrentMana -= count;
            if (this.CurrentMana < 0)
                this.CurrentMana = 0;
        }

        // Сообщаем о сломанном оружие
        private void BreakWeapons(string name)
        {
            Console.WriteLine("Сломалось: {0}", name);
            Console.ReadKey();
        }

        // Добавляем в инвентарь защиту
        public void TryOnProtection(Protection protection)
        {
            this.MaxHealth += protection.ManaToAdd;
            this.Protection = protection;
        }
    }
}
