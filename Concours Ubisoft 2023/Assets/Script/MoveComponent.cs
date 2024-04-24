using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float moveSpeed = 10;
    [SerializeField]
    private float gravity = 3;
    [SerializeField]
    private float jumpForce = 400;
    [SerializeField]
    private float dashForce = 1000;
    [SerializeField] 
    private LayerMask whatIsGround;

    private float gravityModifier;
    private float movementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D rigidbody2D;
    private bool facingRight = true;
    public bool isGrounded;

    //LPPPPPP oublie pas ça ;)
    public Transform persoSprite;

    //need for dash
    Vector2 moveVector;


    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();



    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround) != null;
    }
    public void Move(Vector2 vector)
    {
        moveVector = vector;


        Vector3 targetVelocity = new Vector2(vector.x * moveSpeed , vector.y * moveSpeed);
        //Vector3 targetVelocity = new Vector2(vector.x * moveSpeed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref m_Velocity, movementSmoothing);
 

        if (targetVelocity.x > 0 && !facingRight || targetVelocity.x < 0 && facingRight)
        {
            Flip();
        }
        else
        {

        }

    }
    public void Jump()
    {
        if (isGrounded)
        {
            // Add a vertical force to the player.
            isGrounded = false;
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }
    public void Dash()
    {

        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.velocity += moveVector * 100;

    }
    public void SetGravityModifier(float gravMod)
    {
        gravityModifier = gravMod;
        rigidbody2D.gravityScale = gravityModifier * gravity;
    }
    private void Flip()
    {
        facingRight = !facingRight;
        persoSprite.Rotate(0f, 180f, 0f);
    }
}
