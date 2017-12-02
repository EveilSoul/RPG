namespace TheGame
{
    public class Bow : Weapons
    {
        public Bow()
        {
            this.TypeOfWeapons = WeaponsType.Bow;
        }

        public static Bow GetBowFromFile(string file)
        {
            var characteristics = System.IO.File.ReadAllLines(file);
            Bow weapons = new Bow();

            weapons.Name = characteristics[0];
            weapons.Health = int.Parse(characteristics[1]);
            weapons.PowerAttack = int.Parse(characteristics[2]);
            weapons.Mana = int.Parse(characteristics[3]);
            weapons.Cost = int.Parse(characteristics[4]);
            weapons.Strength = float.Parse(characteristics[5]);
            weapons.CountImpact = 100;
            weapons.MinLevelToUse = int.Parse(characteristics[6]);

            for (int i = 7; i < characteristics.Length; i++)
                weapons.Description += characteristics[i] + "\n";

            return weapons;
        }

        public int Attack()
        {
            return 0;
        }
    }
}