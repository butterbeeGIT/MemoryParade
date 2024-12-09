using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.MemoryParade.Scripts.Game.GameRoot
{
    /// <summary>
    /// Управляет переходами между сценами
    /// </summary>
    public class SceneTransitionManager : MonoBehaviour
    {

        private static SceneTransitionManager _instance;
        public static SceneTransitionManager Instance => _instance;

        /// <summary>
        /// Название предыдущей сцены
        /// </summary>
        private string previousSceneName;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Переход на новую сцену с сохранением предыдущей.
        /// </summary>
        /// <param name="sceneName">Имя новой сцены</param>
        public void GoToScene(string sceneName)
        {
            previousSceneName = SceneManager.GetActiveScene().name;
            StartCoroutine(LoadSceneWithLoadingScreen(sceneName));
        }

        /// <summary>
        /// Переход в меню с сохранением предыдущей сцены.
        /// </summary>
        public void GoToMenu()
        {
            Debug.Log("переход");
            previousSceneName = SceneManager.GetActiveScene().name; // Сохраняем текущую сцену
            StartCoroutine(LoadSceneWithLoadingScreen(Scenes.MAIN_MENU));
        }

        /// <summary>
        /// Возврат на предыдущую сцену.
        /// </summary>
        public void ReturnToPreviousScene()
        {
            if (!string.IsNullOrEmpty(previousSceneName))
            {
                string sceneToLoad = previousSceneName;
                //сохранение текущей сцены
                previousSceneName = SceneManager.GetActiveScene().name;
                StartCoroutine(LoadSceneWithLoadingScreen(sceneToLoad));
            }
            else
            {
                Debug.LogWarning("Предыдущая сцена не установлена.");
            }
        }

        /// <summary>
        /// Корутина для загрузки сцены с экраном загрузки.
        /// </summary>
        /// <param name="sceneName">Имя сцены для загрузки</param>
        private IEnumerator LoadSceneWithLoadingScreen(string sceneName)
        {
            // Показать экран загрузки
            // UIManager.Instance.ShowLoadingScreen();

            // Загрузить сцену асинхронно
            Debug.Log("Загрузка сцены " + sceneName);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                //TODO обновление загрузки
                yield return null;
            }

            // Скрыть экран загрузки
            // UIManager.Instance.HideLoadingScreen();
        }
    }
}