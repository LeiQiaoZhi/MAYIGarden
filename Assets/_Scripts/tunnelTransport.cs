using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnelTransport : MonoBehaviour
{
    [HideInInspector]
    public tunnelManager tm;// need to be private
    [HideInInspector]
    public int id = 0; //need to be private
    public Vector3 targetPosition = new Vector3(0, 0, 0);
    public LayerMask targetLayers;

    public ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //1. ask position from tunnel manager
        //2. transport
        Debug.Log("Something entered the tunnel.");
        if (LayerMaskHelper.IsInLayerMask(collision.gameObject.layer,targetLayers))
        {
            targetPosition = tm.getTunnelPos(id);
            if (targetPosition != new Vector3(0,0,0))
            {
                transportation(targetPosition, collision.gameObject);
            }
            else
            {
                Debug.Log("Unsuccesful transport");
            }
        }
    }

    private void transportation(Vector3 targetPos,GameObject who)
    {
        Debug.Log("Successful transportation!");
        particleSystem.Play();
        who.transform.position = targetPos;
    }
}
