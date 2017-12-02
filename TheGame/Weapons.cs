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
    }
}