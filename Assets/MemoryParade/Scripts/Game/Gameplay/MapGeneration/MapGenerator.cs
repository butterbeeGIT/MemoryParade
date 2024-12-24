using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration
{
    /// <summary>
    /// Генератор карты уровня
    /// </summary>
    public class MapGenerator
    {
        /// <summary>
        /// схематичная карта уровня
        /// 0 - пустое пространство
        /// 1 - стена
        /// 2 - внутренняя часть комнаты или коридора
        /// </summary>
        private int[][] Map;

        /// <summary>
        /// список комнат сгенерированной карты 
        /// (коридоры тоже считаются комнатами)
        /// </summary>
        private List<Room> RoomsOnMap;

        /// <summary>
        /// Размеры карты (ширина и высота)
        /// </summary>
        private readonly int mapWidth;
        private readonly int mapHeight;

        /// <summary>
        /// Создает карту по заданным параметрам
        /// </summary>
        /// <param name="countRooms">Количество комнат</param>
        /// <param name="requiredRooms">Список комнат, которые должны присутствовать на уровне</param>
        public MapGenerator(int countRooms, Room[] requiredRooms, int mapWidth = 100, int mapHeight = 100)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            Map = new int[mapWidth][];
            for (int i = 0; i < mapWidth; i++)
            {
                Map[i] = new int[mapHeight];
            }

            RoomsOnMap = new List<Room>();

            GenerateMap(countRooms, requiredRooms);
        }

        /// <summary>
        /// Генерирует карту, заполняя комнаты и соединяя их коридорами
        /// </summary>
        private void GenerateMap(int countRooms, Room[] requiredRooms)
        {
            Random random = new Random();

            // Добавляем обязательные комнаты
            foreach (var room in requiredRooms)
            {
                PlaceRoom(room);
                RoomsOnMap.Add(room);
            }

            // Генерируем остальные комнаты
            for (int i = RoomsOnMap.Count; i < countRooms; i++)
            {
                Room newRoom = GenerateRandomRoom(random);
                if (!IntersectsWithExistingRooms(newRoom))
                {
                    PlaceRoom(newRoom);
                    RoomsOnMap.Add(newRoom);
                }
            }

            // Соединяем комнаты коридорами
            ConnectRooms();
        }

        /// <summary>
        /// Генерирует случайную комнату
        /// </summary>
        private Room GenerateRandomRoom(Random random)
        {
            int width = random.Next(3, 10); // Минимальная и максимальная ширина комнаты
            int height = random.Next(3, 10); // Минимальная и максимальная высота комнаты
            int x = random.Next(0, mapWidth - width);
            int y = random.Next(0, mapHeight - height);
            RoomType type = (RoomType)random.Next(Enum.GetValues(typeof(RoomType)).Length);

            return new Room(x, y, width, height, type);
        }

        /// <summary>
        /// Проверяет, пересекается ли новая комната с уже существующими
        /// </summary>
        private bool IntersectsWithExistingRooms(Room newRoom)
        {
            return RoomsOnMap.Any(existingRoom => newRoom.Intersects(existingRoom));
        }

        /// <summary>
        /// Размещает комнату на карте
        /// </summary>
        private void PlaceRoom(Room room)
        {
            for (int x = room.x; x < room.x + room.width; x++)
            {
                for (int y = room.y; y < room.y + room.height; y++)
                {
                    Map[x][y] = 2; // Внутреннее пространство комнаты
                }
            }
        }

        /// <summary>
        /// Соединяет комнаты коридорами
        /// </summary>
        private void ConnectRooms()
        {
            for (int i = 1; i < RoomsOnMap.Count; i++)
            {
                Room current = RoomsOnMap[i];
                Room previous = RoomsOnMap[i - 1];

                // Получаем центры комнат
                int currentCenterX = current.x + current.width / 2;
                int currentCenterY = current.y + current.height / 2;
                int previousCenterX = previous.x + previous.width / 2;
                int previousCenterY = previous.y + previous.height / 2;

                // Горизонтальный коридор
                for (int x = Math.Min(currentCenterX, previousCenterX); x <= Math.Max(currentCenterX, previousCenterX); x++)
                {
                    Map[x][previousCenterY] = 2;
                }

                // Вертикальный коридор
                for (int y = Math.Min(currentCenterY, previousCenterY); y <= Math.Max(currentCenterY, previousCenterY); y++)
                {
                    Map[currentCenterX][y] = 2;
                }
            }
        }

        /// <summary>
        /// Получить текущую карту
        /// </summary>
        public int[][] GetMap()
        {
            return Map;
        }

        /// <summary>
        /// Получить список сгенерированных комнат
        /// </summary>
        public List<Room> GetRooms()
        {
            return RoomsOnMap;
        }

    }
}
