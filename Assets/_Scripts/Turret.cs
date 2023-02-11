using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float detectRadius = 5f;
    public float secondsBetweenFire = 1f;
    public float rotateSpeed = 20;
    public GameObject bulletPrefab;

    private float fireTime = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

     Transform target;
    public float minAngleToClampSpeed = 10;

    private void Update()
    {
        // Rotate the turret towards the target if there is any
        if (target)
        {
            if (Vector2.Distance(target.position, transform.position) > detectRadius)
            {
                target = null;
                return;
            }
            Vector3 direction = (target.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
            Debug.Log($"target: {targetAngle}");
            Debug.Log($"z: {transform.rotation.eulerAngles.z}");
            Debug.Log(targetAngle-transform.rotation.eulerAngles.z);
            float angleDiff = (targetAngle - transform.rotation.eulerAngles.z);
            if (transform.rotation.eulerAngles.z > 180)
            {
                angleDiff += 360;
            }

            float rotateIncrement = angleDiff * Time.deltaTime * rotateSpeed;
            if (Mathf.Abs(rotateIncrement) < minAngleToClampSpeed * rotateSpeed * Time.deltaTime)
            {
                rotateIncrement = Mathf.Sign(rotateIncrement) * minAngleToClampSpeed * rotateSpeed * Time.deltaTime;
            }

            transform.Rotate(Vector3.forward,rotateIncrement);
            // transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(), Vector3.forward);

            // Fire a bullet if the fire rate time has passed
            if (Time.time >= fireTime && Mathf.Abs(angleDiff)<1)
            {
                fireTime = Time.time + secondsBetweenFire;
                Instantiate(bulletPrefab, transform.position, transform.rotation);
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
                    target = hit.transform;
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

