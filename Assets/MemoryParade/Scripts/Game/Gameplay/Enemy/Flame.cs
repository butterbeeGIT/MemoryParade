using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.Enemy
{
    /// <summary>
    /// Класс слайма
    /// </summary>
    public class Flame : Enemy
    {
        private void Awake()
        {
            EnemyType = "Flame";
            MaxHealth = 30;
            Health = MaxHealth;
            Attack = 5;
            Speed = 3f;
            RewardPoints = 3;
        }

        public override void AttackPlayer(GameObject player)
        {
            Debug.Log($"{EnemyType} attacks player for {Attack} damage.");
            // TODO Логика атаки
        }
    }
}
