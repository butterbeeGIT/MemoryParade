﻿using UnityEngine;

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

        /// <summary>
        /// Обеспечивает проходимость коридоров
        /// </summary>
        void HandleCollisions()
        {
            foreach (var wall in GameObject.FindGameObjectsWithTag("Wall"))
            {
                BoxCollider2D wallCollider = wall.GetComponent<BoxCollider2D>();
                if (wallCollider == null) continue;

                Collider2D[] overlaps = Physics2D.OverlapBoxAll(wallCollider.bounds.center, wallCollider.bounds.size, 0);
                foreach (var overlap in overlaps)
                {
                    if (overlap.CompareTag("Corridor"))
                    {
                        wallCollider.enabled = false;
                        break;
                    }
                }
            }
        }
    }

}
