//using UnityEngine;

//namespace Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration
//{ 
//    /// <summary>
//    /// обеспечивает проходимость коридоров
//    /// </summary>
//    public class CorridorWallHandler : MonoBehaviour
//    {
//        public void Start()
//        {
//            foreach (var wall in GameObject.FindGameObjectsWithTag("Wall"))
//            {
//                BoxCollider2D wallCollider = wall.GetComponent<BoxCollider2D>();
//                if (wallCollider == null) continue;

//                Collider2D[] overlaps = Physics2D.OverlapBoxAll(wallCollider.bounds.center, wallCollider.bounds.size, 0);
//                foreach (var overlap in overlaps)
//                {
//                    if (overlap.CompareTag("Corridor"))
//                    {
//                        wallCollider.enabled = false;
//                        break;
//                    }
//                }
//            }
//        }
//    }

//}
