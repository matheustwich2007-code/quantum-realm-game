using TMPro;
using UnityEngine;

namespace QuantumRealm.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI ammoText;
        [SerializeField] private TextMeshProUGUI objectiveText;
        [SerializeField] private TextMeshProUGUI xpText;
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void SetHealth(int value)
        {
            if (healthText != null)
            {
                healthText.text = $"HP: {value}";
            }
        }

        public void SetAmmo(int value)
        {
            if (ammoText != null)
            {
                ammoText.text = $"Ammo: {value}";
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
            if (xpText != null)
            {
                xpText.text = $"XP: {value}";
            }
        }

        public void SetWave(int value)
        {
            if (waveText != null)
            {
                waveText.text = $"Wave: {value}";
            }
        }

        public void SetScore(int value)
        {
            if (scoreText != null)
            {
                scoreText.text = $"Score: {value}";
            }
        }
    }
}
