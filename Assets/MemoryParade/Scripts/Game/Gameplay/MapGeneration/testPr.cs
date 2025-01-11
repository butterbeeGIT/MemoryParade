using System.Collections;
using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration
{
    public class testPr : MonoBehaviour
    {
        public GameObject prefab;
        // Use this for initialization
        void Start()
        {
            GameObject instance = UnityEngine.Object.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 90, 0));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}