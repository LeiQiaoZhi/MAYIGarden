using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [System.Serializable]
        public class EnemyInfo
        {
            public GameObject enemyPrefab;
            public int enemyNumber;
        }

        public List<EnemyInfo> enemies;
        public float averageSecondsBetweenSpawns;
    }
    
    public GameEvent gameOverEvent;

    public List<Wave> waves;

    public void GameOver()
    {
        gameOverEvent.Raise();
        
        // Pause Everything
        Time.timeScale = 0;
    }
}
