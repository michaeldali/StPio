using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 40;
    private float start;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position.x;
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D (Collider2D hitInfo) {
        Controller player = hitInfo.GetComponent<Controller>();
        
        if (player != null){
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (hitInfo.gameObject.CompareTag("HellHound")){
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (transform.position.x > start + 15){
            Destroy(gameObject);
        }
        else if (transform.position.x < start - 15){
            Destroy(gameObject);
        }
    }


}
