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

        public virtual string[] GetCharacteristics()
        {
            return new[]
            {
                String.Format("\nИмя: {0}", this.Name),
                String.Format("\r\rМощность атаки: {0}", this.PowerAttack),
                String.Format("Вы сможете поразить не более, чем {0} противников", this.CountImpact),
                String.Format("Описание: {0}",this.Description)
            };
        }
    }
}