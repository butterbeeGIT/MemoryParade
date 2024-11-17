using System;
using Assets.MemoryParade.Scripts.Game.Gameplay.Root;
using Assets.MemoryParade.Scripts.Game.MainMenu.Root.View;
using Assets.MemoryParade.Scripts.Game.GameRoot;
using UnityEngine;
using R3;

namespace Assets.MemoryParade.Scripts.Game.MainMenu.Root
{
    /// <summary>
    /// входная точка на сцену главного меню
    /// </summary>
    public class MainMenuEntryPoint : MonoBehaviour
    {
        /// <summary>
        /// графическое представление в игре
        /// </summary>
        [SerializeField] private UIMainMenuBinder _sceneUIRootPrefab;

        //TODO должен передаваться DI контейнер 

        /// <summary>
        /// запуск сцены главного меню с возможностью перехода на другую сцену
        /// </summary>
        /// <param name="uiRoot">графическое представление</param>
        /// <param name="enterParams">параметры входа для главного меню</param>
        /// <returns>наблюдатель, котрый при изменении сигнала будет рассылать подписчикам экземпляр параметров выхода из меню</returns>
        public Observable<ExitParamsMainMenu> Run(UIRootView uiRoot, EntryParamsMainMenu enterParams)
        {
            //создать UI
            UIMainMenuBinder uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            //будет излучать сигнал выхода из главногом меню
            var exitSignalSubj = new Subject<Unit>();

            //закидываем его в UIMainMenuBinder чтобы начать отслеживать состояние
            uiScene.Bind(exitSignalSubj);

            Debug.Log($"MAIN MENU ENTRY POINT: Run main menu scene." +
                $" Проверка передачи параметра числа убитых врагов: {enterParams?.CountDeadEnemies}");


            int difficultyLevel = UnityEngine.Random.Range(0, 5);
            var gameplayEnterParams = new EntryParamsGameplay(difficultyLevel, null, true);
            var mainMenuExitParams = new ExitParamsMainMenu(gameplayEnterParams);

            //наблюдатель
            //оператор Select трансформирует входящий поток данных.
            //Каждое событие, поступающее в exitSignalSubj, преобразуется в объект mainMenuExitParams
            Observable<ExitParamsMainMenu> exitToGameplaySceneSignal = exitSignalSubj.Select(_ => mainMenuExitParams);
            //Например, если в exitSignalSubj произойдет событие (вызов OnNext), 
            //оно будет преобразовано в mainMenuExitParams и передано всем подписчикам нового Observable.
            return exitToGameplaySceneSignal;
        }
    }
}