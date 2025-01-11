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
        /// Добавляет врага в комнату
        /// </summary>
        /// <param name="enemyPrefab"></param>
        /// <param name="spawnRoom"></param>
        public static void AddEnemy(GameObject enemyPrefab, Room spawnRoom, Vector2 CellSize)
        {
            if (spawnRoom != null)
            {
                int x, y;
                x = Random.Next(spawnRoom.X+1, spawnRoom.X + spawnRoom.Width-1);
                y = Random.Next(spawnRoom.Y + 1, spawnRoom.Y + spawnRoom.Height - 1);
                Vector3 position = new Vector3(x * CellSize.x, -y * CellSize.y, 0);
                InstantiatePrefab(enemyPrefab, position, Quaternion.Euler(0, 0, 0));
            }
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

            GameObject instance = UnityEngine.Object.Instantiate(prefab, position, rotation);
            instance.name = prefab.name; 
        }
    }
}
