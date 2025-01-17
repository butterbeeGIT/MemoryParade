using System.Collections;
using Assets.MemoryParade.Scripts.Game.Gameplay.Root;
using Assets.MemoryParade.Scripts.Game.GameRoot;
using Assets.MemoryParade.Scripts.Game.MainMenu.Root;
using Assets.MemoryParade.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using R3;
//using System.Linq;

namespace Assets.MemoryParade.Scripts.Game.GameRoot
{
    /// <summary>
    /// точка входа в игру
    /// </summary>
    public class EntryPointGame
    {

        private static EntryPointGame _instance;
        private Coroutines _coroutines;
        private UIRootView _uiRoot;
        //этот метод будет срабатывать в первую очередь после запуска
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void StartGame()
        {
            _instance = new EntryPointGame();
            _instance.RunGame();
        }

        private EntryPointGame()
        {

            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);//чтобы после загрузки пустой сцены он продолжал работать

            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);

        }


        private void RunGame()
        {
            //параметры входа в сцену по умолчанию
            EntryParamsGameplay entryGameplayParams = new EntryParamsGameplay(0, null, false);
            EntryParamsMainMenu entryMainMenuParams= new EntryParamsMainMenu(0,null);

            //на момент начала выполнения метода, программа не знает на какой сцене она находится 
            //Директива #if используется для проверки препроцессорного выражения. 
#if UNITY_EDITOR //работа в редакторе
            var sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == Scenes.GAMEPLAY)
            {
                //начнем загрузку этой сцены 
                _coroutines.StartCoroutine(LoadAndStartGameplay(entryGameplayParams));
                return;
            }
            if (sceneName == Scenes.MAIN_MENU)
            {
                //начнем загрузку этой сцены 
                _coroutines.StartCoroutine(LoadAndStartMainMenu(entryMainMenuParams));
                return;
            }
            if (sceneName != Scenes.BOOT)
            {
                return;
            }
#endif
            //непосредственно запуск игры 
            _coroutines.StartCoroutine(LoadAndStartGameplay(entryGameplayParams));

        }

        /// <summary>
        /// Корутина для старта геймплея 
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadAndStartGameplay(EntryParamsGameplay entryParams)
        {
            _uiRoot.ShowLoadingScreen();

            //визуал Unity
            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.LOBBY);
            //yield return LoadScene(Scenes.GAMEPLAY);
            //чтобы все успело загрузиться
            yield return new WaitForSeconds(2);
            
            //TODO создать DI контейнер 

            ////поиск по типу
            //var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            
            ////запуск сцены и сохранение наблюдателя за выходом из сцены
            //Observable<ExitParamsGameplay> monitorsGoToAnotherScene = sceneEntryPoint.RunGameplay(entryParams, _uiRoot);
            ////переход на другую сцену при измении наблюдаемого объекта. В меню
            //monitorsGoToAnotherScene.Subscribe(exitParamsGameplay =>
            //{
            //    _coroutines.StartCoroutine(LoadAndStartMainMenu(exitParamsGameplay.EntryParamsMainMenu));
            //});
            //спрятать загрузочный экран
            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartMainMenu(EntryParamsMainMenu entryParams)
        {
            _uiRoot.ShowLoadingScreen();

            //визуал Unity
            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAIN_MENU);
            //чтобы все успело загрузиться
            yield return new WaitForSeconds(2);

            ////TODO создать DI контейнер 

            ////поиск по типу
            //var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            ////запуск сцены и сохранение наблюдателя за выходом из сцены
            //Observable<ExitParamsMainMenu> monitorsGoToAnotherScene = sceneEntryPoint.Run(_uiRoot,entryParams);
            ////переход на другую сцену при измении наблюдаемого объекта. В геймплей
            //monitorsGoToAnotherScene.Subscribe(exitParamsMainMenu =>
            //{
            //    _coroutines.StartCoroutine(LoadAndStartGameplay(exitParamsMainMenu.EntryParamsGameplay));
            //});

            _uiRoot.HideLoadingScreen();
        }

        /// <summary>
        /// Корутина для загрузки сцены
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }

    
}

