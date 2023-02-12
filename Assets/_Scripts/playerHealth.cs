using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerHealth: Health
{
    public playerMovement movement_manager;
    //public int counterDamage = 2;
    private GameManager _gameManager;
    private float normalSpeed;
    public int rotPerSec = 240;
    public float dieRotTime = 3;
    public float immuneTime = 1;
    private bool immune = false;
    private UIManager _uiManager;
    public Animator playerSpriteAnimator;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    public override void ChangeHealth(int change, GameObject from)
    {
        if (immune)
        {
            return;
        }
        base.ChangeHealth(change, from);

        Debug.LogWarning("Player is being attacked");

        _uiManager.UpdatePlayerHealthUI(_currentHealth);
        
        // being attacked animation

        // deals counter damage
        if (change < 0)
        {
            //Debug.LogWarning($"Dealing counter damage to {from.name}");
            //Health health = from.GetComponent<Health>();
            //health.ChangeHealth(-counterDamage, gameObject);
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    protected override void Die()
    {
        Debug.LogWarning("PLAYER HAS DIED.");

        // stop player motion for 6 seconds, flashing, and continue moving after.
        normalSpeed = movement_manager.mySpeed;
        movement_manager.mySpeed= 0;
        immune= true;
        movement_manager.freezePos();
        playerSpriteAnimator.SetTrigger("Die");
        Debug.LogWarning("The player is immune now.");
        StartCoroutine(DieAndRotate(dieRotTime,normalSpeed));  
    }

    private IEnumerator DieAndRotate(float duration, float curr_speed)
    {
        // float t = 0.0f;
        // while(t < duration)
        // {
        //     t += Time.deltaTime;
        //     gameObject.transform.Rotate(new Vector3(0, 0, rotPerSec) * Time.deltaTime);
        //     yield return null;
        // }
        // Debug.Log(movement_manager.mySpeed);
        yield return new WaitForSeconds(immuneTime);
        movement_manager.mySpeed = curr_speed;
        _currentHealth = maxHealth;
        Debug.Log("Restored Player Health to " + maxHealth);
        ChangeHealth(0,gameObject);
        movement_manager.UnfreezePos();
        immune = false;
        Debug.LogWarning("The player is now normal");
    }

}