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
        public int MoneyReward = 5;
        public int SkillReward = 10;
        public bool Mimicry;
        public static ObjectStructures.Position TheLastEnemyPosition;
        public static bool EnemyExist = false;

        public static int GetSkill(int offset, int divider, int minValue, int level) =>
            (int)Math.Pow(30 - level, 2) / divider + minValue;

        public static Tuple<int, int> GetReward(List<Enemy> enemy)
        {
            int money = 0;
            int skill = 0;
            foreach (var e in enemy)
            {
                money += e.MoneyReward;
                skill += e.SkillReward;
            }
            return Tuple.Create(money, skill);
        }

        //атака на мостра
        public virtual void OnEnemyAtack(Enemy enemy, int damageAtackEnemy)
        {
            enemy.Health -= damageAtackEnemy;
        }

        //атака монстра
        public int EnemyAttack()
        {
            if (Program.Random.NextDouble() <= this.Accuracy && this.IsLive)
                return this.PowerAttack + Program.Random.Next(-3, 4);
            else return 0;
        }

        public static void CheckEnemyDie(List<Enemy> enemy)
        {
            for (int i = enemy.Count - 1; i >= 0; i--)
                if (enemy[i].Health <= 0)
                    enemy.RemoveAt(i);
        }

        //проверка того, жив ли хоть один враг
        public static bool IsEnemyLive(List<Enemy> enemy)
        {
            bool result = false;
            for (int i = 0; i < enemy.Count; i++)
            {
                result = result || enemy[i].IsLive;
            }
            return result;
        }

        public static ObjectStructures.Position EnemyGenerationPosition(ObjectStructures.Position playerPosition)
        {
            var enemyPosition = new ObjectStructures.Position
            {
                X = Program.Random.Next(playerPosition.X - Window.WindowSizeX / 2,
                playerPosition.X + Window.WindowSizeX / 2),
                Y = Program.Random.Next(playerPosition.Y - Window.WindowSizeY / 2,
                playerPosition.Y + Window.WindowSizeY / 2)
            }
            ;
            return enemyPosition;
        }

        public static bool IsEnemyNear(ObjectStructures.Position enemyPosition, ObjectStructures.Position playerPosition)
        {
            return Math.Abs(enemyPosition.X - playerPosition.X) <= Window.WindowSizeX / 4 &&
                Math.Abs(enemyPosition.Y - playerPosition.Y) <= Window.WindowSizeY / 4;
        }

        public static bool IsEnemyFar(ObjectStructures.Position enemyPosition, ObjectStructures.Position playerPosition)
        {
            return Math.Abs(enemyPosition.X - playerPosition.X) >= Window.WindowSizeX / 2 &&
                Math.Abs(enemyPosition.Y - playerPosition.Y) >= Window.WindowSizeY / 2;
        }

        public static Tuple<ObjectStructures.Position, List<Enemy>> CreateEnemy(int PlayerLevel, ObjectStructures.Position playerPosition)
        {
            //увеличивается вероятность выпадения дракона и смешаных монстров в зависиости от уровня
            int rand = Program.Random.Next(1 - 2 * PlayerLevel, 301 + 3 * PlayerLevel - (PlayerLevel < 3 ? 150 : 0));
            var enemyPosition = EnemyGenerationPosition(playerPosition);
            EnemyExist = true;

            if (rand < 1) return Tuple.Create(enemyPosition, EnemyMix.CreateEnemyMix(PlayerLevel));
            if (rand >= 1 && rand <= 50) return Tuple.Create(enemyPosition, EnemyWolf.CreateEnemyWolf(PlayerLevel));
            if (rand > 50 && rand <= 100) return Tuple.Create(enemyPosition, EnemyGoblin.CreateEnemyGoblin(PlayerLevel));
            if (rand > 100 && rand <= 150) return Tuple.Create(enemyPosition, EnemyBear.CreateEnemyBear(PlayerLevel));
            if (rand > 150 && rand <= 200) return Tuple.Create(enemyPosition, EnemyOrk.CreateEnemyOrk(PlayerLevel)); //3 level +
            if (rand > 200 && rand <= 220) return Tuple.Create(enemyPosition, EnemyGriffin.CreateGriffin(PlayerLevel)); //3 level +
            if (rand > 220 && rand <= 250) return Tuple.Create(enemyPosition, EnemyTriton.CreateEnemyTriton(PlayerLevel)); //3 level +
            if (rand > 250 && rand <= 300) return Tuple.Create(enemyPosition, EnemyBandit.CreateEnemyBandit(PlayerLevel)); //3 level +
            if (rand > 300 && rand <= 350) return Tuple.Create(enemyPosition, EnemyDarkKnight.CreateEnemyDarkKnight(PlayerLevel)); //3 level +
            return Tuple.Create(enemyPosition, EnemyDragon.CreateEnemyDragon(PlayerLevel));
        }


        public static void CheckEnemy(List<Enemy> enemy, Player player, ObjectStructures.Position enemyPosition)
        {
            if (EnemyExist && enemyPosition.X == player.Position.X && enemyPosition.Y == player.Position.Y && !enemy[0].Mimicry) Battle.GoBattle(player, enemy);
            else if (EnemyExist && IsEnemyNear(enemyPosition, player.Position) && enemy[0].Mimicry) Battle.GoBattle(player, enemy);
        }




    }
}
