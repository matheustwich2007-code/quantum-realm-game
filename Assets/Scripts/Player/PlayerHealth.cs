using System;
using UnityEngine;
using QuantumRealm.UI;

namespace QuantumRealm.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public event Action<float, float> OnHealthChanged;

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private HUDController hud;

        private float currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        private void Start()
        {
            RefreshHud();
        }

        public void Damage(float amount)
        {
            currentHealth = Mathf.Max(0f, currentHealth - amount);
            RefreshHud();

            if (currentHealth <= 0f)
            {
                if (QuantumRealm.Core.GameFlowController.Instance != null)
                {
                    QuantumRealm.Core.GameFlowController.Instance.GameOver();
                }
            }
        }

        public void Heal(float amount)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
            RefreshHud();
        }

        public float GetRatio()
        {
            return currentHealth / maxHealth;
        }

        private void RefreshHud()
        {
            if (hud != null)
            {
                hud.SetHealth(Mathf.RoundToInt(currentHealth));
            }

            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }
}
