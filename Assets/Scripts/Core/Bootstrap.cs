using UnityEngine;

namespace QuantumRealm.Core
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Scene Setup")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private bool autoSpawnOnStart = true;

        private void Start()
        {
            if (autoSpawnOnStart)
            {
                SpawnPlayer();
            }
        }

        public void SpawnPlayer()
        {
            if (playerPrefab == null)
            {
                Debug.LogWarning("Player prefab not assigned to Bootstrap.");
                return;
            }

            if (spawnPoint == null)
            {
                var root = new GameObject("PlayerSpawnPoint");
                root.transform.position = Vector3.zero;
                spawnPoint = root.transform;
            }

            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
