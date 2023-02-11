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
    public GameEvent gameOverEvent;
    public SpriteRenderer plantSpriteRender;

    [Header("Spawning")] public LevelInfoSO levelInfo;
    public List<Transform> spawnPoints;

    List<LevelInfoSO.Wave> _waves;

    private void Start()
    {
        _waves = levelInfo.waves;
        StartCoroutine(SpawnCoroutine());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator SpawnCoroutine()
    {
        for (int index = 0; index < _waves.Count; index++)
        {
            // new wave
            LevelInfoSO.Wave wave = _waves[index];
            
            // change plant sprite
            plantSpriteRender.sprite = levelInfo.GetSpriteForWave(index);
            
            // wait a few seconds before start spawning
            yield return new WaitForSeconds(wave.secondsBeforeSpawn);

            // spawn wave 
            Debug.LogWarning($"Spawning Wave {index}");

            // prepare all enemies to spawn in a queue
            List<GameObject> enemyList = new List<GameObject>();

            foreach (LevelInfoSO.Wave.EnemyInfo enemy in wave.enemies)
            {
                for (int i = 0; i < enemy.enemyNumber; i++)
                {
                    enemyList.Add(enemy.enemyPrefab);
                }
            }

            // shuffle 
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
                Instantiate(enemyList[i], spawnPoint);
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