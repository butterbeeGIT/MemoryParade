using UnityEngine;
using Assets.MemoryParade.Scripts.Game.GameRoot;
using System.Collections;

namespace Assets.MemoryParade.Scripts.Game.forSceneTransition
{
    public class ToGameplay : MonoBehaviour
    {
        public void OnClick()
        {
            SceneTransitionManager.Instance.GoToScene(Scenes.GAMEPLAY);
        }
    }
}

