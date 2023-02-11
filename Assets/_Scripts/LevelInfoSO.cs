using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelInfoSO : ScriptableObject
{
    public List<Wave> waves;
    public List<Sprite> spritesForPlantStages;

    public Sprite GetSpriteForWave(int waveIndex)
    {
        int spriteIndex = waves[waveIndex].plantSpriteIndex;
        return spritesForPlantStages[spriteIndex];
    }

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
        public int plantSpriteIndex = 0;
    }
    
}
