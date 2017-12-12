using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    /// <summary>
    /// Содержит в себе основные структуры, использующиеся в игре
    /// </summary>
    public class ObjectStructures
    {
        /// <summary>
        /// Позиция объекта на карте
        /// </summary>
        public struct Position
        {
            public int X;
            public int Y;
        }

        /// <summary>
        /// Аптечка, принадлежит игроку
        /// </summary>
        public struct MedicineKit
        {
            public int HpToAdd;

            /// <summary>
            /// Применяет аптечку
            /// </summary>
            /// <param name="player">Игрок, которому прибавляется HP</param>
            public void JoinKit(Player player) =>
                player.AddHP(HpToAdd);
        }

        /// <summary>
        /// Комплект брони, состоит из отдельных частей и содержит обобщенные методы для работы
        /// </summary>
        public struct ArmorComplect
        {
            public Armor Head;
            public Armor Body;
            public Armor Arms;
            public Armor Leggs;
            public Armor Boots;
            /// <summary>
            /// Уменьшает общий нанесенный урон за счет брони
            /// </summary>
            /// <param name="damage">Изначальное количество урона</param>
            /// <param name="num">Часть брони, на которую выпал удар</param>
            /// <returns>Итоговый урон</returns>
            public int Protect(int damage, int num)
            {
                switch (num)
                {
                    case 0:
                        damage = this.Head.Protect(damage);
                        break;
                    case 1:
                        damage = this.Body.Protect(damage);
                        break;
                    case 2:
                        damage = this.Arms.Protect(damage);
                        break;
                    case 3:
                        damage = this.Leggs.Protect(damage);
                        break;
                    case 4:
                        damage = this.Boots.Protect(damage);
                        break;
                }
                return damage;
            }
            /// <summary>
            /// Получает характеристики комплекта брони
            /// </summary>
            /// <returns>Характеристики в виде массива</returns>
            public string[] GetCharacteristics()
            {
                var res = this.Head.GetCharacteristics().ToList<string>();
                res.AddRange(this.Body.GetCharacteristics());
                res.AddRange(this.Arms.GetCharacteristics());
                res.AddRange(this.Leggs.GetCharacteristics());
                res.AddRange(this.Boots.GetCharacteristics());
                return res.ToArray<string>();
            }
            //Возвращает общую стоимость комплекта, умноженную на коэффицент cost
            public int GetCost(float cost = 1) =>
                (int)(cost * (this.Head.Cost + this.Body.Cost + this.Arms.Cost + this.Leggs.Cost + this.Boots.Cost));
            //Возвращает ману, которую прибавляет комплект
            public int GetMana() =>
                this.Head.Mana + this.Body.Mana + this.Arms.Mana + this.Leggs.Mana + this.Boots.Mana;
            //Возвращает реальную стоимость брони, учитывая ее повреждения
            public int GetRealCost(float cost = 1) =>
                (int)(cost * (this.Head.GetCost() + this.Body.GetCost() + this.Arms.GetCost() + this.Leggs.GetCost() + this.Boots.GetCost()));
            //Возвращает максимальное количество HP, которое можно восстановить
            public int GetHealthToAdd() =>
                this.Head.GetHealthToAdd() + this.Body.GetHealthToAdd() + this.Arms.GetHealthToAdd() + this.Leggs.GetHealthToAdd() + this.Boots.GetHealthToAdd();
            //Возвращает стоимость одного HP для поврежденного комплекта
            public int GetOnHPCost() =>
                this.Body.MaxHealth / this.Body.Cost;
            /// <summary>
            /// Восттанавливает броню на заданное число HP в определенном приоритете
            /// </summary>
            /// <param name="health">Количество HP для восстановления</param>
            public void Repair(int health)
            {
                var hp = this.Body.Repair(health);
                hp = this.Leggs.Repair(hp);
                hp = this.Head.Repair(hp);
                hp = this.Arms.Repair(hp);
                hp = this.Boots.Repair(hp);
            }
        }

        /// <summary>
        /// Отдельный элемент брони
        /// </summary>
        public struct Armor
        {
            //Максимальное "здоровье" брони
            public int MaxHealth;
            //Название
            public string Name;
            //Текущее "здоровье"
            public int Health;
            //Стоимость
            public int Cost;
            //Устойчивость против повреждений
            public float Strength;
            //Вероятность полного поглощения урона
            public float Luck;
            //Мана, прибавляется игроку
            public int Mana;
            //Описание брони
            public string Description;
            
            //Возвращает реальную стоимость брони с учетом повреждения
            public int GetCost() =>
                this.Cost * this.Health / (this.MaxHealth != 0 ? this.MaxHealth : 1);

            //Возвращает количество HP, доступных для восстановления
            public int GetHealthToAdd() => this.MaxHealth - this.Health;

            /// <summary>
            /// Чинит элемент брони
            /// </summary>
            /// <param name="hp">Величина HP для восстановления</param>
            /// <returns>Остаток HP после восстановления</returns>
            public int Repair(int hp)
            {
                this.Health += hp;
                if (this.Health > this.MaxHealth)
                {
                    hp = this.Health - this.MaxHealth;
                    this.Health = this.MaxHealth;
                    return hp;
                }
                return 0;
            }

            /// <summary>
            /// Поглощает урон
            /// </summary>
            /// <param name="damage">Урон для поглощения</param>
            /// <returns>Урон, который все же получает игрок</returns>
            public int Protect(int damage)
            {
                if (this.Health > 0 && Program.Random.NextDouble() <= this.Luck)
                    return 0;
                damage = (int)(damage * (1 - this.Strength));
                this.Health -= (this.Strength == 0) ? 0 : damage / 5;
                if (this.Health <= 0)
                {
                    this.Strength = 0;
                    this.Health = 0;
                }
                return (int)damage;
            }

            //Возвращает массив строк с характеристиками брони
            public string[] GetCharacteristics()
            {
                return new string[]{ String.Format("Название: {0}", this.Name),
                    String.Format("Количество жизней: {0}", this.Health),
                    String.Format("Стоимость: {0}", this.Cost),
                    String.Format("Прочность: {0}", this.Strength),
                    String.Format("Шанс полной защиты: {0}", this.Luck),
                    this.Description + '\n'};
            }
        }

        /// <summary>
        /// Получает комплект брони из файла
        /// </summary>
        /// <param name="file">Полный путь к файлу</param>
        /// <returns>Возвращает единицу брони</returns>
        public static Armor GetArmorFromFile(string file)
        {
            var characteristics = System.IO.File.ReadAllLines(file);
            Armor armor = new Armor();

            armor.Name = characteristics[0];
            armor.Health = int.Parse(characteristics[1]);
            armor.Cost = int.Parse(characteristics[2]);
            armor.Strength = float.Parse(characteristics[3]);
            armor.Description = characteristics[4];
            armor.Luck = float.Parse(characteristics[5]);
            if (characteristics.Length != 6)
                armor.Mana = int.Parse(characteristics[6]);

            armor.MaxHealth = armor.Health;
            return armor;
        }

    }
}

