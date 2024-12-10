using UnityEngine;
using Assets.MemoryParade.Scripts.Game.GameRoot;

namespace Assets.MemoryParade.Scripts.Game.forSceneTransition
{
    public class ReturnToPreviousSceneButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneTransitionManager.Instance.ReturnToPreviousScene();
        }
    }
}