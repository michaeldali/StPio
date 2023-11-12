using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireballPrefab;
    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot () {
        // Shooting logic
        Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
    }
}
