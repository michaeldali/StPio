using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    // Player components
    private Rigidbody2D body;
    private CircleCollider2D circleCollider;
    private Animator anim;
    [SerializeField] private LayerMask groundLayer;

    // Movement
    public float baseSpeed = 10f;
    public float speed = 0f;
    [SerializeField] private float jumpPower = 10f;
    private float canJump = 0f;
    public float horizontalMove = 0f;
    public float jumpRate = 1f;

    // Attack
    [SerializeField] private int attackDamage = 40;
    [SerializeField] public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public Animator enemyAnim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Scrolls
    public bool isReading = false;
    public GameObject scrollBackground;

    // Health
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBarScript healthBar;
    public EffectScript effectScript;

    // Bilocation
    private bool bilocating;
    private Vector2 originalLocation;
    public GameObject bilocationLocation;

    // Respawn
    public Vector2 respawnPoint;
    private bool isDying = false;

    // Pause
    private PauseMenu pauseMenu;
    


    // Start is called before the first frame update
    void Start()
    {
        circleCollider = this.GetComponent<CircleCollider2D>();
        body = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        respawnPoint = transform.position;
        pauseMenu = FindObjectOfType<PauseMenu>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDying)
        {
            return;
        }
        // Sets value to move player left and right
        speed = baseSpeed;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 1")){
            speed = baseSpeed/1.5f;
        }
        horizontalMove = Input.GetAxis("Horizontal")*speed;
        body.velocity = new Vector2(horizontalMove, body.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        anim.SetBool("IsGrounded", isGrounded());
        // Checks if player can jump
        if (Input.GetButtonDown("Jump") && isGrounded() && Time.time > canJump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower*1.5f);
            anim.SetBool("IsGrounded", isGrounded());
            canJump = Time.time + jumpRate;
        }

        // Moves player left and right
        if (horizontalMove>0)
        {
            // Face right
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (horizontalMove<0)
        {
            // Face left
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // Attack logic
        if (Time.time > nextAttackTime && Input.GetKeyDown(KeyCode.Mouse0) && isGrounded())
        {
            // Attack animation
            anim.SetTrigger("Attack");
            nextAttackTime = Time.time + 1f / attackRate;
        }

        // Pause game and exit scroll text UI
        if (isReading == true)
        {
            Time.timeScale = 0f;   
        }
        else if (!pauseMenu.IsPaused)
        {
            isReading = false;
            Time.timeScale = 1f;
            scrollBackground.SetActive(false);
        }   

        // Bilocation
        if (bilocating && Input.GetKeyDown(KeyCode.B)){
            exitBilocation();
        }
        
    }
    private bool isGrounded(){
        // Sends raycast from center of box collider
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center,circleCollider.bounds.size, 
        0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void Attack(){
        
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("ChainEnemy")){
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
            else {
                enemy.GetComponent<HellHound>().TakeDamage(attackDamage);
            }
        }
    }

    // Player takes set amount of damage
    public void TakeDamage(int damage){
        currentHealth -= damage;

        // Calls die function if player health is below 0
        if (currentHealth <= 0){
            Die();
        }
        healthBar.setHealth(currentHealth);
        anim.SetTrigger("Hit");

        

    }

    // Removes player's ability to move (NEEDS TO SEND TO MENU SCREEN)
    public void Die(){

        anim.SetBool("isDead", true);
        isDying = true;
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        isDying = false;
        healPlayer();
        anim.SetBool("isDead", false);
        var enemies = FindObjectsOfType<Enemy>();
        var hellhounds = FindObjectsOfType<HellHound>();
        foreach (Enemy enemy in enemies)
        {
            enemy.isDead = false;
            enemy.currentHealth = enemy.maxHealth;
            enemy.anim.SetBool("IsDead", false);
        }
        foreach (HellHound hellhound in hellhounds)
        {
            hellhound.isDead = false;
            hellhound.currentHealth = hellhound.maxHealth;
            hellhound.anim.SetBool("IsDead", false);
        }
    }

    // Used for editor attack size changes
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void healPlayer()
    {
        currentHealth = 100;
        healthBar.setHealth(currentHealth);
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
 
  
    }

    private void playShinyEffect(){
        effectScript.shinyEffect();
    }

    private void bilocation()
    {
        originalLocation = transform.position;
        transform.position = bilocationLocation.transform.position;
        bilocating = true;
    }
    
    private void exitBilocation()
    {
        transform.position = originalLocation;
        bilocating = false;
    }

}