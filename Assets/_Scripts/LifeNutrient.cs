using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeNutrient : Nutrient
{
    public int restoreHealth = 1;
    private Root _rootHealth;

    private void Awake()
    {
        _rootHealth = GetComponent<Root>();
    }

    public override void OnTriggerWithPlayer(GameObject player)
    {
        Debug.LogWarning($"Life Nutrient is picked up by player. Restore {restoreHealth} health to root");
        _rootHealth.ChangeHealth(restoreHealth,gameObject);
    }
}
