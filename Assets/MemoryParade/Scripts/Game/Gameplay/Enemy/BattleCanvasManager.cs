using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.Enemy
{
    /// <summary>
    /// скрипт инициализации BattleCanvas
    /// </summary>
    public class BattleCanvasManager : MonoBehaviour
    {
        // Ссылка на BattleCanvas
        public static GameObject Instance { get; private set; }

        /// <summary>
        /// размер камеры для нужного приближения
        /// </summary>
        public const float orthographicSize = 1.533734f;

        void Awake()
        {
            if (Instance == null)
                Instance = gameObject;
            else
                Destroy(gameObject);
        }

        void Start()
        {
            Debug.Log("BattleCanvas is initialized.");
        }

        /// <summary>
        /// Меняет позицию переданного объекта в зависимости от того игрок это или враг
        /// </summary>
        /// <param name="obj"></param>
        public static void ChangePositionGameObject(GameObject obj)
        {
            //учет приближения камеры
            float cameraApp = orthographicSize / 3.5f;

            BoxCollider2D boxCollider = obj.GetComponent<BoxCollider2D>();
            float halfHeight = boxCollider.size.y / 2 * obj.transform.localScale.y;

            //выравнивает
            Vector3 shiftPositionPlayer = new Vector3(2.26f * cameraApp, -0.08f * cameraApp+ halfHeight, 0);
            Vector3 shiftPositionEnemy = new Vector3(-2.13f * cameraApp, -0.08f * cameraApp+ halfHeight, 0);

            if(obj.CompareTag("Player"))
                obj.transform.position = Instance.transform.position + shiftPositionPlayer;
            else if(obj.CompareTag("Enemy"))
                obj.transform.position = Instance.transform.position + shiftPositionEnemy;
        }

        /// <summary>
        /// Включает и выключает Canvas
        /// </summary>
        /// <param name="setActive"></param>
        public static void SetActive(bool setActive)
        {
            if (Instance != null)
            {
                Instance.SetActive(setActive);
                Debug.Log($"Canvas {Instance.name} set to {(setActive ? "active" : "inactive")}");
            }
            else
            {
                Debug.LogWarning("BattleCanvasManager Instance is null!");
            }
        }

    }
}
