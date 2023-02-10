using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Nutrient : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            OnTriggerWithPlayer();
        }
    }

    public abstract void OnTriggerWithPlayer();
}