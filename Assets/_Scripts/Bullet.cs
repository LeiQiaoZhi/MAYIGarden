using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeTime = 10f;
    public LayerMask layersToDestroyIt;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var _colGameObject = col.gameObject;
        if (_colGameObject.CompareTag("Enemy"))
        {
            Health health = _colGameObject.GetComponent<Health>();
            health.ChangeHealth(-damage, gameObject);
        }

        if(LayerMaskHelper.IsInLayerMask(_colGameObject.layer,layersToDestroyIt))
        {
            Destroy(gameObject);
        }
    }
}