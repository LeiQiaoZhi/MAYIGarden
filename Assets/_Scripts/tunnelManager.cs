using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnelManager : MonoBehaviour
{
    public int seedNum = 0;
    private GameObject[] tunnelList = new GameObject[2];
    private int tunnelNum = 0; //this will need to change to private later
    public GameObject tunnelPrefab;
    public float coolDownTime = 1.5f;
    private float nextTrasTime = 1f; // potentially, the player may get transported when creating the portal
    private UIManager _uiManager;


    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _uiManager.UpdateTunnelCount(tunnelNum);
    }

    public Vector3 getTunnelPos(int id)
    {
        if (Time.time >= nextTrasTime)
        {
            nextTrasTime = Time.time + coolDownTime;
            if (tunnelNum < 2)
            {
                Debug.Log("Not enough tunnel, only " + tunnelNum);
                return Vector3.zero; //problem here, will go to origin if only one tunnel
            }
            else
            {
                int theOtherIndex = 1 - id;
                return tunnelList[theOtherIndex].transform.position;
            }
        }
        else
        {
            Debug.Log("Tunnel in cool down");
            return Vector3.zero;
        }
    }

    public void createTunnel(Vector3 pos)
    {
        if (seedNum < 1)
        {
            Debug.Log("Not enough seed, onlt " + seedNum);
            return;
        }

        seedNum -= 1;
        _uiManager.UpdateTunnelCount(seedNum);
        nextTrasTime = Time.time + coolDownTime; // not transporting at first creation
        // instantiate a tunnel at pos
        GameObject newTunnel = Instantiate(tunnelPrefab, pos, Quaternion.identity);
        newTunnel.GetComponent<tunnelTransport>().tm = this;
        newTunnel.GetComponent<tunnelTransport>().id = tunnelNum;
        tunnelList[tunnelNum] = newTunnel; //add the new tunnel to the list
        tunnelNum += 1;
    }
}