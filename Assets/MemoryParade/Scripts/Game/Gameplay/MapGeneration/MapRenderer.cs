using System;
using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration
{
    /// <summary>
    /// Отрисовывает карту и объекты карты
    /// </summary>
    public static class MapRenderer
    {
        public static GameObject WallPrefab;
        public static GameObject HorizontalCorridorPrefab;
        public static GameObject VerticalCorridorPrefab;
        public static GameObject FloorPrefab;
        public static GameObject DoorPrefab;
        public static GameObject WallAnglePrefab;
        public static GameObject EmptyWallPrefab;
        public static Transform MapParent; // Родительский объект для всех элементов карты
        public static Vector2 CellSize = new Vector2(1, 1); // Размер одной клетки карты

        /// <summary>
        /// Добавляет на карту визуально отрисованную комнату с учетом поворотов стен и углов.
        /// </summary>
        public static void AddRoom(Room room)
        {
            for (int i = room.Y; i <= room.Y + room.Height; i++)
            {
                for (int j = room.X; j <= room.X + room.Width; j++)
                {
                    Vector3 position = new Vector3(j * CellSize.x, -i * CellSize.y, 0.1f);

                    // Верхняя и нижняя стены
                    if (i == room.Y) // Верхняя стена
                    {
                        InstantiatePrefab(WallPrefab, position, Quaternion.Euler(0, 0, 0));
                    }
                    else if (i == room.Y + room.Height) // Нижняя стена
                    {
                        InstantiatePrefab(WallPrefab, position, Quaternion.Euler(0, 0, 180));
                    }

                    // Левые и правые стены
                    else if (j == room.X) // Левая стена
                    {
                        InstantiatePrefab(WallPrefab, position, Quaternion.Euler(0, 0, 90));
                    }
                    else if (j == room.X + room.Width) // Правая стена
                    {
                        InstantiatePrefab(WallPrefab, position, Quaternion.Euler(0, 0, -90));
                    }

                    // Пол комнаты
                    else
                    {
                        position.z = -0.1f;
                        InstantiatePrefab(FloorPrefab, position, Quaternion.identity);
                    }
                }
            }

            DrawCorners(room);
        }


        /// <summary>
        /// Отрисовывает углы комнаты.
        /// </summary>
        /// <param name="room">Комната для отрисовки углов.</param>
        private static void DrawCorners(Room room)
        {
            float leftX = room.X * CellSize.x;
            float rightX = (room.X + room.Width) * CellSize.x;
            float topY = -room.Y * CellSize.y;
            float bottomY = -(room.Y + room.Height) * CellSize.y;
            //float constZ = -0.1f;
            // Левый верхний угол
            Vector3 topLeft = new Vector3(leftX, topY, 0);
            InstantiatePrefab(WallAnglePrefab, topLeft, Quaternion.Euler(0, 0, 0));

            // Правый верхний угол
            Vector3 topRight = new Vector3(rightX, topY, 0);
            InstantiatePrefab(WallAnglePrefab, topRight, Quaternion.Euler(0, 0, -90));

            // Левый нижний угол
            Vector3 bottomLeft = new Vector3(leftX, bottomY, 0);
            InstantiatePrefab(WallAnglePrefab, bottomLeft, Quaternion.Euler(0, 0, 90));

            // Правый нижний угол
            Vector3 bottomRight = new Vector3(rightX, bottomY, 0);
            InstantiatePrefab(WallAnglePrefab, bottomRight, Quaternion.Euler(0, 0, 180));

            
        }


        /// <summary>
        /// Добавляет визуально отрисованный коридор
        /// </summary>
        public static void AddCorridor(Room room1, Room room2)
        {
            var (x1, y1) = room1.Center();
            var (x2, y2) = room2.Center();
            x1 = AddHorizontalCorridor(room1,  room2, x1, y1, x2, y2);
            AddVerticalCorridor(room1, room2, x1, y1, x2, y2);
        }
        

        /// <summary>
        /// отрисовывает горизонтальный коридор
        /// </summary>
        /// <returns>измененную координату x</returns>
        private static int AddHorizontalCorridor(Room room1, Room room2, int x1, int y1, int x2, int y2)
        {
            bool needAddCorridor = true;
            while (needAddCorridor)//x1 != x2)
            {
                Vector3 position = new Vector3(x1 * CellSize.x, -y1 * CellSize.y, 0);
                if(!(room1.isFloorRoom(x1, y1) || room2.isFloorRoom(x1, y1))) //если не пол комнаты
                {
                    InstantiatePrefab(HorizontalCorridorPrefab, position, Quaternion.identity);
                    Vector3 positionEmptyWall = position;
                    //ограничители
                    positionEmptyWall.y = position.y  + CellSize.y;
                    InstantiatePrefab(EmptyWallPrefab, positionEmptyWall, Quaternion.identity);
                    positionEmptyWall.y = position.y -  CellSize.y;
                    InstantiatePrefab(EmptyWallPrefab, positionEmptyWall, Quaternion.identity);
                }
                if (x1 == x2)
                    return x1;                   
                x1 += x1 < x2 ? 1 : -1; // Двигаемся к целевой точке
            }
            return x1;
        }

        private static int AddVerticalCorridor(Room room1, Room room2, int x1, int y1, int x2, int y2)
        {
            bool needAddCorridor = true;
            while (needAddCorridor)//y1 != y2)
            {
                Vector3 position = new Vector3(x1 * CellSize.x, -y1 * CellSize.y, 0);

                if (!(room1.isFloorRoom(x1, y1) || room2.isFloorRoom(x1, y1))) //если не пол комнаты
                {

                    InstantiatePrefab(VerticalCorridorPrefab, position, Quaternion.identity);
                    Vector3 positionEmptyWall = position;
                    positionEmptyWall.x = position.x +  CellSize.x;
                    InstantiatePrefab(EmptyWallPrefab, positionEmptyWall, Quaternion.identity);
                    positionEmptyWall.x = position.x - CellSize.x;
                    InstantiatePrefab(EmptyWallPrefab, positionEmptyWall, Quaternion.identity);
                }
                if (y1 == y2)
                    return y1;
                y1 += y1 < y2 ? 1 : -1; // Двигаемся к целевой точке
            }
            return y1;
        }

        /// <summary>
        /// Инстанцирует префаб и помещает его в сцену
        /// </summary>
        private static void InstantiatePrefab(GameObject prefab, Vector3 position, Quaternion rotation)
        {

            if (prefab == null)
            {
                Debug.LogWarning("Prefab is not assigned!");
                return;
            }

            GameObject instance = UnityEngine.Object.Instantiate(prefab, position, rotation, MapParent);
            instance.name = prefab.name; // Опционально, для удобства отладки
           // Debug.Log($"Instantiated {prefab.name} at {position} with rotation {rotation}");
        }
    }
}
