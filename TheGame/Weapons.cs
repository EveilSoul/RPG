using System;
namespace TheGame
{
    public class Weapons
    {
        public string Name;
        public float Health;
        public int PowerAttack;
        public int Mana;
        public int Cost;
        public float Strength;
        public int CountImpact;
        public int MinLevelToUse;
        public string Description;
        public WeaponsType TypeOfWeapons;

        public enum WeaponsType
        {
            Sword,
            Bow,
            Spell
        }

        public virtual string[] GetCharacteristics(bool store = false)
        {
            return new[]
            {
                String.Format("\nИмя: {0}", this.Name),
                String.Format("Мощность атаки: {0}", this.PowerAttack),
                String.Format("Атака на {0} противников", this.CountImpact),
                (store ? String.Format("Стоимость: {0}", this.Cost) : null),
                (store ? String.Format("Минимальный уровень для владения: {0}", this.MinLevelToUse) : null),
                (store ? String.Format("Прибавка к мане: {0}", this.Mana) : null)
            };
        }
    }
}