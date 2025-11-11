using OriginL.EnemySpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OriginL {
    public class EnemySpawner : MonoBehaviour, IIntractable {
        public List<GameObject> enemyPrefabs; 
        public List<Transform> spawnPoints; 

        public static EnemySpawner instance;

        public string message;

        public static Action<EnemySpawner> OnUIInteracted;
            
        private int currentLevel = 1;    
        private int enemiesToSpawn = 2;
        private int enemiesRemaining;

        private bool isSpawning = false;    

        private void Awake() {
            if(instance != null) {
                Destroy(gameObject);
            } else {
                instance = this;
            }
        }

        public void OnEnable() {
            EnemyStat.OnDeath += OnEnemyDeath;
        }

        public void OnDisable() {
            EnemyStat.OnDeath -= OnEnemyDeath;
        }


        public void StartSpawning() {
            if(!isSpawning) {
                isSpawning = true;
                StartCoroutine(SpawnEnemies());
            }
        }


        public void StopSpawning() {
            if(isSpawning) {
                isSpawning = false;
                StopCoroutine(SpawnEnemies());
            }
        }


        private IEnumerator SpawnEnemies() {
            while(isSpawning) {
                if(enemiesRemaining < enemiesToSpawn) {

                    int randomEnemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Count);
                    int randomSpawnIndex = UnityEngine.Random.Range(0, spawnPoints.Count);

                    GameObject enemy = Instantiate(enemyPrefabs[randomEnemyIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
                    enemiesRemaining++;


                    yield return new WaitForSeconds(1f);
                } else {

                    yield return new WaitForSeconds(2f);
                    if(enemiesRemaining == 0) {
                        IncreaseLevel();
                    }
                }
            }
        }


        private void IncreaseLevel() {
            currentLevel++;
            enemiesToSpawn = Mathf.Min(10, currentLevel * 5); 
            enemiesRemaining = 0; 

            Debug.Log($"Level {currentLevel} started! Spawning {enemiesToSpawn} enemies.");
        }


        public void OnEnemyDeath() {
            enemiesRemaining--;
        }

        #region Interactable

        public void OnIntract() {
            StartSpawning();
        }

        public string Message() {
            return message;
        }

        public GameObject GetGameObject() {
            return this.gameObject;
        }

        #endregion
    }
}
