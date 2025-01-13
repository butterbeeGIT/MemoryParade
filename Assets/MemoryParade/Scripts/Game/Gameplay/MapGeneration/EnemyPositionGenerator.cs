using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static void SpawnEnemies(GameObject enemyPrefab, int count)
        {

        }


        /// <summary>
        /// Генерирует рандомную позицию для врага в комнате
        /// </summary>
        /// <param name="spawnRoom">комната где нужен враг</param>
        /// <param name="CellSize">размер поля на сцене</param>
        /// <returns>рандомная позиция</returns>
        public static Vector2 RandomPositionEnemyInRoom(Room spawnRoom, Vector2 CellSize)
        {
            if (spawnRoom != null)
            {
                int x, y;
                x = Random.Next(spawnRoom.X+1, spawnRoom.X + spawnRoom.Width-1);
                y = Random.Next(spawnRoom.Y + 1, spawnRoom.Y + spawnRoom.Height - 1);
                Vector2 position = new Vector2(x * CellSize.x, -y * CellSize.y);
                return position;
                //InstantiatePrefab(enemyPrefab, position, Quaternion.Euler(0, 0, 0));
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

        //private static bool HasCollisionsWithOthers()
        //{

        //}

        /// <summary>
        /// Удаляет всех врагов
        /// </summary>
        //void DestroyEnemy()
        //{
        //    foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy")) Destroy(obj);
        //}
    }
}
