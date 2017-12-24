using System;

namespace TheGame
{
    /// <summary>
    /// Отдельный элемент брони
    /// </summary>
    class Armor
    {
        // Максимальное "здоровье" брони
        public int MaxHealth;
        // Название
        public string Name;
        // Текущее "здоровье"
        public int Health;
        // Стоимость
        public int Cost;
        // Устойчивость против повреждений
        public float Strength;
        // Вероятность полного поглощения урона
        public float Luck;
        // Мана, прибавляется игроку
        public int Mana;
        // Описание брони
        public string Description;

        // Возвращает реальную стоимость брони с учетом повреждения
        public int GetCost() =>
            this.Cost * this.Health / (this.MaxHealth != 0 ? this.MaxHealth : 1);

        // Возвращает количество HP, доступных для восстановления
        public int GetHealthToAdd() => this.MaxHealth - this.Health;

        // Анализ изношенности брони
        public float GetStatistic()
        {
            if (this.MaxHealth != 0)
                return (float)this.Health / this.MaxHealth;
            else return 0;
        }

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

        // Возвращает массив строк с характеристиками брони
        public string[] GetCharacteristics()
        {
            return new string[]{ String.Format("Название: {0}", this.Name),
                String.Format("Количество жизней: {0}", this.Health),
                String.Format("Стоимость: {0}", this.Cost),
                String.Format("Прочность: {0}", this.Strength),
                String.Format("Шанс полной защиты: {0}", this.Luck),
                this.Description + '\n'};
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