using System;
using System.Collections.Generic;

namespace TheGame
{
    class EnemyGolem : Enemy
    {
        public EnemyGolem(int level)
        {
            this.Name = "Голем";
            this.PowerAttack = 108 + level * level / 2;
            this.IsLive = true;
            this.Health = 248 + 2 * level * level;
            this.Accuracy = 0.7f;
            this.MoneyReward = 100 + 2 * level;
            this.SkillReward = GetSkill(30, 80, 55, level);
            this.Mimicry = false;
            this.Type = EnemyType.Golem;
        }

        public static List<Enemy> CreateEnemyGolem(int playerLevel)
        {
            int level = Program.Random.Next(1, 101);

            if (level <= 70) return new List<Enemy> { new EnemyDarkKnight(playerLevel) };
            else if (level <= 80) return new List<Enemy> { new EnemyDarkKnight(playerLevel + 1) };
            else if (level <= 90) return new List<Enemy> { new EnemyDarkKnight(playerLevel - 1) };
            else if (level <= 95) return new List<Enemy> { new EnemyDarkKnight(playerLevel + 2) };
            else if (level <= 98) return new List<Enemy> { new EnemyDarkKnight(playerLevel - 2) };
            return new List<Enemy> { new EnemyDarkKnight(playerLevel + 3) };
        }

        public override void OnEnemyAtack(Enemy enemy, int damageAtackEnemy)
        {
            var rand = Program.Random.Next(0, 10);
            if (rand < 6)
                enemy.Health -= (int)Math.Ceiling(damageAtackEnemy * 0.75);
            else if (rand < 9)
                enemy.Health -= (int)Math.Ceiling(damageAtackEnemy * 0.9);
            else enemy.Health -= damageAtackEnemy;
        }
    }
}