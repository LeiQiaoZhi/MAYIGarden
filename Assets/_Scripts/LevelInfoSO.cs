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

        [System.Serializable]
        public class NutrientInfo
        {
            public GameObject nutrientPrefab;
            [Tooltip("Bot left of screen is (-16,-10)")]
            public Vector2 spawnRangeBotLeft;
            [Tooltip("Top right of screen is (16, 6)")]
            public Vector2 spawnRangeTopRight;
        }

        public List<EnemyInfo> enemies;
        [Tooltip("Normally just spawn 0 or 1 nutrient, before a wave starts spawning")]
        public List<NutrientInfo> nutrients;
        
        public float averageSecondsBetweenSpawns;
        public float secondsBeforeSpawn;
        public int plantSpriteIndex = 0;
    }
}