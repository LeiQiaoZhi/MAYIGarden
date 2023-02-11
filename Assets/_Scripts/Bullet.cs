using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public enum DirectionEnum
    {
        Right,
        Up
    }

    public DirectionEnum defaultTowardsDirection = DirectionEnum.Right;
    public float speed;
    public int damage;
    public float lifeTime = 10f;
    public LayerMask layersToDestroyIt;
    public LayerMask targetLayers;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = GetFacingDirection() * speed;

        Destroy(gameObject, lifeTime);
    }

    protected Vector3 GetFacingDirection()
    {
        switch (defaultTowardsDirection)
        {
            case (DirectionEnum.Right):
                return transform.right ;
            case (DirectionEnum.Up):
                return transform.up ;
        }

        return transform.right;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var _colGameObject = col.gameObject;
        if (LayerMaskHelper.IsInLayerMask(col.gameObject.layer, targetLayers))
        {
            Health health = _colGameObject.GetComponent<Health>();
            health.ChangeHealth(-damage, gameObject);
        }

        if (LayerMaskHelper.IsInLayerMask(_colGameObject.layer, layersToDestroyIt))
        {
            Destroy(gameObject);
        }
    }
}