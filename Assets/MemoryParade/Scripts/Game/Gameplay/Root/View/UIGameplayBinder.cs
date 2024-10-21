using System;
using UnityEngine;

public class UIGameplyBinder : MonoBehaviour
{
    public event Action GoToMainMenuButtonClicked;

    /// <summary>
    /// если кликнули, запускаем событие GoToMainMenuButtonClicked
    /// </summary>
    public void HandleGoToMainMenuButtonClicked(){
        GoToMainMenuButtonClicked?.Invoke();
    }
}
