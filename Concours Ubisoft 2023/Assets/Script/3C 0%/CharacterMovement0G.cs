using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement0G : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)] public float maxSpeed = 11f;

    public Vector2 desiredVelocity;
    public Vector2 velocity;
    public Vector2 direction;
    private float directionX;
    private float directionY;
    public float isFacing;

    private bool pressingKey;

    private Rigidbody2D body;

    private ParticleSystem fireParticle;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        fireParticle = this.transform.GetComponentInChildren<ParticleSystem>();
    }

    public void OnMovement(Vector2 vector) {
        direction = vector;

        if (direction.x > 0f) {
            directionX = 1f;
            isFacing = 1f;
        }
        else if (direction.x < 0f) {
            directionX = -1f;
            isFacing = -1f;
        }

        if (direction.y > 0f) {
            directionY = 1f;
        }
        else if (direction.y < 0f) {
            directionY = -1f;
        }
    }

    private void Update() {
        if (direction != Vector2.zero) {
            transform.localScale = new Vector3(directionX > 0 ? 1 : -1, directionY >= 0 ? 1 : -1, 1);
            pressingKey = true;
        }
        else {
            pressingKey = false;
        }
        
        desiredVelocity = new Vector2(directionX, directionY) * Mathf.Max(maxSpeed, 0f);
          }

    private void FixedUpdate() {
        velocity = body.velocity;

        Fly();

        var emissionParticle = fireParticle.emission;
        emissionParticle.rateOverTime = (Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y) > 2 ? 50 : 0);

    }

    private void Fly() {
        if (pressingKey)
            body.AddForce(desiredVelocity, ForceMode2D.Force);

        if (velocity.x > maxSpeed)
        {
            velocity.x = maxSpeed;
            body.velocity = velocity;
        }

        if (velocity.x < -maxSpeed)
        {
            velocity.x = -maxSpeed;
            body.velocity = velocity;
        }

        if (velocity.y > maxSpeed)
        {
            velocity.y = maxSpeed;
            body.velocity = velocity;
        }

        if (velocity.y < -maxSpeed)
        {
            velocity.y = -maxSpeed;
            body.velocity = velocity;
        }
    }
}
