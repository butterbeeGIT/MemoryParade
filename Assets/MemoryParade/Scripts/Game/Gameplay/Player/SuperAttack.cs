using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.Player
{
    public class SuperAttack : MonoBehaviour
    {
        private BattleSystem _battleSystem;
        private Button button;
        public Sprite sprite;
        private Animator player;
        private Sprite startSprite;
        public bool click = false;

        void Start()
        {
            _battleSystem = FindAnyObjectByType<BattleSystem>();
            button = GetComponent<Button>();
            player = GameObject.Find("testPerson_0").GetComponent<Animator>();
            startSprite = button.image.sprite;
        }

        void Update()
        {
            // После того, как нажали возвращаем в исходное состояние
            if (_battleSystem.powerAttackCount < 3)
            {
                button.image.sprite = startSprite;
            }
            // Обновляем кнопку когда функция становится доступной
            if (_battleSystem.powerAttackCount == 3)
            {
                button.image.sprite = sprite;
            }
            // Проверяем нажата ли кнопка, сбрасываем количество атак
            if (click)
            {
                _battleSystem.powerAttackCount = 0;
                click = false;
            }
        }

        public void OnClick()
        {
            if (_battleSystem.powerAttackCount >= 3)
            {
                click = true;
                _battleSystem.PlayerSuperAttack();
            }
        }
    }
}
