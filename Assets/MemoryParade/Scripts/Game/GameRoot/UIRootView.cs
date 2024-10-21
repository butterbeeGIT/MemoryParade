using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.GameRoot
{

    public class UIRootView : MonoBehaviour
    {
        /// <summary>
        /// SerializeField нужны чтобы переменные оставалить приватными, 
        /// но их можно было изменять в инспекторе Unity
        /// </summary>
        [SerializeField] private GameObject _loadingScreen;
        /// <summary>
        /// Ссылка на контейнер
        /// </summary>
        [SerializeField] private Transform _uiSceneContainer;

        private void Awake()
        {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }

        public void HideLoadingScreen()
        { //спрятать окно
            _loadingScreen.SetActive(false);
        }

        /// <summary>
        /// прикрепляет UI сцены
        /// </summary>
        public void AttachSceneUI(GameObject SceneUI){
            ClearSceneUI();
            //устанавливает родителя и говорит пересчитать координаты в соотвествии с ним
            SceneUI.transform.SetParent(_uiSceneContainer, false);
        }

        private void ClearSceneUI(){
            var childCount = _uiSceneContainer.childCount;
            for (int i = 0;i<childCount; i++){
                //.gameObject - потому что не дает уничтожать Transform 
                Destroy(_uiSceneContainer.GetChild(i).gameObject);
            }
        }
    }
}