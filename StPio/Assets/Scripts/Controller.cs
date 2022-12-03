using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


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
        if (Input.GetButton("Jump") && isGrounded() && Time.time > canJump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower*1.5f);
            anim.SetBool("IsGrounded", isGrounded());
            canJump = Time.time + 1f;
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
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    private bool isGrounded(){
        // Sends raycast from center of box collider
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center,circleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void Attack(){
        // Attack animation
        anim.SetTrigger("Attack");
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