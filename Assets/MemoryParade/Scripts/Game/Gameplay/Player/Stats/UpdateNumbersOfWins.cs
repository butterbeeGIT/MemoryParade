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
        private TextMeshProUGUI health;
        private TextMeshProUGUI attackPower;
        private void Awake()
        {
            // Получаем компонент TextMeshProUGUI, прикрепленный к объекту
            winsText = GetComponent<TextMeshProUGUI>();
            health = GameObject.Find("HealthCount").GetComponent<TextMeshProUGUI>();
            attackPower = GameObject.Find("AttackPower").GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            if (attackPower != null)
            {
                attackPower.text = PlayerСharacteristics.Instance.baseAttack.ToString();
            }
            if (health != null)
            {
                health.text = PlayerСharacteristics.Instance.healthPoints.ToString();
            }
            if (winsText != null)
            {
                winsText.text = PlayerСharacteristics.Instance.numberOfWins.ToString();
            }
            else
            {
                Debug.LogError("Компонент TextMeshProUGUI не найден!");
            }
        }
    }
}
