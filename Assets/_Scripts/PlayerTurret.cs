using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurret : MonoBehaviour
{
    public KeyCode placeKey = KeyCode.Space;
    public GameObject turretPrefab;

    private int _turretCount = 0;

    public void GiveTurret()
    {
        _turretCount += 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(placeKey)&&_turretCount>0)
        {
            Debug.LogWarning("Placing Turret");
            // place turret
            GameObject turret = Instantiate(turretPrefab);
            turret.transform.position = transform.position;
            _turretCount -= 1;
        }
    }
}
