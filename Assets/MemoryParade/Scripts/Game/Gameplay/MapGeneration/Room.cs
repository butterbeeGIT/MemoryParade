using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration
{
    /// <summary>
    /// Описывает объект комнаты
    /// </summary>
    public class Room
    {
        /// <summary>
        ///  Координаты на карте
        /// </summary>
        public int x, y;
        /// <summary>
        /// параметры комнаты в сетке карты
        /// </summary>
        public int width, height;
        /// <summary>
        /// тип комнаты
        /// </summary>
        public RoomType roomType;

        public Room(int x, int y, int width, int height, RoomType roomType)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.roomType = roomType;
        }

        /// <summary>
        /// Проверяет пересечение двух комнат
        /// </summary>
        public bool Intersects(Room other)
        {
            return (x < other.x + other.width && x + width > other.x &&
                    y < other.y + other.height && y + height > other.y);
        }
    }
}
