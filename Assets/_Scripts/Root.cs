using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : Health
{
    public override void ChangeHealth(int change)
    {
        base.ChangeHealth(change);
        
        Debug.LogWarning("Root is being attacked");
        
        // being attacked animation
        
        // return damage
    }

    protected override void Die()
    {
        Debug.LogWarning("ROOT HAS DIED.");
        
        // game over
    }
}
