using System;
using System.Collections.Generic;

namespace TheGame
{
    class EnemyDarkKnight : Enemy
    {
        public EnemyDarkKnight(int level)
        {
            this.Name = "Темный воин";
            this.PowerAttack = 58 + level * level / 2;
            this.IsLive = true;
            this.Health = 148 + 2 * level * level;
            this.Accuracy = 0.7f;
            this.MoneyReward = 35 + 2 * level;
            this.SkillReward = GetSkill(30, 80, 35, level);
            this.Mimicry = false;
        }

        public static List<Enemy> CreateEnemyDarkKnight(int playerLevel)
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
            if (rand < 8)
                enemy.Health -= (int)Math.Ceiling(damageAtackEnemy * 0.65);
            else enemy.Health -= damageAtackEnemy;
        }
    }
}