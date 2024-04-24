using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterJump : MonoBehaviour
{
    [SerializeField, Range(2f, 5.5f)] public float jumpHeight = 7.3f;
    [SerializeField, Range(0.2f, 1.25f)] public float timeToHeightMax;
    [SerializeField, Range(0f, 5f)] public float upwardMovementMultiplier = 1f;
    [SerializeField, Range(1f, 10f)] public float downwardMovementMultiplier = 6.17f;
    [SerializeField, Range(0, 1)] public int maxAirJumps = 0;

    public bool variablejumpHeight;
    [SerializeField, Range(1f, 10f)] public float jumpCutOff;

    [SerializeField] public float speedLimit;
    [SerializeField, Range(0f, 0.3f)] public float coyoteTime = 0.15f;
    [SerializeField, Range(0f, 0.3f)] public float jumpBuffer = 0.15f;

    [HideInInspector] public Rigidbody2D body;
    [HideInInspector] public Vector2 velocity;
    private GroundCheck ground;

    private float jumpSpeed;
    private float defaultGravityScale;
    private float gravMultiplier;

    private bool canJumpAgain = false;
    private bool desiredJump;
    private float jumpBufferCounter;
    private float coyoteTimeCounter = 0;
    [HideInInspector] public bool pressingJump;
    private bool onGround;
    private bool currentlyJumping;

    public Animator animator;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<GroundCheck>();
        defaultGravityScale = 1f;
    }

    /*public void Jump(InputAction.CallbackContext context) {
        if (Movable.instance.CharacterCanMove) {
            if (context.started) {
                desiredJump = true;
                pressingJump = true;
            }
            if (context.canceled) {
                pressingJump = false;
            }
        }
    }*/
    public void JumpStart()
    {
        if (Movable.instance.CharacterCanMove)
        {
            desiredJump = true;
            pressingJump = true;
            isJumping = true;
            
        }
    }
    public void JumpCancel()
    {
        if (Movable.instance.CharacterCanMove)
        {
            pressingJump = false;
        }
    }


    private bool isJumping = false;
    void Update() {
        setPhysics();

        onGround = ground.GetOnGround();

        animator.SetBool("isJumping", !onGround && isJumping);

        if (jumpBuffer > 0) {
            if (desiredJump) {
                jumpBufferCounter += Time.deltaTime;
                if (jumpBufferCounter > jumpBuffer) {
                    desiredJump = false;
                    jumpBufferCounter = 0;
                }
            }
        }
        
        if (!currentlyJumping && !onGround) {
            coyoteTimeCounter += Time.deltaTime;
            isJumping = false;
        }
        else {
            coyoteTimeCounter = 0;
        }




    }

    private void setPhysics() {
        Vector2 newGravity = new Vector2(0, (-2 * jumpHeight) / (timeToHeightMax * timeToHeightMax));
        body.gravityScale = (newGravity.y / Physics2D.gravity.y) * gravMultiplier;
    }

    private void FixedUpdate() {
        velocity = body.velocity;

        if (desiredJump) {
            DoAJump();
            return;
        }
        calculateGravity();
    }

    private void calculateGravity() {
        if (body.velocity.y > 0.01f) {
            if (onGround) {
                gravMultiplier = defaultGravityScale;
            }
            else {
                if (variablejumpHeight) {
                    if (pressingJump && currentlyJumping) {
                        gravMultiplier = upwardMovementMultiplier;
                    }
                    else {
                        gravMultiplier = jumpCutOff;
                    }
                }
                else {
                    gravMultiplier = upwardMovementMultiplier;
                }
            }
        }
        else if (body.velocity.y < -0.01f) {
            if (onGround) {
                gravMultiplier = defaultGravityScale;
            }
            else {
                gravMultiplier = downwardMovementMultiplier;
            }
        }
        else {
            if (onGround) {
                currentlyJumping = false;
            }
            gravMultiplier = defaultGravityScale;
        }
        body.velocity = new Vector3(velocity.x, Mathf.Clamp(velocity.y, -speedLimit, 100));
    }

    private void DoAJump() {
        if (onGround || (coyoteTimeCounter > 0.03f && coyoteTimeCounter < coyoteTime) || canJumpAgain) {
            AudioManager.instance.Play("Jump");
            desiredJump = false;
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;

            canJumpAgain = (maxAirJumps == 1 && canJumpAgain == false);

            jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * 3.75f * jumpHeight);

            if (velocity.y > 0f) {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            else if (velocity.y < 0f) {
                jumpSpeed += Mathf.Abs(body.velocity.y);
            }

            velocity.y += jumpSpeed;
            currentlyJumping = true;
        }
        if (jumpBuffer == 0) {
            desiredJump = false;
        }
        body.velocity = velocity;
    }
}
