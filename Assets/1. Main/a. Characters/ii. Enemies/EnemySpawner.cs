using OriginL.Player;
using System.Collections.Generic;
using UnityEngine;

namespace OriginL
{
    public class EnemySpawner : MonoBehaviour {
        public List<GameObject> enemyPrefabs; // Array of enemy prefabs
        public List<Transform> spawnPoint; // The position where enemies will spawn

        public static EnemySpawner instance;

        private void Awake() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }else
                Destroy(gameObject);
        }

        public void SpawnRandomEnemy() {
            int randomIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject randomEnemy = enemyPrefabs[randomIndex];

            // Spawn the selected enemy at the spawn point
            Instantiate(randomEnemy, spawnPoint[0].position, Quaternion.identity);
        }

    }
}
