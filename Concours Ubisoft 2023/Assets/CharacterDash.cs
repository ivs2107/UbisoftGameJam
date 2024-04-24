using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDash : MonoBehaviour
{
    [SerializeField] private float dashingVelocity = 30f;
    [SerializeField] private float dashingCD = 0.5f;
    private Vector2 dashingDir;
    public bool canDash;
    public bool onGround;
    public bool cdDashFinish;

    [HideInInspector] public Rigidbody2D body;
    private GroundCheck ground;
    private CharacterMovement movement;
    private CharacterJump jump;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<GroundCheck>();
        movement = GetComponent<CharacterMovement>();
        jump = GetComponent<CharacterJump>();
        canDash = true;
    }

   /* public void Dash(InputAction.CallbackContext context) {
        if (Movable.instance.CharacterCanMove) {
            if (context.started && canDash) {
                jump.pressingJump = false;
                dashingDir = new Vector2(movement.direction.x, movement.direction.y);
                //dashingDir = new Vector2(movement.directionX, 0);
                StartCoroutine(DoADash());
            }
        }
    }*/

    public void Dash()
    {
        if (Movable.instance.CharacterCanMove)
        {
            if (canDash)
            {
                jump.pressingJump = false;
                dashingDir = new Vector2(movement.direction.x, movement.direction.y);
                StartCoroutine(DoADash());
            }
        }
    }

    private void Update() {
        onGround = ground.GetOnGround();
        if (onGround && cdDashFinish) {
            canDash = true;
        }
    }

    private IEnumerator DoADash() {
        //trail
        FindObjectOfType<GhostTrail>().ShowGhost();
        //sound
        AudioManager.instance.Play("Dash");

        canDash = false;
        cdDashFinish = false;

        float trueGravity = body.gravityScale;

        body.gravityScale = 0f;
        body.velocity = Vector2.zero;

        //body.velocity = dashingDir * dashingVelocity;
        body.AddForce(dashingDir * dashingVelocity, ForceMode2D.Impulse);
        if (dashingDir == Vector2.zero)
            //body.velocity =  dashingVelocity * movement.isFacing * Vector2.right;
            body.AddForce(dashingVelocity * movement.isFacing * Vector2.right, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashingCD);

        body.gravityScale = trueGravity;
        cdDashFinish = true;
    }
}
