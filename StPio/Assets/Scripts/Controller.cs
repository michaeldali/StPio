using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private Rigidbody2D body;
    private CircleCollider2D circleCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private int attackDamage = 40;
    [SerializeField] public float attackRate = 2f;
    private float nextAttackTime = 0f;
    private float canJump = 0f;
    private float horizontalMove = 0f;
    public float jumpRate = 1f;
    private Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Image[] scrollTexts;


    // Start is called before the first frame update
    void Start()
    {
        circleCollider = this.GetComponent<CircleCollider2D>();
        body = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Sets value to move player left and right
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
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (horizontalMove<0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // Attack logic
        if (Time.time >= nextAttackTime && Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Attack animation
            anim.SetTrigger("Attack");
            nextAttackTime = Time.time + 1f / attackRate;
        }

        // Pause game and exit scroll text UI
        if (ScrollCounterScript.scrollAmount == 0)
        {
            
        } else 
        {
            if (scrollTexts[ScrollCounterScript.scrollAmount-1].enabled == true)
            {
                Time.timeScale = 0f;
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    scrollTexts[ScrollCounterScript.scrollAmount-1].enabled = false;
                    Time.timeScale = 1f;
                }
            }          
        }
        
    }
    private bool isGrounded(){
        // Sends raycast from center of box collider
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center,circleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void Attack(){

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}