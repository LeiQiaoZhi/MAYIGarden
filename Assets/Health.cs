using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int maxHealth;
    
    private int _currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
    }

    public void ChangeHealth(int change)
    {
        _currentHealth += change;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
