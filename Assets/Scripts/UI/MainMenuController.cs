using UnityEngine;

namespace QuantumRealm.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameFlowController gameFlow;

        public void StartGame()
        {
            if (gameFlow != null)
            {
                gameFlow.StartGame();
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
