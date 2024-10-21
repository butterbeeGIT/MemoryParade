using System;
using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.MainMenu.Root.View
{ 
    public class UIMainMenuBinder : MonoBehaviour 
    {
        public event Action GoToGameplayButtonClicked;

        public void HandleGoToGameplayButtonClicked(){
            GoToGameplayButtonClicked?.Invoke();
        }
    }
}