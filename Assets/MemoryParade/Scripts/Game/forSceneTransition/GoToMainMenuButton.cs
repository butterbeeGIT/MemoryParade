using UnityEngine;
using Assets.MemoryParade.Scripts.Game.GameRoot;

namespace Assets.MemoryParade.Scripts.Game.forSceneTransition
{
    public class GoToMainMenuButton : MonoBehaviour
    {
        public void OnClick()
        {
            Debug.Log("Нажата кнопка меню");
            SceneTransitionManager.Instance.GoToMenu();
        }
    }
}
