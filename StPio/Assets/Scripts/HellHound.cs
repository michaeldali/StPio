using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellHound : MonoBehaviour
{
    // Health
    public int maxHealth = 100;
    public int currentHealth;
    // GameObject information
    public Animator anim;
    private Rigidbody2D rb;
    // Movement
    public float moveDistance = 5.0f;
    public float speed = 1.0f;
    private float startingX;
    public bool movingRight = true;
    public bool stillFaceLeft = false;
    // Attack
    public float attackRange = 2.0f;
    private AiPlayerEnterAreaDetector detect;
    public bool playerDetector;
    public LayerMask playerLayer;
    public float agroDistance = 5f;
    public Transform player;
    public int attackDamage = 20;
    public Transform attackPoint;
    public float attackArea = .7f;

    // Edge check
    private BoxCollider2D boxCol;
    private LayerMask groundLayer;
    public Transform groundDetectOrigin;

    // Fireball projectile
    public Transform firePoint;
    public GameObject fireballPrefab;

    // Death
    public bool isDead = false;

    // Hit flash (NEEDS TO BE DONE)


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        detect = gameObject.GetComponentInChildren<AiPlayerEnterAreaDetector>();
        boxCol = gameObject.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startingX = transform.position.x;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }
        // Enemy patrol logic
        float currentX = transform.position.x;
        playerDetector = detect.PlayerDetected;

        if (stillFaceLeft){
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (Vector2.Distance(player.position, rb.position) <= attackRange && playerDetector == true){
                anim.SetTrigger("inRange");
            }
            return;
        }
        if (playerDetector == false || onEdge()) // Patrol if player isn't seen by enemy
        {
            patrolling(currentX);
        }
        else if (Vector2.Distance(player.position, rb.position) <= attackRange){ // Stop and attack the player if they are within range
            anim.SetTrigger("inRange");
        }
        else { // Move towards player when seen by enemy
            moveTowardsPlayer();
        }

    }

    // Checks if the enemy is on the edge (Position of raycast needs to be adjusted!!!!!)
    private bool onEdge(){
        RaycastHit2D raycastHit = Physics2D.Raycast(groundDetectOrigin.position, Vector2.down, 1f);
        return raycastHit.collider == null;
    }


    // Moves enemy between two points set from gameobjects original position
    public void patrolling(float currentX)
    {
        if (movingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            rb.velocity = new Vector2(speed, 0);
            if (currentX >= startingX + moveDistance)
            {
                movingRight = false;
            }
        }
        else {
            transform.eulerAngles = new Vector3(0, 180, 0);
            rb.velocity = new Vector2(-speed, 0);
            if (currentX <= startingX - moveDistance)
            {
                movingRight = true;
            }
        }
    }

    // Called during the attack animation
    // Damages player if within overlap circle
    public void attack(){
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackArea, playerLayer);
        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetType() == typeof(BoxCollider2D)){
                player.GetComponent<Controller>().TakeDamage(attackDamage);
            }
        }
        Shoot();
        
    }

    void Shoot () {
        // Shooting logic
        float facing = 0f;
        if (this.transform.rotation.y == 0){
            facing = 0f;
        }
        else {
            facing = 180f;
        }

        Instantiate(fireballPrefab, firePoint.position, Quaternion.Euler(0, facing, -90));
    }

    // Enemy walks towards player when within sight
    public void moveTowardsPlayer()
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        
    }


    // Enemy takes a set amount of damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // If enemy's health goes below 0, call die function
        if (currentHealth <= 0)
        {
            Die();
        }

        // Play hurt animation
        anim.SetTrigger("Hurt");
        
        
    }


    // Plays death animation and removes ability to move and attack
    void Die()
    {  
        // Prevents enemy from moving
        anim.SetBool("IsDead", true);
        isDead = false;

    }

       void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackArea);
        
    }
}
