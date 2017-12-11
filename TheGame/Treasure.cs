using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Treasure
    {
        public static ObjectStructures.Position TreasureGenerationPosition(ObjectStructures.Position playerPosition)
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

       
    }
}
