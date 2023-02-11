using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingBullet : Bullet
{
    public Transform scannAreaCenter;
    public float scanAreaRadius;

    [Tooltip("If out of range, stop homing")]
    public float homingRadius;

    [SerializeField] private float rotateSpeed;

    [SerializeField] private Transform _target;
    private Rigidbody2D _rb;


    public override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // find target
        if (!_target)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(scannAreaCenter.position, scanAreaRadius, targetLayers);
            foreach (Collider2D hit in hits)
            {
                Debug.LogWarning($"Bullet Target Acquired -- {hit.name}");
                _target = hit.transform;
                break;
            }
        }
        // homing behaviour
        else
        {
            if (Vector2.Distance(_target.position, transform.position) < homingRadius)
            {
                var direction = (Vector2)(_target.position - transform.position).normalized;
                float rotateAmount = Vector3.Cross(direction, GetFacingDirection()).z;
                _rb.angularVelocity = -rotateAmount * rotateSpeed;
                _rb.velocity = GetFacingDirection() * speed;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(scannAreaCenter.position, scanAreaRadius);
    }
}