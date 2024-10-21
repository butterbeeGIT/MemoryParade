using System;
using Assets.MemoryParade.Scripts.Game.GameRoot;
using Assets.MemoryParade.Scripts.Game.MainMenu.Root.View;
using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.MainMenu.Root{
    public class MainMenuEntryPoint:MonoBehaviour
    {
        public event Action GoToGameplaySceneRequested;

       // [SerializeField] private GameObject _sceneRootBinder;

        [SerializeField] private UIMainMenuBinder _sceneUIRootPrefab;
        
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
            uiScene.GoToGameplayButtonClicked += () =>{
                GoToGameplaySceneRequested?.Invoke();
            };
        }
    }
}