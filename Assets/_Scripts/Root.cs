using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Root : Health
{
    public int counterDamage = 2;
    public float secondsBeforeCounter = 0.5f;
    public ParticleSystem healingEffect;

    private GameManager _gameManager;
    private UIManager _uiManager;

    public override void Start()
    {
        base.Start();
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void ChangeHealth(int change, GameObject from)
    {
        base.ChangeHealth(change, from);
        if (_uiManager)
        {
            _uiManager.UpdateHealthSlider(_currentHealth, maxHealth);
        }

        Debug.LogWarning("Root is being attacked");

        // being attacked animation

        // deals counter damage
        if (change < 0 && counterDamage > 0)
        {
            StartCoroutine(Counter(from));
        }

        if (change < 0)
        {
            AudioManager.Instance.PlayRandomFromSoundSet("Hit");
        }

        if (change > 0)
        {
            AudioManager.Instance.PlaySound("Heal");
            var effect = Instantiate(healingEffect, transform.GetChild(0));
            Destroy(effect,5f);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator Counter(GameObject from)
    {
        yield return new WaitForSeconds(secondsBeforeCounter);
        Debug.LogWarning($"Dealing counter damage to {from.name}");
        Health health = from.GetComponent<Health>();
        health.ChangeHealth(-counterDamage, gameObject);
    }

    protected override void Die()
    {
        Debug.LogWarning("ROOT HAS DIED.");

        // game over
        _gameManager.GameOver();
    }
}