using System;
using System.Linq;

namespace TheGame
{
    /// <summary>
    /// Комплект брони, состоит из отдельных частей и содержит обобщенные методы для работы
    /// </summary>
    class ArmorComplect
    {
        public Armor Head;
        public Armor Body;
        public Armor Arms;
        public Armor Leggs;
        public Armor Boots;

        public ArmorComplect()
        {
            Head = new Armor();
            Body = new Armor();
            Arms = new Armor();
            Leggs = new Armor();
            Boots = new Armor();
        }

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
        // Возвращает общую стоимость комплекта, умноженную на коэффицент cost
        public int GetCost(float cost = 1) =>
            (int)(cost * (this.Head.Cost + this.Body.Cost + this.Arms.Cost + this.Leggs.Cost + this.Boots.Cost));
        // Возвращает ману, которую прибавляет комплект
        public int GetMana() =>
            this.Head.Mana + this.Body.Mana + this.Arms.Mana + this.Leggs.Mana + this.Boots.Mana;
        // Возвращает реальную стоимость брони, учитывая ее повреждения
        public int GetRealCost(float cost = 1) =>
            (int)(cost * (this.Head.GetCost() + this.Body.GetCost() + this.Arms.GetCost() + this.Leggs.GetCost() + this.Boots.GetCost()));
        // Возвращает максимальное количество HP, которое можно восстановить
        public int GetHealthToAdd() =>
            this.Head.GetHealthToAdd() + this.Body.GetHealthToAdd() + this.Arms.GetHealthToAdd() + this.Leggs.GetHealthToAdd() + this.Boots.GetHealthToAdd();
        // Возвращает стоимость одного HP для поврежденного комплекта
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

        public string[] GetStatistic()
        {
            return new[]{
                "Целостность вашего комплекта брони:",
                String.Format("Шлем: {0}%", this.Head.GetStatistic() * 100),
                String.Format("Нагрудник: {0}%", this.Body.GetStatistic() * 100),
                String.Format("Наручи: {0}%", this.Arms.GetStatistic() * 100),
                String.Format("Поножи: {0}%", this.Leggs.GetStatistic() * 100),
                String.Format("Ботинки: {0}%", this.Boots.GetStatistic() * 100)
            };
        }
    }

}

