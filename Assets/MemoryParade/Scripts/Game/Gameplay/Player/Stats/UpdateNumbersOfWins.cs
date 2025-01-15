using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.Player
{
    public class UpdateNumbersOfWins: MonoBehaviour
    {
        private TextMeshProUGUI winsText;

        private void Awake()
        {
            // Получаем компонент TextMeshProUGUI, прикрепленный к объекту
            winsText = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            if (winsText != null)
            {
                winsText.text = PlayerСharacteristics.Instance.numberOfWins.ToString();
                Debug.Log($"Текст обновлен: {winsText.text}");
            }
            else
            {
                Debug.LogError("Компонент TextMeshProUGUI не найден!");
            }
        }
    }
}
