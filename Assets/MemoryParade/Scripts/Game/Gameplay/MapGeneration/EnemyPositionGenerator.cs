using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration
{
    public class EnemyPositionGenerator
    {
        private static System.Random Random = new System.Random();

        /// <summary>
        /// Размещает врагов на карте в нужном количестве
        /// </summary>
        /// <param name="enemyPrefab">Префаб врага</param>
        /// <param name="count">количество на карте</param>
        public static void SpawnEnemies(GameObject enemyPrefab, int count, List<Room> rooms, Vector2 CellSize)
        {
            Vector2 positionEnemy;
            for (int i = 0; i < count; i++)
            {
                positionEnemy = RandomPositionEnemyInRoom(rooms, CellSize);
                while (HasCollisionsWithOthers(positionEnemy))
                {
                    positionEnemy = RandomPositionEnemyInRoom(rooms, CellSize);
                }
                InstantiatePrefab(enemyPrefab, positionEnemy, Quaternion.identity);
            }
        }


        /// <summary>
        /// Генерирует рандомную позицию для врага в рандомной комнате
        /// </summary>
        /// <param name="spawnRoom">комната где нужен враг</param>
        /// <param name="CellSize">размер поля на сцене</param>
        /// <returns>рандомная позиция</returns>
        public static Vector2 RandomPositionEnemyInRoom(List<Room> rooms, Vector2 CellSize)
        {
            Room spawnRoom = rooms[Random.Next(0, rooms.Count-1)];
            if (spawnRoom != null)
            {
                int x, y;
                x = Random.Next(spawnRoom.X+1, spawnRoom.X + spawnRoom.Width-1);
                y = Random.Next(spawnRoom.Y + 1, spawnRoom.Y + spawnRoom.Height - 1);
                Vector2 position = new Vector2(x * CellSize.x, -y * CellSize.y);
                return position;
            }
            return Vector2.zero;
        }

        /// <summary>
        /// Инстанцирует префаб и помещает его в сцену
        /// </summary>
        private static void InstantiatePrefab(GameObject prefab, Vector2 position, Quaternion rotation)
        {
            if (prefab == null)
            {
                Debug.LogWarning("Prefab is not assigned!");
                return;
            }

            GameObject instance = UnityEngine.Object.Instantiate(prefab, position, rotation);
            instance.name = prefab.name; 
        }

        /// <summary>
        /// Возвращает true если есть пересечения с другими объектами
        /// </summary>
        /// <returns></returns>
        private static bool HasCollisionsWithOthers(Vector2 position)
        {

            float radius = 0.5f;  // Радиус проверки (можно настроить)

            // Получаем все объекты в радиусе проверки
            Collider2D[] hits = Physics2D.OverlapCircleAll(position, radius);

            foreach (Collider2D hit in hits)
            {
                // Проверяем, является ли объект стеной (по тегу)
                if (hit.CompareTag("Enemy") || hit.CompareTag("Wall") || hit.CompareTag("Corridor") || hit.CompareTag("Player"))
                {
                    return true; // Пересечение со стеной
                }
            }

            return false; // Нет запрещённых пересечений
        }
    }
}
