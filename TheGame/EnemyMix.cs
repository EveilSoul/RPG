using System.Collections.Generic;

namespace TheGame
{
    class EnemyMix : Enemy
    {
        public static Enemy[] CreateEnemyMix(int PlayerLevel)
        {
            var enemyList = new List<Enemy>();
            int count = Program.Random.Next(PlayerLevel, 4 + PlayerLevel);

            for (int i = 0; i < count; i++)
            {
                int rand = Program.Random.Next(1, 151 + PlayerLevel * PlayerLevel * PlayerLevel);

                if (rand >= 1 && rand <= 50) enemyList.Add(new EnemyWolf(PlayerLevel));
                if (rand > 50 && rand <= 100) enemyList.Add(new EnemyGoblin(PlayerLevel));
                if (rand > 100 && rand <= 150) enemyList.Add(new EnemyBear(PlayerLevel));
                else enemyList.Add(new EnemyDragon(PlayerLevel));
            }

            
            int listLength = enemyList.Count;
            var enemy = new Enemy[listLength];
            for (int i = 0; i < listLength; i++)
            {
                enemy[i] = enemyList[i];
            }
            return enemy;
        }
    }
}