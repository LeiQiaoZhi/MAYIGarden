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
    [SerializeField] private float nutrientFallSpeed;

    List<LevelInfoSO.Wave> _waves;
    private bool spawnFinished = false;
    private int _enemyCount = 0;

    private void Start()
    {
        spawnFinished = false;
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
            
            // spawn nutrients
            foreach (var nutrient in wave.nutrients)
            {
                var nutrientComponent = Instantiate(nutrient.nutrientPrefab).GetComponent<Nutrient>();
                Vector2 targetPos = new Vector2(
                    UnityEngine.Random.Range(nutrient.spawnRangeBotLeft.x,nutrient.spawnRangeTopRight.x),
                    UnityEngine.Random.Range(nutrient.spawnRangeBotLeft.y,nutrient.spawnRangeTopRight.y)
                );
                nutrientComponent.SpawnFromSky(targetPos,fallSpeed:nutrientFallSpeed);
                Debug.LogWarning($"Spawning nutrient {nutrient.nutrientPrefab.name} to {targetPos}");
            }


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
                UpdateEnemyCount(1);
                Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
                Instantiate(enemyList[i], spawnPoint);
                yield return new WaitForSeconds(wave.averageSecondsBetweenSpawns);
            }
        }

        spawnFinished = true;
    }

    public void UpdateEnemyCount(int change)
    {
        _enemyCount += change;
        if (_enemyCount <= 0 && spawnFinished && FindObjectOfType<EnemyHealth>() == null)
        {
            LevelFinish();
        }
    }

    public void LevelFinish()
    {
        Debug.LogWarning("LEVEL FINISHED.");
        Animator animator = Camera.main.GetComponent<Animator>();
        if (animator)
        {
            animator.SetTrigger("LevelEnd");
        }
    }


    public void GameOver()
    {
        gameOverEvent.Raise();

        // Pause Everything
        Time.timeScale = 0;
    }

    private void OnDrawGizmos()
    {
        var levelInfoWaves = levelInfo.waves;
        for (int index = 0; index < levelInfoWaves.Count; index++)
        {
            // new wave
            LevelInfoSO.Wave wave = levelInfoWaves[index];

            Gizmos.color = Color.yellow;
            foreach (var nutrient in wave.nutrients)
            {
                Vector2 center = (nutrient.spawnRangeBotLeft + nutrient.spawnRangeTopRight) / 2;
                Vector2 diff = nutrient.spawnRangeTopRight - nutrient.spawnRangeBotLeft;
                Gizmos.DrawWireCube(center,diff);
            }
        }
    }
}