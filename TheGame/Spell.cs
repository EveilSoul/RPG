using System;
namespace TheGame
{
    public class Spell : Weapons
    {
        public TypeOfSpell SpellType;
        public int Damage;
        public float Accuracy;

        public int CountOfProtect;
        public float Protect;

        public enum TypeOfSpell
        {
            Defence,
            Attacking
        };

        public Spell(string path)
        {
            this.TypeOfWeapons = WeaponsType.Spell;
            var lines = System.IO.File.ReadAllLines(path);
            this.Name = lines[0];
            this.Mana = int.Parse(lines[1]);
            this.MinLevelToUse = int.Parse(lines[2]);
            this.Cost = int.Parse(lines[3]);
            this.SpellType = (TypeOfSpell)int.Parse(lines[4]);
            int k = 5;
            switch (this.SpellType)
            {
                case TypeOfSpell.Attacking:
                    this.CountImpact = int.Parse(lines[k++]);
                    this.Damage = int.Parse(lines[k++]);
                    this.Accuracy = float.Parse(lines[k++]);
                    break;
                case TypeOfSpell.Defence:
                    this.CountOfProtect = int.Parse(lines[k++]);
                    this.Protect = int.Parse(lines[k++]);
                    break;
            }
            for (int i = k; i < lines.Length; i++)
                this.Description += lines[i] + "\n";
        }

        public override string[] GetCharacteristics()
        {
            return new[]
            {
                String.Format("\nИмя: {0}", this.Name),
                String.Format("Мощность заклинания: {0}", this.Damage),
                String.Format("Вы сможете поразить не более, чем {0} противников", this.CountImpact),
                String.Format("Использование заклинания будет стоить {0} маны", this.Mana),
                String.Format("Описание: {0}",this.Description)
            };
        }

        public int[] JoinSpell(int countEnemy, params int[] nums)
        {
            int[] result = new int[countEnemy];

            switch (this.SpellType)
            {
                case TypeOfSpell.Attacking:

                    if (this.CountImpact >= countEnemy)
                    {
                        for (int i = 0; i < countEnemy; i++)
                        {
                            result[i] = GetDamage();
                        }
                    }
                    else
                    {
                        foreach (var num in nums)
                        {
                            if (num <= countEnemy)
                                result[num] += GetDamage();
                        }
                    }
                    break;

                case TypeOfSpell.Defence:

                    for (int i = 0; i<countEnemy; i++)
                    {
                        result[i] = (int)(nums[i] * (1 - this.Protect));
                    }
                    break;
            }
            return result;
        }

        private int GetDamage()
        {
            return (int)((Program.Random.Next((int)(this.Accuracy * 100), 100) / 100d) * this.Damage) + 1;
        }
    }
}