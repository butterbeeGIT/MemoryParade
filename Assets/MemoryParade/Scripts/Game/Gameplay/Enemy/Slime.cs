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
    public class Slime : Enemy
    {
        private void Awake()
        {
            EnemyType = "Slime";
            MaxHealth = 20;
            Health = MaxHealth;
            Attack = 2;
            Speed = 2f;
            RewardPoints = 1;
        }

        public override void AttackPlayer(GameObject player)
        {
            Debug.Log($"{EnemyType} attacks player for {Attack} damage.");
            // TODO Логика атаки
        }
    }
}
