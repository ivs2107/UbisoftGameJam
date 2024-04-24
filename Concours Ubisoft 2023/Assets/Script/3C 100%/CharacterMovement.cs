using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField, Range(0f,  20f)] public float maxSpeed = 11f;
    [SerializeField, Range(0f, 100f)] public float maxAcceleration = 80f;
    [SerializeField, Range(0f, 100f)] public float maxDecceleration = 100f;
    [SerializeField, Range(0f, 100f)] public float maxTurnSpeed = 80f;
    [SerializeField, Range(0f, 100f)] public float maxAirAcceleration = 80f;
    [SerializeField, Range(0f, 100f)] public float maxAirDeceleration = 100f;
    [SerializeField, Range(0f, 100f)] public float maxAirTurnSpeed = 80f;

    private Vector2 desiredVelocity;
    private Vector2 velocity;
    public Vector2 direction;
    public float isFacing;
    public float directionX;
    private float maxSpeedChange;
    private float acceleration;
    private float deceleration;
    private float turnSpeed;

    private bool onGround;
    private bool pressingKey;

    private Rigidbody2D body;
    GroundCheck ground;

    public Animator animator;


    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<GroundCheck>();
    }

    public void OnMovement(Vector2 vector) {
        direction = vector;
        if (Movable.instance.CharacterCanMove) {
            if (vector.x > 0.2f) {
                directionX = 1f;
                isFacing = 1f;
            }
            else if (vector.x < -0.2f) {
                directionX = -1f;
                isFacing = -1f;
            }
            else {
                directionX = 0f;
            }
        }
    }

    private void Update() {
        if (directionX != 0) {
            transform.localScale = new Vector3(directionX > 0 ? 1 : -1, 1, 1);
            pressingKey = true;
        }
        else {
            pressingKey = false;
        }

        desiredVelocity = new Vector2(directionX, 0f) * Mathf.Max(maxSpeed, 0f);

        animator.SetFloat("Speed", Mathf.Abs(desiredVelocity.x));
    }

    private void FixedUpdate() {
        onGround = ground.GetOnGround();
        velocity = body.velocity;
        Run();
    }

    private void Run() {
        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        deceleration = onGround ? maxDecceleration : maxAirDeceleration;
        turnSpeed = onGround ? maxTurnSpeed : maxAirTurnSpeed;

        if (pressingKey) {
            if (Mathf.Sign(directionX) != Mathf.Sign(velocity.x)) {
                maxSpeedChange = turnSpeed * Time.deltaTime;
            }
            else {
                maxSpeedChange = acceleration * Time.deltaTime;
            }
        }
        else {
            maxSpeedChange = deceleration * Time.deltaTime;
        }

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;
    }
}
