using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public int levelIndex ;
    public Vector3 originalScale;
    public Vector3 hoverScale;
    public Sprite grownSprite;
    public Sprite youngSprite;
    
    private GameSceneManager _sceneManager;
    private MainMenuManager _mainMenuManager;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mainMenuManager = FindObjectOfType<MainMenuManager>();
        _sceneManager = FindObjectOfType<GameSceneManager>();
        
        // init 
        UpdateSprite();
    }

    private void OnEnable()
    {
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (LevelManager.instance.IsLevelUnlocked(levelIndex))
        {
            _spriteRenderer.sprite = grownSprite;
        }
        else
        {
            _spriteRenderer.sprite = youngSprite;
        } 
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
        AudioManager.Instance.PlaySound("Click");
        Camera.main.GetComponent<Animator>().SetTrigger($"MoveDown{levelIndex}");
        _mainMenuManager.MainMenuLeave();
    }
}
