namespace TheGame
{
    public class Spell
    {
        public string Name;
        public int Mana;
        public int Level;
        public string Description;
        public TypeOfSpell Type;

        public int CountImpact;
        public int Damage;
        public float Accuracy;

        public int CountOfProtect;
        public float Protect;

        public enum TypeOfSpell
        {
            Defence,
            Attacking
        };
    }
}