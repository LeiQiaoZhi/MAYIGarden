using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSuicide : MonoBehaviour
{
    // Start is called before the first frame update
    public Health myHealth;
    public bool kill =false;
    public int suicideDamage = 10;

    // Update is called once per frame
    void Update()
    {
        if (kill)
        {
            myHealth.ChangeHealth(-suicideDamage, gameObject);
            kill = false;
        }
    }
}
