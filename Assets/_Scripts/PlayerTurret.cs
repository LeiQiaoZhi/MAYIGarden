using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurret : MonoBehaviour
{
    public KeyCode placeKey = KeyCode.Space;
    public GameObject turretPrefab;

    private int turretCount = 0;

    public void GiveTurret()
    {
        turretCount += 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(placeKey)&&turretCount>0)
        {
            // place turret
            GameObject turret = (turretPrefab);
            turret.transform.position = transform.position;
            turretCount -= 1;
        }
    }
}
