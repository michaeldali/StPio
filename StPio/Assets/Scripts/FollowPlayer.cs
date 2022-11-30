using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPostiion = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPostiion, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
