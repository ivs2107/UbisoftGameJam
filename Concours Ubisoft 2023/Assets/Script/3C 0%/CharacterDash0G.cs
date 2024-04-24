using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDash0G : MonoBehaviour
{
    [SerializeField] private float dashingVelocity = 30f;
    [SerializeField] private float dashingCD = 0.5f;
    private Vector2 dashingDir;
    public bool canDash;
    public bool onGround;
    public bool cdDashFinish;

    [HideInInspector] public Rigidbody2D body;
    private CharacterMovement0G movement;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<CharacterMovement0G>();
        canDash = true;
    }

    public void Dash()
    {
        if (canDash) {
            dashingDir = new Vector2(movement.direction.x, movement.direction.y);
            StartCoroutine(DoADash());
        }
    }

    private IEnumerator DoADash() {
        FindObjectOfType<GhostTrail>().ShowGhost();

        canDash = false;

        body.velocity = Vector2.zero;

        body.AddForce(dashingDir * dashingVelocity, ForceMode2D.Impulse);
        if (dashingDir == Vector2.zero)
            body.AddForce(dashingVelocity * movement.isFacing * Vector2.right, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashingCD);
        canDash = true;
    }
}
