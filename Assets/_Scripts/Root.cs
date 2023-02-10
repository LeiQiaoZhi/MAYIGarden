using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Root : Health
{

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public override void ChangeHealth(int change)
    {
        base.ChangeHealth(change);
        
        Debug.LogWarning("Root is being attacked");
        
        // being attacked animation
        
        // return damage
    }

    protected override void Die()
    {
        Debug.LogWarning("ROOT HAS DIED.");
        
        // game over
        _gameManager.GameOver();
    }
}
