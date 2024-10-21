using System;
using Assets.MemoryParade.Scripts.Game.GameRoot;
using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.Root
{
    //Программаная точка входа 
    public class GameplayEntryPoint : MonoBehaviour
    {
        //тут будет связваться представление и логика модели

        public event Action GoToMainMenuSceneRequested;

       // [SerializeField] private GameObject _sceneRootBinder;

        [SerializeField] private UIGameplyBinder _sceneUIRootPrefab;
        
        //TODO должен передаваться DI контейнер 
        /// <summary>
        /// заходим на сцену
        /// </summary>
        /// <param name="uIRoot"></param>
        public void RunGameplay(UIRootView uIRoot)
        {
            //Debug.Log("Сцена геймплея загружена");
            //создаем UI
            var uiScene = Instantiate(_sceneUIRootPrefab);
            //прикрепляет uiScene к uiRoot
            uIRoot.AttachSceneUI(uiScene.gameObject);

            //подписка на событие клика
            uiScene.GoToMainMenuButtonClicked += () =>{
                GoToMainMenuSceneRequested?.Invoke();
            };
        }
    }
}