using UnityEngine;

namespace QuantumRealm.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public int CurrentLevel { get; private set; } = 1;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void AdvanceLevel()
        {
            CurrentLevel++;
            Debug.Log($"Nível avançado: {CurrentLevel}");
        }
    }
}
