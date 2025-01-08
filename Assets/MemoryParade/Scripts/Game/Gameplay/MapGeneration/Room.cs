using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration
{
    public class Room
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Room(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public (int, int) Center()
        {
            return (X + Width / 2, Y + Height / 2);
        }

        /// <summary>
        /// проверяет, является ли точка стеной этой комнаты
        /// </summary>
        /// <param name="other_x"></param>
        /// <param name="other_y"></param>
        /// <returns></returns>
        public bool isWall(int other_x, int other_y)
        {
            return other_y == Y || other_y == Y + Height - 1 || other_x == X || other_x == X + Width - 1;
        }
    }
}
