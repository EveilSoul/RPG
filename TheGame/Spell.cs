using System;
namespace TheGame
{
    public class Spell : Weapons
    {
        public int Damage;
        public float Accuracy;

        public Spell(string path)
        {
            this.TypeOfWeapons = WeaponsType.Spell;
            var lines = System.IO.File.ReadAllLines(path);
            this.Name = lines[0];
            this.Mana = int.Parse(lines[1]);
            this.MinLevelToUse = int.Parse(lines[2]);
            this.Cost = int.Parse(lines[3]);
            this.CountImpact = Program.Parse(lines[4]);
            this.Damage = Program.Parse(lines[5]);
            this.Accuracy = float.Parse(lines[6]);
            for (int i = 7; i < lines.Length; i++)
                this.Description += lines[i] + "\n";
        }

        public override string[] GetCharacteristics(bool store = false)
        {
            return new[]
            {
                String.Format("\nИмя: {0}", this.Name),
                String.Format("Мощность заклинания: {0}", this.Damage),
                String.Format("Вы сможете поразить {0} противников", this.CountImpact),
                String.Format("Использование заклинания стоит {0} маны", this.Mana),
                (store ? String.Format("Стоимость изучения: {0}", this.Cost) : null),
                (store ? String.Format("Минимальный уровень: {0}", this.MinLevelToUse) : null),
            };
        }

        public int[] JoinSpell(int countEnemy, params int[] nums)
        {
            int[] result = new int[countEnemy];
            if (this.CountImpact >= countEnemy)
            {
                for (int i = 0; i < countEnemy; i++)
                    result[i] = GetDamage();
            }
            else
            {
                foreach (var num in nums)
                    if (num <= countEnemy)
                        result[num] += GetDamage();
            }
            return result;
        }

        private int GetDamage() =>
            (int)((Program.Random.Next((int)(this.Accuracy * 100), 100) / 100d) * this.Damage) + 1;
    }
}