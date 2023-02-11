using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    private Collider2D _attackCollider;
    private LayerMask _attackableMask;
    private int _damage;

    private void Start()
    {
        _attackCollider = GetComponent<Collider2D>();
    }

    public void TurnOn(LayerMask attackableMask, int damage)
    {
        _attackableMask = attackableMask;
        _damage = damage;
        _attackCollider.enabled = true;
    }

    /// <summary>
    /// used as animation event in attack animation
    /// </summary>
    public void TurnOff()
    {
        _attackCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (LayerMaskHelper.IsInLayerMask(col.gameObject.layer, _attackableMask))
        {
            Debug.LogWarning($"Hit {col.gameObject.name}");
            Health health = col.GetComponent<Health>();
            health.ChangeHealth(-_damage,transform.parent.gameObject);
        }
    }
}
