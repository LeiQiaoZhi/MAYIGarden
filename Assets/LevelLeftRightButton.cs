using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLeftRightButton : MonoBehaviour
{
    
    public int increment = 1;

    private MainMenuManager _mainMenuManager;


    private void Start()
    {
        _mainMenuManager = FindObjectOfType < MainMenuManager>();
    }

    private void OnMouseDown()
    {
        _mainMenuManager.ChangeLevelIndex(increment);
    }
}
