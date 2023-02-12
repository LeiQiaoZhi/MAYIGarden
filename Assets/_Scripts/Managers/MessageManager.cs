using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SINGLETON

public class MessageManager : MonoBehaviour
{
    [SerializeField] GameObject messageCanvasPrefab;
    [SerializeField] GameObject messageObjectPrefab;

    public static MessageManager Instance;

    GameObject _messageCanvas;

    private void Start()
    {
        // DisplayMessage("Test Message", null, 2);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(_messageCanvas == null)
        {
            _messageCanvas = Instantiate(messageCanvasPrefab, transform);
        }
    }

    public void DisplayMessage(string text, Color? textColor = null, float? duration = null)
    {
        GameObject messageGo =  Instantiate(messageObjectPrefab,_messageCanvas.transform);
        Message m = messageGo.GetComponent<Message>();
        m.Init(text, textColor, duration);
        Destroy(messageGo, 10f);
    }
}
