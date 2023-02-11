using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerHealth: Health
{
    public playerMovement movement_manager;
    public int counterDamage = 2;
    private GameManager _gameManager;
    public float normalSpeed = 10;
    public int rotPerSec = 240;
    public float dieRotTime = 3;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public override void ChangeHealth(int change, GameObject from)
    {
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
        movement_manager.mySpeed= 0;
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
        Debug.LogWarning(movement_manager.mySpeed);
    }

}