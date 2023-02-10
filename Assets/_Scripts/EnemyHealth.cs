using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    protected override void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }
}
