using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnelNutrient : Nutrient
{
    public tunnelManager tm;

    private void Start()
    {
        tm = FindObjectOfType<tunnelManager>();
    }

    public override void OnTriggerWithPlayer(GameObject player)
    {
        Debug.LogWarning($"Tunnel Nutrient is picked up by player. Gained 1 seed");
        tm.seedNum++;
    }
}
