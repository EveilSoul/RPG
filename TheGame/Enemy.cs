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
        public int Count;
        public float Accuracy;

        public void OnEnemyAtack(int damage)
        {
            this.Health -= damage;
        }

        public int Attack()
        {
            return (int)(PowerAttack * Accuracy);
        }

        
        public void PrintEnemy(int enemyCount, Enemy[] res)
        {
            Console.Clear();
            Console.WriteLine("You have " + enemyCount + " enemy!");
            for (int i = 0; i < enemyCount; i++)
            {
                Console.WriteLine(res[i].Name);
                Console.WriteLine("Health: " + res[i].Health);
                Console.WriteLine("Power atack: " + res[i].PowerAttack);

            }
        }


        public Enemy[] CreateEnemy()
        {
            int rand = Program.Random.Next(1, 152);
            if (rand>=1 && rand <= 50)
            {
                
                this.Count = Program.Random.Next(3, 6);
                var res = new Enemy[this.Count];
                for (int i = 0; i < this.Count; i++)
                {
                    res[i] = new Enemy
                    {
                        Name = "Wolf",
                        PowerAttack = 10,
                        IsLive = true,
                        Health = 50
                    };
                }
                return res;

            }
            if (rand>50 && rand <= 100)
            {
                this.Count = Program.Random.Next(2, 4);
                var res = new Enemy[this.Count];
                for (int i = 0; i < this.Count; i++)
                {
                    res[i] = new Enemy
                    {
                        Name = "Goblin",
                        PowerAttack = 20,
                        IsLive = true,
                        Health = 50
                    };
                }
                return res;
            }
            if (rand > 100 && rand <= 150)
            {
                this.Count = 1;
                var res = new Enemy[1];
                res[0]=new Enemy
                {
                    Name = "Bear",
                    PowerAttack = 20,
                    IsLive = true,
                    Health = 100,
                    Count = 1
                };
                return res;

            }
            else
            {
                this.Count = 1;
                var res = new Enemy[1];
                res[0]=new Enemy
                {
                    Name = "Dragon",
                    PowerAttack = 150,
                    IsLive = true,
                    Health = 500,
                    Count = 1
                };
                return res;
            }
            

            }


        }
}
