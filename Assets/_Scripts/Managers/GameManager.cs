using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// manages time,
/// controls game over behaviour,
/// spawns enemies using WaveSpawner
/// </summary>
public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName = "Wave";
        [System.Serializable]
        public class EnemyInfo
        {
            public GameObject enemyPrefab;
            public int enemyNumber;
        }

        public List<EnemyInfo> enemies;
        public float averageSecondsBetweenSpawns;
        public float secondsBeforeSpawn;
    }
    
    public GameEvent gameOverEvent;

    [Header("Spawning")]
    public List<Wave> waves;
    public List<Transform> spawnPoints;

    private int _currentWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        for (int index = 0; index < waves.Count; index++)
        {
            Wave wave = waves[index];
            yield return new WaitForSeconds(wave.secondsBeforeSpawn);
            
            // spawn wave i
            Debug.LogWarning($"Spawning Wave {index}");
            
            // prepare all enemies to spawn in a queue
            List<GameObject> enemyList = new List<GameObject>();

            foreach (Wave.EnemyInfo enemy in wave.enemies)
            {
                for (int i = 0; i < enemy.enemyNumber; i++)
                {
                    enemyList.Add(enemy.enemyPrefab);
                }
            }

            System.Random rng = new System.Random();
            int n = enemyList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (enemyList[k], enemyList[n]) = (enemyList[n], enemyList[k]);
            }

            // periodically spawn an enemy
            for (int i = 0; i < enemyList.Count; i++)
            {
                Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
                Instantiate(enemyList[i],spawnPoint);
                yield return new WaitForSeconds(wave.averageSecondsBetweenSpawns);
            }
        }
    }


    public void GameOver()
    {
        gameOverEvent.Raise();
        
        // Pause Everything
        Time.timeScale = 0;
    }
}
