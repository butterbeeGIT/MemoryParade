using System;
using R3;
using UnityEngine;

/// <summary>
/// сюда потом будет передаваться view модель
/// пока принимает состояние единственной кнопки и 
/// если ее состояние меняется, то передает его всем подписчикам
/// </summary>
public class UIGameplyBinder : MonoBehaviour
{
    /// <summary>
    /// подкласс Observable, может уведомлять о своем изменении всех кто подписан
    /// </summary>
    private R3.Subject<R3.Unit> _exitSceneSignalSubj;

    /// <summary>
    /// Уведомляет подписчиков о событии нажатия на кнопку, 
    /// если состояние кнопки изменилось
    /// </summary>
    public void HandleGoToMainMenuButtonClicked(){
        //OnNext(value) – Отправляет новое значение всем подписчикам. 
        Debug.Log("я нажата, но нихера не работаю");
        _exitSceneSignalSubj?.OnNext(Unit.Default);
    }

    /// <summary>
    /// Связать. Принимает состояние кнопки перехода
    /// </summary>
    /// <param name="exitSceneSignal"></param>
    public void Bind(R3.Subject<R3.Unit> exitSceneSignal){
        Debug.Log("присоединение выполнено");
        _exitSceneSignalSubj = exitSceneSignal;     
    }
}
