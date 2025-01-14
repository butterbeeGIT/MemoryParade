using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.Enemy
{
    public class BattleCanvasManager : MonoBehaviour
    {
        // Ссылка на BattleCanvas
        public static GameObject Instance { get; private set; }

        void Awake()
        {
            // Убедитесь, что BattleCanvas только один (Singleton)
            if (Instance == null)
            {
                Instance = gameObject;
                DontDestroyOnLoad(gameObject); // Если нужно сохранить при смене сцен
            }
            else
            {
                Destroy(gameObject); // Уничтожить дублирующийся объект
            }
        }

        void Start()
        {
            // Здесь можно выполнять дополнительные действия с BattleCanvas
            Debug.Log("BattleCanvas is initialized.");
        }
    }
}
