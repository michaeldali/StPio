using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody2D body;
    private CircleCollider2D circleCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 10f;
    private float canJump = 0f;
    private float horizontalMove = 0f;
    
    

    private Animator anim;

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
        // Moves player left and right
        horizontalMove = Input.GetAxis("Horizontal")*speed;
        body.velocity = new Vector2(horizontalMove, body.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        anim.SetBool("IsGrounded", isGrounded());
        if (Input.GetButton("Jump") && isGrounded() && Time.time > canJump)
        {
            body.velocity = new Vector2(body.velocity.x, speed*1.5f);
            anim.SetBool("IsGrounded", isGrounded());
            canJump = Time.time + 1f;
        }
        if (horizontalMove>0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (horizontalMove<0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    private bool isGrounded(){
        // Sends raycast from center of box collider
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center,circleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}