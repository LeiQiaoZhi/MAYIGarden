using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTunnel : MonoBehaviour
{
    public tunnelManager tm;
    public KeyCode TunnelKey = KeyCode.T;
    public float coolDownTime = 1f;
    private float nextTunnelTime = 0f;

    private void Start()
    {
        tm = FindObjectOfType<tunnelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(TunnelKey)&&Time.time >= nextTunnelTime) 
        {
            nextTunnelTime=Time.time+ coolDownTime;
            tm.createTunnel(gameObject.transform.position);
        }
    }
}
