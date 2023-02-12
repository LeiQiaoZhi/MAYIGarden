using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.SceneManagement;


// SINGLETON
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public List<int> levelsUnlockedAtTheStart;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // foreach (var t in levelsUnlockedAtTheStart)
        // {
        //     UnlockLevel(t);
        // }
    }
    public void ResetLevels()
    {
        for (int i = 1; i <= 3; i++)
        {
            LockLevel(i);
        } 
    }

    public void LockLevel(int i)
    {
        Debug.Log($"Level {i} is locked");
        PlayerPrefs.SetInt($"level{i}", 0);
    }
    public void UnlockLevel(int i)
    {
        Debug.Log($"Level {i} is unlocked");
        PlayerPrefs.SetInt($"level{i}", 1);
    }

    public bool IsLevelUnlocked(int i)
    {
        int unlocked = PlayerPrefs.GetInt($"level{i}", 0);
        Debug.Log($"level {i} is unlocked: {unlocked}");
        return unlocked == 1 ? true : false;
    }
}