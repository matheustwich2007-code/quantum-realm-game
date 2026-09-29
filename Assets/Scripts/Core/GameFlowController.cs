using UnityEngine;

namespace QuantumRealm.Core
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    public class GameFlowController : MonoBehaviour
    {
        public static GameFlowController Instance { get; private set; }

        [SerializeField] private WaveSpawner waveSpawner;
        [SerializeField] private HUDController hud;
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject hudPanel;

        public GameState CurrentState { get; private set; } = GameState.MainMenu;
        public int CurrentWave { get; private set; } = 1;
        public int Score { get; private set; }
        public int XP { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            if (mainMenuPanel != null)
            {
                mainMenuPanel.SetActive(true);
            }

            if (hudPanel != null)
            {
                hudPanel.SetActive(false);
            }

            if (hud != null)
            {
                hud.SetWave(CurrentWave);
                hud.SetScore(Score);
                hud.SetExperience(XP);
            }
        }

        public void StartGame()
        {
            CurrentState = GameState.Playing;
            CurrentWave = 1;
            Score = 0;
            XP = 0;
            Time.timeScale = 1f;

            if (mainMenuPanel != null)
            {
                mainMenuPanel.SetActive(false);
            }

            if (hudPanel != null)
            {
                hudPanel.SetActive(true);
            }

            if (hud != null)
            {
                hud.SetWave(CurrentWave);
                hud.SetScore(Score);
                hud.SetExperience(XP);
            }

            if (waveSpawner != null)
            {
                waveSpawner.BeginWave(CurrentWave);
            }
        }

        public void AddScore(int amount)
        {
            Score += amount;
            XP += Mathf.RoundToInt(amount * 0.5f);

            if (hud != null)
            {
                hud.SetScore(Score);
                hud.SetExperience(XP);
            }
        }

        public void AdvanceWave()
        {
            CurrentWave++;
            Score += 100;
            XP += 25;

            if (hud != null)
            {
                hud.SetWave(CurrentWave);
                hud.SetScore(Score);
                hud.SetExperience(XP);
            }

            if (waveSpawner != null)
            {
                waveSpawner.BeginWave(CurrentWave);
            }
        }

        public void GameOver()
        {
            CurrentState = GameState.GameOver;
            Time.timeScale = 0f;

            if (mainMenuPanel != null)
            {
                mainMenuPanel.SetActive(true);
            }

            if (hudPanel != null)
            {
                hudPanel.SetActive(false);
            }
        }

        public void TogglePause()
        {
            if (CurrentState == GameState.Playing)
            {
                CurrentState = GameState.Paused;
                Time.timeScale = 0f;
            }
            else if (CurrentState == GameState.Paused)
            {
                CurrentState = GameState.Playing;
                Time.timeScale = 1f;
            }
        }
    }
}
