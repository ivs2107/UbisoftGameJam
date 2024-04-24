using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBoost0G : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)] public float maxSpeed = 11f;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Boost(InputAction.CallbackContext context)
    {
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void DoABoost()
    {
        
    }
}
