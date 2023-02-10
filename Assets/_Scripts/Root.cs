using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Root : Health
{

    public int counterDamage = 2;
    
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public override void ChangeHealth(int change, GameObject from)
    {
        base.ChangeHealth(change, from);
        
        Debug.LogWarning("Root is being attacked");
        
        // being attacked animation
        
        // deals counter damage
        Debug.LogWarning($"Dealing counter damage to {from.name}");
        Health health = from.GetComponent<Health>();
        health.ChangeHealth(-counterDamage, gameObject);
    }

    protected override void Die()
    {
        Debug.LogWarning("ROOT HAS DIED.");
        
        // game over
        _gameManager.GameOver();
    }
}
