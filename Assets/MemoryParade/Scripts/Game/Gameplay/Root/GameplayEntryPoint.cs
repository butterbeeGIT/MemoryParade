using System;
using Assets.MemoryParade.Scripts.Game.GameRoot;
using Assets.MemoryParade.Scripts.Game.MainMenu.Root;
using R3;
using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.Root
{
    //Программаная точка входа 
    public class GameplayEntryPoint : MonoBehaviour
    {
        //тут будет связваться представление и логика модели

        [SerializeField] private UIGameplyBinder _sceneUIRootPrefab;
        
        //TODO должен передаваться DI контейнер 

        /// <summary>
        /// заходим на сцену
        /// </summary>
        /// <param name="entryParams">входные параметры сцены</param>
        /// <param name="uIRoot">представление</param>
        /// <returns>событие, которое возвращает параметры выхода.
        /// Сигнализирует о том, что кто-то хочет выйти из этой сцены</returns>
        public Observable<ExitParamsGameplay> RunGameplay(EntryParamsGameplay entryParams, UIRootView uIRoot)
        {
            //создаем UI
            UIGameplyBinder uiScene = Instantiate(_sceneUIRootPrefab);
            //прикрепляет uiScene к uiRoot
            uIRoot.AttachSceneUI(uiScene.gameObject);

            //сейчас на сигнал влияет UI
            R3.Subject<Unit> exitSceneSignalSubj = new();
            uiScene.Bind(exitSceneSignalSubj);

            ExitParamsGameplay exitParams = new ExitParamsGameplay(new EntryParamsMainMenu(100, null));
            //преобразование Unit к нужному типу 
            Observable<ExitParamsGameplay> exitSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
            return exitSceneSignal;
        }
    }
}