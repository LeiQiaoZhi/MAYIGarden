using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Turret : MonoBehaviour
{
    public float detectRadius = 5f;
    public float secondsBetweenFire = 1f;
    public float rotateSpeed = 20;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireTime = 0f;
    private Rigidbody2D _rb;


    Transform _target;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        // Rotate the turret towards the target if there is any
        if (_target)
        {
            if (Vector2.Distance(_target.position, transform.position) > detectRadius)
            {
                _target = null;
                return;
            }

            RotateHelper.RotateTowards(_rb,_target,transform.right,rotateSpeed);
            var direction = (Vector2)(_target.position - transform.position).normalized;
            // float rotateAmount = Vector3.Cross(direction, transform.right).z;
            // _rb.angularVelocity = -rotateAmount * rotateSpeed;
            
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
            float angleDiff = (targetAngle - transform.rotation.eulerAngles.z);
            if (transform.rotation.eulerAngles.z > 180)
            {
                angleDiff += 360;
            }

            // Fire a bullet if the fire rate time has passed
            if (Time.time >= fireTime && Mathf.Abs(angleDiff) < 5)
            {
                AudioManager.Instance.PlaySound("Shoot");
                fireTime = Time.time + secondsBetweenFire;
                Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            }
        }
        else
        {
            // Detect if there is any enemy within the detection radius
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRadius);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Debug.LogWarning("Target Acquired");
                    _target = hit.transform;
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}