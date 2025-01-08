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
                    Vector3 position = new Vector3(j * CellSize.x, -i * CellSize.y, 0);

                    // Верхняя и нижняя стены
                    if (i == room.Y) // Верхняя стена
                    {
                        InstantiatePrefab(WallPrefab, position, Quaternion.Euler(0, 0, 0));
                    }
                    else if (i == room.Y + room.Height) // Нижняя стена
                    {
                        InstantiatePrefab(WallPrefab, position, Quaternion.Euler(0, 180, 0));
                    }

                    // Левые и правые стены
                    else if (j == room.X) // Левая стена
                    {
                        InstantiatePrefab(WallPrefab, position, Quaternion.Euler(0, -90, 0));
                    }
                    else if (j == room.X + room.Width) // Правая стена
                    {
                        InstantiatePrefab(WallPrefab, position, Quaternion.Euler(0, 90, 0));
                    }

                    // Пол комнаты
                    else
                    {
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
            // Левый верхний угол
            Vector3 topLeft = new Vector3(leftX, 0, topY);
            InstantiatePrefab(WallAnglePrefab, topLeft, Quaternion.Euler(0, 0, 0));

            // Правый верхний угол
            Vector3 topRight = new Vector3(rightX, 0, topY);
            InstantiatePrefab(WallAnglePrefab, topRight, Quaternion.Euler(0, 90, 0));

            // Левый нижний угол
            Vector3 bottomLeft = new Vector3(leftX, 0, bottomY);
            InstantiatePrefab(WallAnglePrefab, bottomLeft, Quaternion.Euler(0, -90, 0));

            // Правый нижний угол
            Vector3 bottomRight = new Vector3(rightX, 0, bottomY);
            InstantiatePrefab(WallAnglePrefab, bottomRight, Quaternion.Euler(0, 180, 0));
        }


        /// <summary>
        /// Добавляет визуально отрисованный коридор
        /// </summary>
        public static void AddCorridor(int x1, int y1, int x2, int y2)
        {
            x1 = AddHorizontalCorridor(x1, y1, x2, y2);
            AddVerticalCorridor(x1, y1, x2, y2);
        }

        private static int AddHorizontalCorridor(int x1, int y1, int x2, int y2)
        {
            while (x1 != x2)
            {
                Vector3 position = new Vector3(x1 * CellSize.x, -y1 * CellSize.y, 0);
                InstantiatePrefab(HorizontalCorridorPrefab, position, Quaternion.identity);
                x1 += x1 < x2 ? 1 : -1; // Двигаемся к целевой точке
            }
            return x1;
        }

        private static int AddVerticalCorridor(int x1, int y1, int x2, int y2)
        {
            while (y1 != y2)
            {
                Vector3 position = new Vector3(x1 * CellSize.x, -y1 * CellSize.y, 0);
                InstantiatePrefab(VerticalCorridorPrefab, position, Quaternion.identity);
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
            Debug.Log($"Instantiated {prefab.name} at {position} with rotation {rotation}");
        }
    }
}



//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration
//{
//    /// <summary>
//    /// Отрисовывает карту и объекты карты
//    /// </summary>
//    public static class MapRenderer
//    {
//        /// <summary>
//        /// добавляет на карту символически отрисованную комнату
//        /// </summary>
//        /// <param name="room"></param>
//        /// <param name="map"></param>
//        public static void AddSymbolicRoomOnMap(Room room, char[,] map)
//        {
//            for (int i = room.Y; i < room.Y + room.Height; i++)
//            {
//                for (int j = room.X; j < room.X + room.Width; j++)
//                {
//                    if (room.isWall(j, i))
//                        map[i, j] = '#';
//                    else
//                        map[i, j] = '.';
//                }
//            }
//        }

//        /// <summary>
//        /// добавляет на карту символически отрисованный коридор
//        /// </summary>
//        /// <param name="room"></param>
//        /// <param name="map"></param>
//        public static void AddSymbolicCorridorOnMap(int x1, int y1, int x2, int y2, char[,] map)
//        {
//            x1 = AddSymbolicHorizontalCorridorOnMap(x1, y1, x2, y2, map);
//            AddSymbolicVerticalCorridorOnMap(x1, y1, x2, y2, map);


//        }

//        /// <summary>
//        /// строит горизонтальный коридор при необходимости
//        /// </summary>
//        /// <param name="x1">начальная точка</param>
//        /// <param name="y1"></param>
//        /// <param name="x2">конечная точка</param>
//        /// <param name="y2"></param>
//        /// <param name="map">символьная карта</param>
//        /// <returns>координата x до которой удалось построить коридор</returns>
//        public static int AddSymbolicHorizontalCorridorOnMap(int x1, int y1, int x2, int y2, char[,] map)
//        {

//            while (x1 != x2)
//            {
//                int yOffset = y1; // Ограничиваем смещение, чтобы не выйти за границы карты.
//                char originalSymb = map[yOffset, x1];
//                int nextX = x1 < x2 ? x1 + 1 : x1 - 1;
//                if (originalSymb == '#' && map[yOffset, nextX] == '#')
//                {
//                    if (y1 != y2)
//                    {
//                        y1 = AddSymbolicVerticalCorridorOnMap(x1, y1, x2, y2, map);
//                        break;
//                    }
//                    originalSymb = map[yOffset, x1];
//                }

//                map[yOffset, x1] = originalSymb switch
//                {
//                    ' ' => '-',
//                    '#' => 'D',
//                    _ => originalSymb
//                };
//                x1 += x1 < x2 ? 1 : -1; // Двигаемся к целевой точке.
//            }
//            return x1;
//        }

//        /// <summary>
//        /// строит вертикальный коридор при необходимости
//        /// </summary>
//        /// <param name="x1">начальная точка</param>
//        /// <param name="y1"></param>
//        /// <param name="x2">конечная точка</param>
//        /// <param name="y2"></param>
//        /// <param name="map">символьная карта</param>
//        /// <returns>координата y до которой удалось построить коридор</returns>

//        public static int AddSymbolicVerticalCorridorOnMap(int x1, int y1, int x2, int y2, char[,] map)
//        {
//            while (y1 != y2)
//            {
//                int xOffset = x1; // Ограничиваем смещение.
//                char originalSymb = map[y1, xOffset];
//                int nextY = y1 < y2 ? y1 + 1 : y1 - 1;
//                if (originalSymb == '#' && map[nextY, xOffset] == '#')
//                {
//                    if (x1 != x2)
//                    {
//                        x1 = AddSymbolicHorizontalCorridorOnMap(x1, y1, x2, y2, map);
//                        break;
//                    }

//                    originalSymb = map[y1, xOffset];
//                }
//                map[y1, xOffset] = originalSymb switch
//                {
//                    ' ' => '|',
//                    '#' => 'D',
//                    _ => originalSymb
//                };
//                y1 += y1 < y2 ? 1 : -1; // Двигаемся к целевой точке.
//            }
//            return y1;
//        }

//    }
//}
