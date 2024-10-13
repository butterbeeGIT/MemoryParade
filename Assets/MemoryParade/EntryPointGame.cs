using System.Collections;
using Assets.MemoryParade.Scripts.Game.Gameplay.Root;
using Assets.MemoryParade.Scripts.Game.GameRoot;
using Assets.MemoryParade.Scripts.Utils;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.Linq;

namespace Assets.MemoryParade
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
            //на момент начала выполнения метода, программа не знает на какой сцене она находится 
            //Директива #if используется для проверки препроцессорного выражения. 
#if UNITY_EDITOR //работа в редакторе
            var sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == Scenes.GAMEPLAY)
            {
                //начнем загрузку этой сцены 
                _coroutines.StartCoroutine(LoadAndStartGameplay());
                return;
            }
            if (sceneName != Scenes.BOOT)
            {
                return;
            }
#endif
            //непосредственно запуск игры 
            _coroutines.StartCoroutine(LoadAndStartGameplay());

        }

        /// <summary>
        /// Корутина для старта геймплея 
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadAndStartGameplay()
        {
            _uiRoot.ShowLoadingScreen();

            //визуал Unity
            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);
            //чтобы все успело загрузиться
            yield return new WaitForSeconds(2);
            
            //TODO создать DI контейнер 

            //поиск по типу
            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            sceneEntryPoint.RunGameplay();
            
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

