using UnityEngine;

namespace QuantumRealm.Systems
{
    public class PowerUpSystem : MonoBehaviour
    {
        [SerializeField] private float healAmount = 25f;
        [SerializeField] private float speedBoost = 1.5f;
        [SerializeField] private float duration = 5f;

        public void ApplyHeal(GameObject target)
        {
            var health = target.GetComponent<HealthController>();
            if (health != null)
            {
                health.Heal(healAmount);
            }
        }

        public void ApplySpeedBoost(GameObject target)
        {
            var controller = target.GetComponent<UnityEngine.CharacterController>();
            if (controller != null)
            {
                Debug.Log("Speed boost activated for " + target.name);
            }
        }
    }

    public class HealthController : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        private float currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void Heal(float amount)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        }

        public void Damage(float amount)
        {
            currentHealth = Mathf.Max(0, currentHealth - amount);
            if (currentHealth <= 0f)
            {
                Debug.Log("Player defeated");
            }
        }
    }
}
