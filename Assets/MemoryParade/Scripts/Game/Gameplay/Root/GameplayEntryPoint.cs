using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.Root
{

    //Программаная точка входа 
    public class GameplayEntryPoint : MonoBehaviour
    {
        //тут будет связваться представление и логика модели
        [SerializeField] private GameObject _sceneRootBinder;
        
        public void RunGameplay()
        {
            Debug.Log("Сцена геймплея загружена");
        }
    }
}