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
