using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Enemy
    {
        public string Name;
        public bool IsLive;
        public int PowerAttack;
        public int Health;
        public float Accuracy;

        //атака на мостра
        public void OnEnemyAtack(int damage, Enemy[] enemy, int[] numberAtackEnemy)
        {
            for (int i = 0; i < numberAtackEnemy.Length; i++)
            {
                enemy[i].Health -= damage;
                if (enemy[i].Health <= 0) enemy[i].IsLive = false;
            }
        }

        //атака монстра
        public int Attack(Enemy[] enemy)
        {
            int damage = 0;
            for (int i = 0; i < enemy.Length; i++)
            {
                damage += (int)(enemy[i].PowerAttack * enemy[i].Accuracy) + Program.Random.Next(-5, 6);
            }
            return damage;
        }

        //проверка того, жив ли хоть один враг
        public bool IsEnemyLive(Enemy[] enemy)
        {
            bool result = false;
            for (int i = 0; i < enemy.Length; i++)
            {
                result = result || enemy[i].IsLive;
            }
            return result;
        }

        public static ObjectStructures.Position EnemyGenerationPosition(ObjectStructures.Position playerPosition)
        {
            var enemyPosition = new ObjectStructures.Position {
                X = Program.Random.Next(playerPosition.X - 2 * Window.WindowSizeX, 
                playerPosition.X + 2 * Window.WindowSizeX),
                Y = Program.Random.Next(playerPosition.Y - 2 * Window.WindowSizeY, 
                playerPosition.Y + 2 * Window.WindowSizeY) };
            return enemyPosition;
        }


        public void PrintEnemy(int enemyCount, Enemy enemy)
        {
            Console.Clear();
            Console.WriteLine("You have " + enemyCount + " enemy!");
            for (int i = 0; i < enemyCount; i++)
            {
                Console.WriteLine(enemy.Name);
                Console.WriteLine("Health: {0}", enemy.Health);
                Console.WriteLine("Power atack: {0}", enemy.PowerAttack);

            }
        }

        public static bool IsEnemyNear(ObjectStructures.Position enemyPosition, ObjectStructures.Position playerPosition)
        {
            return Math.Abs(enemyPosition.X - playerPosition.X) <= Window.WindowSizeX / 2 && 
                Math.Abs(enemyPosition.Y - playerPosition.Y) <= Window.WindowSizeY / 2;
        }

        public static Enemy[] CreateEnemy(int PlayerLevel)
        {
            //увеличивается вероятность выпадения дракона в зависиости от уровня
            int rand = Program.Random.Next(1, 151 + PlayerLevel * 2);

            if (rand >= 1 && rand <= 50) return EnemyWolf.CreateEnemyWolf();
            if (rand > 50 && rand <= 100) return EnemyGoblin.CreateEnemyGoblin();
            if (rand > 100 && rand <= 150) return EnemyBear.CreateEnemyBear();
            return EnemyDragon.CreateEnemyDragon();
        }


        
        



    }
}
