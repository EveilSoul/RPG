using System;
using System.IO;

namespace TheGame
{
    /// <summary>
    /// Защитные предметы -щиты, магические барьеры
    /// </summary>
    class Protection
    {
        // Максимальное здоровье
        public int MaxHealth;
        // Текущее здоровье
        public int CurrentHealth;

        // Название
        public string Name;
        // Мана, которую добавляет предмет
        public int ManaToAdd;

        // Стоимость применения
        public int ApplicationCost;
        // Цена при покупке
        public int Cost;

        // Насколько хорошо предмет защищает
        public float ProtectionScale;
        // Насколько велика вероятность защиты
        public float ProtectionLuck;
        // Насколько прочен предмет
        public float Strenght;

        // Чтение предмета из файла
        public Protection(string path)
        {
            var tempArray = File.ReadAllLines(Program.Path + @"\TextFiles\Protection" + path);
            this.Name = tempArray[0];
            this.MaxHealth = Program.Parse(tempArray[1], 0);
            this.Cost = Program.Parse(tempArray[2], 0);
            this.ManaToAdd = Program.Parse(tempArray[3], 0);
            this.ApplicationCost = Program.Parse(tempArray[4], 0);
            this.Strenght = float.Parse(tempArray[5]);
            this.ProtectionScale = float.Parse(tempArray[6]);
            this.ProtectionLuck = float.Parse(tempArray[7]);
            this.CurrentHealth = this.MaxHealth;
        }

        /// <summary>
        /// Применение защиты
        /// </summary>
        /// <param name="damage"> Наносимы урон </param>
        /// <param name="combine"> Комбинируется ли защита с атакой</param>
        /// <returns> Не поглощенный предметом урон </returns>
        public int ApplyProtection(int damage, float combine = 1)
        {
            if (this.CurrentHealth > 0 && Program.Random.NextDouble() <= this.ProtectionLuck)
            {
                damage = (int)((1 - this.ProtectionScale * combine) * damage);
                this.CurrentHealth -= (int)((1 - this.Strenght) / 5 * damage);
            }
            return damage;
        }

        // Получение характеристик оружия
        public string[] GetCharacteristics()
        {
            return new[]
            {
                this.Name,
                String.Format("Максимальное здоровье: {0}", this.MaxHealth),
                String.Format("Изношенность: {0}%", (1 - (float)this.CurrentHealth /  this.MaxHealth) * 100),
                String.Format("Добавляет {0} маны", this.ManaToAdd),
                String.Format("Стоит {0} монет", this.Cost),
                String.Format("Применение стоит {0} маны", this.ApplicationCost),
                String.Format("Устойчивость {0:0.00}%", this.Strenght * 100),
                String.Format("Вероятность защиты: {0:0.00}%", this.ProtectionLuck * 100),
                String.Format("Защищает на {0:0.00}%", this.ProtectionScale * 100)
            };
        }
    }
}