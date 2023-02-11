using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int maxHealth;
    
    protected int _currentHealth;

    // Start is called before the first frame update
    public virtual void Start()
    {
        _currentHealth = maxHealth;
    }

    public virtual void ChangeHealth(int change, GameObject from)
    {
        _currentHealth += change;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
