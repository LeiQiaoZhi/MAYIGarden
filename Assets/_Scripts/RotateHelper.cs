using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RotateHelper  
{
    public static void RotateTowards(Rigidbody2D rb, Transform target, Vector3 referenceDirection  ,float rotateSpeed)
    {
        var direction = (Vector2)(target.position - rb.transform.position).normalized;
        float rotateAmount = Vector3.Cross(direction, referenceDirection).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
    }

    /// <summary>
    /// returns angle from 0 to 180 degrees
    /// </summary>
    /// <param name="originPosition"></param>
    /// <param name="targetPosition"></param>
    /// <param name="referenceDirection"></param>
    /// <returns></returns>
    public static float AngleBetween(Vector3 originPosition, Vector3 targetPosition, Vector3 referenceDirection)
    {
        var direction = (Vector2)(targetPosition - originPosition).normalized;
        var angle = Mathf.Acos(Vector2.Dot(direction, referenceDirection.normalized))*Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 180;
        }

        return angle;
    }
}