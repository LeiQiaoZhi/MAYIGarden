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

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public override void ChangeHealth(int change, GameObject from)
    {
        if (immune)
        {
            return;
        }
        base.ChangeHealth(change, from);

        Debug.LogWarning("Player is being attacked");

        // being attacked animation

        // deals counter damage
        if (change < 0)
        {
            //Debug.LogWarning($"Dealing counter damage to {from.name}");
            //Health health = from.GetComponent<Health>();
            //health.ChangeHealth(-counterDamage, gameObject);
        }
    }
    protected override void Die()
    {
        Debug.LogWarning("PLAYER HAS DIED.");

        // stop player motion for 6 seconds, flashing, and continue moving after.
        normalSpeed = movement_manager.mySpeed;
        movement_manager.mySpeed= 0;
        immune= true;
        Debug.Log("The player is immune now.");
        StartCoroutine(dieAndRotate(dieRotTime,normalSpeed));  
    }

    private IEnumerator dieAndRotate(float duration, float curr_speed)
    {
        float t = 0.0f;
        while(t < duration)
        {
            t += Time.deltaTime;
            gameObject.transform.Rotate(new Vector3(0, 0, rotPerSec) * Time.deltaTime);
            yield return null;
        }
        movement_manager.mySpeed = curr_speed;
        Debug.Log(movement_manager.mySpeed);
        _currentHealth = maxHealth;
        Debug.Log("Restored Player Health to " + maxHealth);
        yield return new WaitForSeconds(immuneTime);
        immune = false;
        Debug.Log("The player is now normal");
    }

}