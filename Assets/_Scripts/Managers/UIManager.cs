using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject levelEndScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Slider healthSlider;
    public List<Image> heartImages;

    private void Awake()
    {
        SetEnableGameOverScreen(false);
        SetEnableLevelEndScreen(false);
    }

    public void SetEnableGameOverScreen(bool enable)
    {
        gameOverScreen.SetActive(enable);
    }

    public void SetEnableLevelEndScreen(bool enable)
    {
        levelEndScreen.SetActive(enable);
    }

    public void UpdateHealthSlider(int health, int maxHealth)
    {
        healthSlider.value = (health / (float)maxHealth);
    }

    public void UpdatePlayerHealthUI(int health)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < health)
            {
                Color temp = heartImages[i].color;
                temp.a = 1;
                heartImages[i].color = temp;
            }
            else
            {
                Color temp = heartImages[i].color;
                temp.a = 0.2f;
                heartImages[i].color = temp;
            }
        }
    }

    public void DisplayAchievementUnlockMessage(int i)
    {
        AchievementManager achievementManager = AchievementManager.instance;
        if (achievementManager.IsAchievementUnlocked(i))
        {
            Debug.Log($"Achivement {achievementManager.achievementNames[i]} is already unlocked");
            return;
        }

        achievementManager.UnlockAchievement(i);
        MessageManager.Instance.DisplayMessage(
            $"Achievement Unlock: {achievementManager.achievementNames[i].ToUpper()}");
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
}