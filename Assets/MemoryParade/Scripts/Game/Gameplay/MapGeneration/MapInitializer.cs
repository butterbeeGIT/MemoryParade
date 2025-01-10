using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration
{
    /// <summary>
    /// загружает префабы карты
    /// </summary>
    public class MapInitializer : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject wallPrefab;
        public GameObject horizontalCorridorPrefab;
        public GameObject verticalCorridorPrefab;
        public GameObject floorPrefab;
        public GameObject doorPrefab;
        public GameObject wallAnglePrefab;
        public GameObject emptyWallPrefab;
        //персонаж
        public GameObject player;

        public static Vector2 CellSize = new Vector2(1, 1); // Размер одной клетки карты

        void Start()
        {
            // Подключаем префабы к MapRenderer
            MapRenderer.WallPrefab = wallPrefab;
            MapRenderer.HorizontalCorridorPrefab = horizontalCorridorPrefab;
            MapRenderer.VerticalCorridorPrefab = verticalCorridorPrefab;
            MapRenderer.FloorPrefab = floorPrefab;
            MapRenderer.DoorPrefab = doorPrefab;
            MapRenderer.WallAnglePrefab = wallAnglePrefab;
            MapRenderer.EmptyWallPrefab = emptyWallPrefab;

            // Генерируем и отрисовываем карту
            Room spawnRoom = MapGenerator.GenerateAndRenderMap();
            HandleCollisions();
            SpawnPlayerInRoom(player, spawnRoom);
            
        }

        /// <summary>
        /// Перемещает игрока в центр одной из комнат
        /// </summary>
        /// <param name="player"></param>
        /// <param name="spawnRoom"></param>
        void SpawnPlayerInRoom(GameObject player, Room spawnRoom)
        {
            if (spawnRoom != null)
            {
                var (centerX, centerY) = spawnRoom.Center();

                player.transform.position = new Vector3(centerX * CellSize.x, -centerY * CellSize.y, 0);
            }
        }

        void HandleCollisions()
        {
            foreach (var wall in GameObject.FindGameObjectsWithTag("Wall"))
            {
                BoxCollider2D wallCollider = wall.GetComponent<BoxCollider2D>();
                if (wallCollider == null) continue;

                Vector2 wallCenter = new Vector2(wallCollider.bounds.center.x, wallCollider.bounds.center.y);
                Vector2 wallSize = wallCollider.bounds.size;

                Collider2D[] overlaps = Physics2D.OverlapBoxAll(wallCenter, wallSize, 0);
                foreach (var overlap in overlaps)
                {
                    if (overlap.gameObject == wall) continue; // Пропускаем самого себя

                    if (overlap.CompareTag("Corridor") || overlap.CompareTag("Floor"))
                    {
                        BoxCollider2D overlapCollider = overlap.GetComponent<BoxCollider2D>();
                        if (overlapCollider != null)
                        {
                            Vector2 overlapCenter = new Vector2(overlapCollider.bounds.center.x, overlapCollider.bounds.center.y);
                            Vector2 overlapSize = overlapCollider.bounds.size;

                            float tolerance = 0.01f; // Допустимая погрешность
                            bool positionsMatch = Vector2.Distance(overlapCenter, wallCenter) < tolerance;
                            bool sizesMatch = Mathf.Abs(overlapSize.x - wallSize.x) < tolerance &&
                                              Mathf.Abs(overlapSize.y - wallSize.y) < tolerance;

                            if (positionsMatch && sizesMatch)
                            {
                                wallCollider.enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
        }

        ///// <summary>
        ///// Обеспечивает проходимость коридоров
        ///// </summary>
        //void HandleCollisions()
        //{
        //    foreach (var wall in GameObject.FindGameObjectsWithTag("Wall"))
        //    {
        //        BoxCollider2D wallCollider = wall.GetComponent<BoxCollider2D>();
        //        if (wallCollider == null) continue;

        //        // Центр и размер коллайдера для OverlapBoxAll, игнорируем Z
        //        Vector2 wallCenter = new Vector2(wallCollider.bounds.center.x, wallCollider.bounds.center.y);
        //        Vector2 wallSize = wallCollider.bounds.size;

        //        // Получаем все пересечения
        //        Collider2D[] overlaps = Physics2D.OverlapBoxAll(wallCenter, wallSize, 0);
        //        foreach (var overlap in overlaps)
        //        {
        //            if (overlap.CompareTag("Corridor"))
        //            {
        //                BoxCollider2D corridorCollider = overlap.GetComponent<BoxCollider2D>();
        //                if (corridorCollider != null)
        //                {
        //                    // Игнорируем Z при сравнении центров и размеров
        //                    Vector2 corridorCenter = new Vector2(corridorCollider.bounds.center.x, corridorCollider.bounds.center.y);
        //                    Vector2 corridorSize = corridorCollider.bounds.size;

        //                    if (corridorCenter == wallCenter && corridorSize == wallSize)
        //                    {
        //                        wallCollider.enabled = false; // Отключаем, если размеры и позиции совпадают
        //                        break;
        //                    }
        //                }
        //            }
        //            else if (overlap.CompareTag("Floor"))
        //            {
        //                BoxCollider2D floorCollider = overlap.GetComponent<BoxCollider2D>();
        //                if (floorCollider != null)
        //                {
        //                    // Игнорируем Z при сравнении центров и размеров
        //                    Vector2 floorCenter = new Vector2(floorCollider.bounds.center.x, floorCollider.bounds.center.y);
        //                    Vector2 floorSize = floorCollider.bounds.size;

        //                    if (floorCenter == wallCenter)
        //                    {
        //                        wallCollider.enabled = false; // Отключаем, если размеры и позиции совпадают
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }

}
