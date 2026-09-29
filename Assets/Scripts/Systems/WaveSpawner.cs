using System.Collections;
using UnityEngine;

namespace QuantumRealm.Systems
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float spawnInterval = 1.1f;
        [SerializeField] private int baseEnemiesPerWave = 5;

        private Coroutine currentWaveRoutine;
        private int remainingEnemies;

        private void OnEnable()
        {
            Enemy.EnemyHealth.OnEnemyKilled += HandleEnemyKilled;
        }

        private void OnDisable()
        {
            Enemy.EnemyHealth.OnEnemyKilled -= HandleEnemyKilled;
        }

        public void BeginWave(int waveNumber)
        {
            if (enemyPrefab == null)
            {
                Debug.LogWarning("Enemy prefab not assigned to WaveSpawner.");
                return;
            }

            if (currentWaveRoutine != null)
            {
                StopCoroutine(currentWaveRoutine);
            }

            remainingEnemies = Mathf.Max(baseEnemiesPerWave, waveNumber * 3);
            currentWaveRoutine = StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            int spawned = 0;
            int totalToSpawn = remainingEnemies;

            while (spawned < totalToSpawn)
            {
                SpawnEnemy();
                spawned++;
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnEnemy()
        {
            Vector3 position = GetSpawnPosition();
            Instantiate(enemyPrefab, position, Quaternion.identity);
        }

        private Vector3 GetSpawnPosition()
        {
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                return randomPoint.position;
            }

            return transform.position + Random.onUnitSphere * 12f;
        }

        private void HandleEnemyKilled()
        {
            if (remainingEnemies > 0)
            {
                remainingEnemies--;
            }

            if (remainingEnemies <= 0 && GameFlowController.Instance != null)
            {
                GameFlowController.Instance.AdvanceWave();
            }
        }
    }
}
