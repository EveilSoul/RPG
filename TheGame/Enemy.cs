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
        public float Accuracy = 0.9f;
        public ObjectStructures.Position Position;


        //атака на мостра
        public static void OnEnemyAtack(Enemy[] enemy, int[] damageAtackEnemy)
        {
            for (int i = 0; i < damageAtackEnemy.Length; i++)
            {
                enemy[i].Health -= damageAtackEnemy[i];
                if (enemy[i].Health <= 0)
                {
                    enemy[i].Health = 0;
                    enemy[i].IsLive = false;
                }
            }
        }

        //атака монстра
        public int EnemyAttack()
        {
            if (Program.Random.NextDouble() <= this.Accuracy && this.IsLive)
                return this.PowerAttack + Program.Random.Next(-3, 4);
            else return 0;
        }

        //проверка того, жив ли хоть один враг
        public static bool IsEnemyLive(Enemy[] enemy)
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
                X = Program.Random.Next(playerPosition.X - Window.WindowSizeX/3, 
                playerPosition.X + Window.WindowSizeX/3),
                Y = Program.Random.Next(playerPosition.Y - Window.WindowSizeY/3, 
                playerPosition.Y + Window.WindowSizeY/3) }
            ;
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

        public static bool IsEnemyFar(ObjectStructures.Position enemyPosition, ObjectStructures.Position playerPosition)
        {
            return Math.Abs(enemyPosition.X - playerPosition.X) >= Window.WindowSizeX &&
                Math.Abs(enemyPosition.Y - playerPosition.Y) >= Window.WindowSizeY;
        }

        public static Enemy[] CreateEnemy(int PlayerLevel)
        {
            //увеличивается вероятность выпадения дракона в зависиости от уровня
            int rand = Program.Random.Next(1, 151 + PlayerLevel * PlayerLevel * PlayerLevel);

            if (rand >= 1 && rand <= 50) return EnemyWolf.CreateEnemyWolf(PlayerLevel);
            if (rand > 50 && rand <= 100) return EnemyGoblin.CreateEnemyGoblin(PlayerLevel);
            if (rand > 100 && rand <= 150) return EnemyBear.CreateEnemyBear(PlayerLevel);
            return EnemyDragon.CreateEnemyDragon(PlayerLevel);
        }


        
        



    }
}
