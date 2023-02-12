using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public Vector3 originalScale;
    public Vector3 hoverScale;
    private GameSceneManager _sceneManager;
    private MainMenuManager _mainMenuManager;

    private void Start()
    {
        _mainMenuManager = FindObjectOfType<MainMenuManager>();
        _sceneManager = FindObjectOfType<GameSceneManager>();
    }

    private void OnMouseEnter()
    {
        transform.localScale = hoverScale;
    }

    private void OnMouseExit()
    {
        transform.localScale = originalScale;
    }

    private void OnMouseDown()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("MoveDown");
        _mainMenuManager.MainMenuLeave();
    }
}
