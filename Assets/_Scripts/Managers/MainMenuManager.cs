using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    [SerializeField] Button levelsButton;
    public List<CanvasGroup> achievementCanvasGroups;
    public List<GameObject> views;
    public TextMeshProUGUI deathCountTxt;
    public List<GameObject> levelViews;

    private int _currentLevelIndex = 0;

    [Header("Sound")]
    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle soundFxToggle;
    [SerializeField] float defaultMusicVol = -15f;
    [SerializeField] float defaultSoundFxVol;
    [SerializeField] AudioMixerGroup musicOutput;
    [SerializeField] AudioMixerGroup soundEffectOutput;

    private void Start()
    {
        // default view is levels
        levelsButton.Select();
        ShowView(0);
        
        // select a level view
        ChangeLevelIndex(0);
        
        // default music and sound vol
        SetMusicVol(defaultMusicVol);
        SetSoundFxVol(defaultSoundFxVol);
        
        // Lock Levels
        SetLevelButtonsLock();
        
        // achievements
        SetAchievementsLock();
        UpdateDeathCount();
        
        //  bgm
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic("MenuBGM");
        
    }



    public void ChangeLevelIndex(int increment)
    {
        _currentLevelIndex = (_currentLevelIndex + increment + levelViews.Count) % levelViews.Count;
        for (int i = 0; i < levelViews.Count; i++)
        {
            if (_currentLevelIndex == i)
            {
                levelViews[i].SetActive(true);
            }
            else
            {
                levelViews[i].SetActive(false);
            }
        }
    }

    public void ClickSound()
    {
        AudioManager.Instance.PlaySound("Click");
    }

    public void MainMenuLeave()
    {
        mainMenu.GetComponent<Animator>().SetTrigger("Leave");
    }

    public void UpdateDeathCount()
    {
        deathCountTxt.text = "Deaths: " + AchievementManager.instance.GetDeathCount().ToString();
    }

    public void SetMusicVol(float volume)
    {
       musicOutput.audioMixer.SetFloat("musicVol", volume);
    }

    public void SetSoundFxVol(float volume)
    {
       soundEffectOutput.audioMixer.SetFloat("soundFxVol", volume);
    }

    public void ToggleMusic()
    {
        if (musicToggle.isOn)
        {
            SetMusicVol(defaultMusicVol);
        }
        else
        {
            SetMusicVol(-80);
        }
        AudioManager.Instance.PlaySound("Click");
    }

    public void ToggleSoundFx()
    {
        if (soundFxToggle.isOn)
        {
            SetSoundFxVol(defaultSoundFxVol);
        }
        else
        {
            SetSoundFxVol(-80);
        }
        AudioManager.Instance.PlaySound("Click");
    }
    

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }


    public void ShowView(int index)
    {
        foreach(GameObject view in views)
        {
            view.SetActive(false);
        }
        views[index].SetActive(true);
    }



    public void SetLevelButtonsLock()
    {
        // for(int i = 0; i<buttons.Count; i++)
        // {
        //     bool unlocked = LevelManager.instance.IsLevelUnlocked(i);
        //     buttons[i].interactable = unlocked;
        // }
    }

    public void SetAchievementsLock()
    {
        for (int i = 0; i < achievementCanvasGroups.Count; i++)
        {
            bool unlocked = AchievementManager.instance.IsAchievementUnlocked(i);
            Debug.Log($"achievement {i} is unlocked: {unlocked}");
            achievementCanvasGroups[i].alpha = unlocked ? 1 : 0.5f;
        }
    }
}
