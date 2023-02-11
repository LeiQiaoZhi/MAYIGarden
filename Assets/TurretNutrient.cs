using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretNutrient : Nutrient
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTriggerWithPlayer(GameObject player)
    {
        player.GetComponent<PlayerTurret>().GiveTurret();
    }
}
