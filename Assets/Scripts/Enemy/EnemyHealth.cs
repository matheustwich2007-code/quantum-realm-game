using System;
using UnityEngine;

namespace QuantumRealm.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        public static event Action OnEnemyKilled;

        [SerializeField] private int maxHealth = 100;
        private int currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            OnEnemyKilled?.Invoke();
            Destroy(gameObject);
        }
    }
}
