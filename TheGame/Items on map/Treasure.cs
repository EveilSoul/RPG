using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Treasure
    {
        public ObjectStructures.Position Position;
        public bool IsEnemy;
        public static ObjectStructures.Position TheLastTreasurePosition;
        public static bool EnemyExist;
        public List<Enemy> enemy;

        public ObjectStructures.Position TreasureGenerationPosition(ObjectStructures.Position playerPosition)
        {
            var treasurePosition = new ObjectStructures.Position
            {
                X = Program.Random.Next(playerPosition.X - Window.MapSizeX / 3,
                playerPosition.X + Window.MapSizeX / 3),
                Y = Program.Random.Next(playerPosition.Y - Window.MapSizeY / 3,
                playerPosition.Y + Window.MapSizeY / 3)
            };

            return treasurePosition;
        }

        public Treasure(Player player)
        {
            this.Position = TreasureGenerationPosition(player.Position);

            bool exictPosition = false;
            do
            {
                exictPosition = false;
                foreach (var city in Program.Cities)
                {
                    if (city.Position.X == this.Position.X && city.Position.Y == this.Position.Y)
                    {
                        this.Position = TreasureGenerationPosition(player.Position);
                        exictPosition = true;
                        break;
                    }
                }
            } while (exictPosition);

            var rand = Program.Random.Next(0, 100 - player.Level*(int)Math.Sqrt(player.Level));
            if (rand < 80) this.IsEnemy = true;
            else this.IsEnemy = false;

            this.enemy = CreateEnemyForTreasure(player.Level);

        }

        public void CkeckTreasure(Player player)
        {
            if (EnemyExist && player.Position.X == this.Position.X && player.Position.Y == this.Position.Y) Battle.GoBattle(player, this.enemy);
            player.AddMoney(GetReward(player.Level));
        }

        public List<Enemy> CreateEnemyForTreasure(int PlayerLevel)
        {

            int rand = Program.Random.Next(1 - PlayerLevel, 301 + PlayerLevel);
            EnemyExist = true;
            PlayerLevel-=3;

            if (rand < 1) return EnemyMix.CreateEnemyMix(PlayerLevel);
            if (rand > 0 && rand <= 50) return EnemyBear.CreateEnemyBear(PlayerLevel);
            if (rand > 50 && rand <= 100) return EnemyOrk.CreateEnemyOrk(PlayerLevel); 
            if (rand > 100 && rand <= 150) return EnemyGriffin.CreateGriffin(PlayerLevel); 
            if (rand > 150 && rand <= 200) return EnemyTriton.CreateEnemyTriton(PlayerLevel); 
            if (rand > 200 && rand <= 250) return EnemyBandit.CreateEnemyBandit(PlayerLevel); 
            if (rand > 250 && rand <= 300) return EnemyDarkKnight.CreateEnemyDarkKnight(PlayerLevel); 
            return EnemyDragon.CreateEnemyDragon(PlayerLevel);
        }

        public int GetReward(int playerLevel)
        {
            return Program.Random.Next(30 + playerLevel * playerLevel, 80 + playerLevel * playerLevel * playerLevel);
        }

        public bool MayNewTreasure(ObjectStructures.Position lastPosition, ObjectStructures.Position newPosition)
        {
            return (int)(Math.Sqrt((lastPosition.X - newPosition.X) * (lastPosition.X - newPosition.X) +
                (newPosition.Y - lastPosition.Y) * (newPosition.Y - lastPosition.Y))) > 10;
        }
    }
}
