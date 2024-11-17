using System;
using UnityEngine;
using R3;

namespace Assets.MemoryParade.Scripts.Game.MainMenu.Root.View
{
    /// <summary>
    /// работает с действиями из интерфеса
    /// меняет состояние сигнала для выхода из главного меню
    /// </summary>
    public class UIMainMenuBinder : MonoBehaviour
    {
        /// <summary>
        /// сигнал о выходе из сцены, 
        /// который уведомляет наблюдателей о своем изменении
        /// </summary>
        private Subject<Unit> _exitSceneSignalSubj;

        /// <summary>
        /// управление нажатием кнопки перехода к геймплею
        /// </summary>
        public void HandleGoToGameplayButtonClick()
        {
            //если сигнал сработал - уведомить остальных
            Debug.Log("я нажата, и работаю");
            _exitSceneSignalSubj?.OnNext(Unit.Default);
        }

        /// <summary>
        /// инициализация сигнала о выходе из сцены
        /// </summary>
        /// <param name="exitSceneSignalSubj" сигнал создается во время запуска сцены меню></param>
        public void Bind(Subject<Unit> exitSceneSignalSubj)
        {
            _exitSceneSignalSubj = exitSceneSignalSubj;
        }
    }
}