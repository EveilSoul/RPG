using System.Collections.Generic;

namespace TheGame
{
    class EnemyMix : Enemy
    {
        public static List<Enemy> CreateEnemyMix(int PlayerLevel)
        {
            var enemy = new List<Enemy>();
            int count = Program.Random.Next(PlayerLevel, 4 + PlayerLevel);

            for (int i = 0; i < count; i++)
            {
                int rand = Program.Random.Next(1, 151 + PlayerLevel * PlayerLevel);

                if (rand >= 1 && rand <= 50) enemy.Add(new EnemyWolf(PlayerLevel));
                else if (rand > 50 && rand <= 100) enemy.Add(new EnemyGoblin(PlayerLevel));
                else if (rand > 100 && rand <= 150) enemy.Add(new EnemyBear(PlayerLevel));
                else enemy.Add(new EnemyDragon(PlayerLevel));
            }
            return enemy;
        }
    }
}