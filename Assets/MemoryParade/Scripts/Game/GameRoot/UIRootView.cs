using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.GameRoot
{

    public class UIRootView : MonoBehaviour
    {

        [SerializeField] private GameObject _loadingScreen;

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
    }
}