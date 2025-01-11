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
        public GameObject enemy;

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

            Room spawnRoom = GeneratingMap();
            SpawnPlayerInRoom(player, spawnRoom);
            SpawnEnemyInRoom(enemy, spawnRoom);
        }

        /// <summary>
        /// генерирует работчую карту
        /// </summary>
        /// <returns>комната спавна персонажа</returns>
        Room GeneratingMap()
        {
            Room spawnRoom;
            spawnRoom = MapGenerator.GenerateAndRenderMap();
            while (CheckRegeneration())
            {
                DestroyMap();
                spawnRoom = MapGenerator.GenerateAndRenderMap();
            }

            HandleCollisions();
            
            return spawnRoom;
            
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

        /// <summary>
        /// Перемещает врага в комнату
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="spawnRoom"></param>
        void SpawnEnemyInRoom(GameObject enemy, Room spawnRoom)
        {
            if (spawnRoom != null)
            {
                var (centerX, centerY) = spawnRoom.Center();

                enemy.transform.position = new Vector3((centerX+2) * CellSize.x, -(centerY+2) * CellSize.y, 0);
            }
        }

        /// <summary>
        /// Отключает Box collider у стен, там где они сопадают с другими объектами карты
        /// </summary>
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

        /// <summary>
        /// Проверяет на сопадения углы комнат и коридоры
        /// Если они сопадают, то скорее всего карта сгенерировалась так что одна из стен комнаты превратилась в коридор
        /// </summary>
        /// <returns>нужна ли перегенерация</returns>
        bool CheckRegeneration()
        {
            //bool needRegeneration = false; // Флаг для перегенерации карты
            foreach (var corner in GameObject.FindGameObjectsWithTag("Corner"))
            {
                BoxCollider2D cornerCollider = corner.GetComponent<BoxCollider2D>();
                if (cornerCollider == null) continue;

                Vector2 cornerCenter = new Vector2(cornerCollider.bounds.center.x, cornerCollider.bounds.center.y);
                Vector2 cornerSize = cornerCollider.bounds.size;

                Collider2D[] overlaps = Physics2D.OverlapBoxAll(cornerCenter, cornerSize, 0);
                foreach (var overlap in overlaps)
                {
                    if (overlap.gameObject == corner) continue; // Пропускаем самого себя

                    if (overlap.CompareTag("Corridor"))
                    {
                        BoxCollider2D overlapCollider = overlap.GetComponent<BoxCollider2D>();
                        if (overlapCollider != null)
                        {
                            Vector2 overlapCenter = new Vector2(overlapCollider.bounds.center.x, overlapCollider.bounds.center.y);
                            Vector2 overlapSize = overlapCollider.bounds.size;

                            float tolerance = 0.01f; // Допустимая погрешность
                            bool positionsMatch = Vector2.Distance(overlapCenter, cornerCenter) < tolerance;
                            bool sizesMatch = Mathf.Abs(overlapSize.x - cornerSize.x) < tolerance &&
                                              Mathf.Abs(overlapSize.y - cornerSize.y) < tolerance;

                            if (positionsMatch && sizesMatch)
                            {
                                Debug.LogWarning($"Пересечение коридора с углом комнаты");
                                return true;   
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Удаляет все объекты карты
        /// </summary>
        void DestroyMap()
        {
            foreach (var obj in GameObject.FindGameObjectsWithTag("Wall")) Destroy(obj);
            foreach (var obj in GameObject.FindGameObjectsWithTag("Floor")) Destroy(obj);
            foreach (var obj in GameObject.FindGameObjectsWithTag("Corridor")) Destroy(obj);
            foreach (var obj in GameObject.FindGameObjectsWithTag("Corner")) Destroy(obj);
        }
    }
}
