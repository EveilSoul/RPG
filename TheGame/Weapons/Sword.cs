namespace TheGame
{
    /// <summary>
    /// Самый настоящий меч
    /// </summary>
    public class Sword : Weapons
    {
        public Sword()
        {
            // Присваиваем тип нашему оружию
            this.TypeOfWeapons = WeaponsType.Sword;
        }

        /// <summary>
        /// Удар мечом
        /// </summary>
        /// <returns> Нанесенный урон </returns>
        public int Attack()
        {
            if (this.CurrentHelth <= 0)
                return 0;
            this.CurrentHelth -= 1 - this.Strength / 2;
            return PowerAttack;
        }

        /// <summary>
        /// Чтение меча из файла
        /// </summary>
        /// <param name="file"> Путь к файлу с оружием </param>
        /// <returns> Готовый меч </returns>
        public static Sword GetWeaponsFromFile(string file)
        {
            var characteristics = System.IO.File.ReadAllLines(file);
            Sword sword = new Sword();

            sword.Name = characteristics[0];
            sword.MaxHealth = float.Parse(characteristics[1]);
            sword.PowerAttack = int.Parse(characteristics[2]);
            sword.Mana = int.Parse(characteristics[3]);
            sword.Cost = int.Parse(characteristics[4]);
            sword.Strength = float.Parse(characteristics[5]);
            sword.CountImpact = int.Parse(characteristics[6]);
            sword.MinLevelToUse = int.Parse(characteristics[7]);
            sword.CurrentHelth = sword.MaxHealth;

            for (int i = 8; i < characteristics.Length; i++)
                sword.Description += characteristics[i] + "\n";

            return sword;
        }
    }
}

