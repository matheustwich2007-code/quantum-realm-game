using UnityEngine;
using TMPro;

namespace QuantumRealm.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI ammoText;
        [SerializeField] private TextMeshProUGUI objectiveText;
        [SerializeField] private TextMeshProUGUI xpText;

        private int currentHealth = 100;
        private int currentAmmo = 30;
        private int currentXP = 0;

        public void SetHealth(int value)
        {
            currentHealth = value;
            if (healthText != null)
            {
                healthText.text = $"HP: {currentHealth}";
            }
        }

        public void SetAmmo(int value)
        {
            currentAmmo = value;
            if (ammoText != null)
            {
                ammoText.text = $"Ammo: {currentAmmo}";
            }
        }

        public void SetObjective(string text)
        {
            if (objectiveText != null)
            {
                objectiveText.text = text;
            }
        }

        public void SetExperience(int value)
        {
            currentXP = value;
            if (xpText != null)
            {
                xpText.text = $"XP: {currentXP}";
            }
        }
    }
}
