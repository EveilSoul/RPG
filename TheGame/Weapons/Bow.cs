namespace TheGame
{
    /// <summary>
    /// Лук, оружие
    /// </summary>
    public class Bow : Weapons
    {
        public Bow()
        {
            this.TypeOfWeapons = WeaponsType.Bow;
        }

        /// <summary>
        /// Создание экзампляра класса из файла
        /// </summary>
        /// <param name="file"> Путь к файлу </param>
        /// <returns> Заполненный экземпляр класса </returns>
        public static Bow GetBowFromFile(string file)
        {
            var characteristics = System.IO.File.ReadAllLines(file);
            Bow bow = new Bow();

            bow.Name = characteristics[0];
            bow.MaxHealth = int.Parse(characteristics[1]);
            bow.PowerAttack = int.Parse(characteristics[2]);
            bow.Mana = int.Parse(characteristics[3]);
            bow.Cost = int.Parse(characteristics[4]);
            bow.Strength = float.Parse(characteristics[5]);
            bow.CountImpact = 100;
            bow.MinLevelToUse = int.Parse(characteristics[6]);
            bow.CurrentHelth = bow.MaxHealth;

            for (int i = 7; i < characteristics.Length; i++)
                bow.Description += characteristics[i] + "\n";

            return bow;
        }

        /// <summary>
        /// Выстрел из лука
        /// </summary>
        /// <returns> Нанесенный урон </returns>
        public int Attack()
        {
            if (this.CurrentHelth <= 0) return 0;
            this.CurrentHelth -= 1 - this.Strength / 2;
            return this.PowerAttack + Program.Random.Next(-3, 4);
        }
    }
}