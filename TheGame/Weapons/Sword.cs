namespace TheGame
{
    public class Sword : Weapons
    {
        
        public Sword()
        {
            this.TypeOfWeapons = WeaponsType.Sword;
        }

        public int Attack()
        {
            if (this.Health > 0)
            {
                this.Health -= (1 - this.Strength) / 2;
            }
            else Die();
            return PowerAttack;
        }

        private void Die()
        {
            this.Description = null;
            this.PowerAttack = 0;
        }

        public static Sword GetWeaponsFromFile(string file)
        {
            var characteristics = System.IO.File.ReadAllLines(file);
            Sword sword = new Sword();

            sword.Name = characteristics[0];
            sword.Health = int.Parse(characteristics[1]);
            sword.PowerAttack = int.Parse(characteristics[2]);
            sword.Mana = int.Parse(characteristics[3]);
            sword.Cost = int.Parse(characteristics[4]);
            sword.Strength = float.Parse(characteristics[5]);
            sword.CountImpact = int.Parse(characteristics[6]);
            sword.MinLevelToUse = int.Parse(characteristics[7]);

            for (int i = 8; i < characteristics.Length; i++)
                sword.Description += characteristics[i] + "\n";

            return sword;
        }
    }
}

