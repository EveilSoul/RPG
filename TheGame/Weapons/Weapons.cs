using System;
namespace TheGame
{
    /// <summary>
    /// Предок всего, чем можно длаться - оружие
    /// </summary>
    public class Weapons
    {
        // Название
        public string Name;
        // Максимальное здоровье
        public float MaxHealth;
        // Текущее здоровье
        public float CurrentHelth;
        // Сила атаки
        public int PowerAttack;
        // Мана, которую дает предмет
        public int Mana;
        // Цена оружия
        public int Cost;
        // Прочность оружия
        public float Strength;
        // Количество противников, которых можно атаковать за один раз
        public int CountImpact;
        // Минимальный уровень для владения и использования
        public int MinLevelToUse;
        // Описание
        public string Description;
        // Тип оружия
        public WeaponsType TypeOfWeapons;

        public enum WeaponsType
        {
            Sword,
            Bow,
            Spell
        }

        /// <summary>
        /// Описание оружия
        /// </summary>
        /// <param name="store"> Для магазина ли надо </param>
        /// <param name="cost"> Коэффицент стоимости </param>
        /// <returns> Массив с описанием </returns>
        public virtual string[] GetCharacteristics(bool store = false, float cost = 1)
        {
            return new[]
            {
                String.Format("\nИмя: {0}", this.Name),
                String.Format("Мощность атаки: {0}", this.PowerAttack),
                String.Format("Атака на {0} противников", this.CountImpact),
                (store ? String.Format("Стоимость: {0}", this.Cost * cost) : null),
                (store ? String.Format("Минимальный уровень для владения: {0}", this.MinLevelToUse) : null),
                (store ? String.Format("Прибавка к мане: {0}", this.Mana) : null)
            };
        }
    }
}