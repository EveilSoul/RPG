namespace TheGame
{
    public class Spell
    {
        public string Name;
        public int Mana;
        public int Level;
        public int Cost;
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

        public Spell(string path)
        {
            var lines = System.IO.File.ReadAllLines(path);
            this.Name = lines[0];
            this.Mana = int.Parse(lines[1]);
            this.Level = int.Parse(lines[2]);
            this.Cost = int.Parse(lines[3]);
            this.Type = (TypeOfSpell)int.Parse(lines[4]);
            int k = 5;
            switch (this.Type)
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

        public int[] JoinSpell(int countEnemy, params int[] nums)
        {
            var random = new System.Random();
            int[] result = new int[countEnemy];

            switch (this.Type)
            {
                case TypeOfSpell.Attacking:

                    if (this.CountImpact >= countEnemy)
                    {
                        for (int i = 0; i < countEnemy; i++)
                        {
                            result[i] = GetDamage(random);
                        }
                    }
                    else
                    {
                        foreach (var num in nums)
                        {
                            if (num <= countEnemy)
                                result[num - 1] += GetDamage(random);
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

        private int GetDamage(System.Random random)
        {
            return (int)((random.Next((int)(this.Accuracy * 100), 100) / 100d) * this.Damage) + 1;
        }
    }
}