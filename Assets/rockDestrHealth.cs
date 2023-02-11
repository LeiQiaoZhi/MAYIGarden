using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rockDestrHealth : Health
{
    //public int counterDamage = 2;
    private GameManager _gameManager;
    public Sprite halfDestructedRock;
    public SpriteRenderer spriteRenderer;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public override void ChangeHealth(int change, GameObject from)
    {
        base.ChangeHealth(change, from);

        Debug.LogWarning("The Rock Is Being Attacked");
        if (_currentHealth <= maxHealth / 2)
        {
            changeSprite(halfDestructedRock);
        }
         
        
    }
    protected override void Die()
    {
        Debug.LogWarning("The Rock Is Destructed.");
        Destroy(gameObject);
    }

    private void changeSprite(Sprite targetSprite)
    {
        spriteRenderer.sprite = targetSprite;
    }
}