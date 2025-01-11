using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.MemoryParade.Scripts.Game.Gameplay.Enemy
{
    /// <summary>
    /// общий класс врага
    /// </summary>
    public abstract class Enemy : MonoBehaviour
    {
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Attack { get; protected set; }
        public float Speed { get; protected set; }
        public String EnemyType { get; protected set; }
        /// <summary>
        /// Очки за убийство
        /// </summary>
        public int RewardPoints { get; protected set; }

        /// <summary>
        /// нанесение урона
        /// </summary>
        /// <param name="damage"></param>
        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// атакует игрока
        /// </summary>
        /// <param name="player"></param>
        public abstract void AttackPlayer(GameObject player);

        public virtual void Move(Vector3 targetPosition)
        {
            //TODO Базовая логика передвижения
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        }

        /// <summary>
        /// смерть врага
        /// </summary>
        protected virtual void Die()
        {
            Debug.Log($"{EnemyType} died");
            // TODO Добавить логику уничтожения объекта, начисления очков и т.д.
            Destroy(gameObject);
        }
    }
}
