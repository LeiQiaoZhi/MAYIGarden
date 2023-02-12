using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameralAnimator : MonoBehaviour
{
    private UIManager _uiManager;

    private GameSceneManager _sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        _sceneManager = FindObjectOfType<GameSceneManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToLevel(int index)
    {
        _sceneManager.LoadScene(index);
    }

    public void LevelEndUI()
    {
        _uiManager.SetEnableLevelEndScreen(true);
    }
}
