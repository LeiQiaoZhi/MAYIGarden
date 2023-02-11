using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameralAnimator : MonoBehaviour
{
    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelEndUI()
    {
        _uiManager.SetEnableLevelEndScreen(true);
    }
}
