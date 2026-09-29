using UnityEngine;

namespace QuantumRealm.Systems
{
    public class SaveSystem : MonoBehaviour
    {
        public void SaveProgress(int level, int xp, int skillPoints)
        {
            PlayerPrefs.SetInt("player_level", level);
            PlayerPrefs.SetInt("player_xp", xp);
            PlayerPrefs.SetInt("player_skill_points", skillPoints);
            PlayerPrefs.Save();
            Debug.Log("Progress saved successfully.");
        }

        public (int level, int xp, int skillPoints) LoadProgress()
        {
            int level = PlayerPrefs.GetInt("player_level", 1);
            int xp = PlayerPrefs.GetInt("player_xp", 0);
            int skillPoints = PlayerPrefs.GetInt("player_skill_points", 0);
            return (level, xp, skillPoints);
        }
    }
}
