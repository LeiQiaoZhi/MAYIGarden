using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretNutrient : Nutrient
{

    public override void OnTriggerWithPlayer(GameObject player)
    {
        player.GetComponent<PlayerTurret>().GiveTurret();
    }
}
