using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent attackAction;

    [SerializeField]
    private UnityEvent<Vector2> moveAction;

    [SerializeField]
    private UnityEvent createAction;

    [SerializeField]
    private UnityEvent jumpAction;

    [SerializeField]
    private UnityEvent jumpCancelAction;


    [SerializeField]
    private UnityEvent dashAction;

    private static InputManager instance;

    PlayerControler controler;



    private float startTime;
    private bool canCall = true;

    private void Awake()
    {
        controler = new PlayerControler();
    }
    private void OnEnable()
    {
        controler.Enable();
    }
    private void OnDisable()
    {
        controler.Disable();
    }
    private void Attack(InputAction.CallbackContext context)
    {
        if (canCall)
        {
            attackAction.Invoke();
            StartCoroutine(waitTime());
            canCall = false;
        }
    }
    private void Create(InputAction.CallbackContext context)
    {
        createAction.Invoke();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        jumpAction.Invoke();
    }

    private void Dash(InputAction.CallbackContext context)
    {
        dashAction.Invoke();
    }


    private void JumpCancel(InputAction.CallbackContext context)
    {
        jumpCancelAction.Invoke();
    }

    private void Start()
    {
        controler.PlayerControls.Attack.performed += Attack;
        controler.PlayerControls.CreateBlock.performed += Create;
        controler.PlayerControls.Jump.started += Jump;
        controler.PlayerControls.Jump.canceled += JumpCancel;
        controler.PlayerControls.Dash.performed += Dash;
    }
    // Update is called once per frame
    void Update()
    {
        moveAction.Invoke(controler.PlayerControls.Move.ReadValue<Vector2>());
    }

    IEnumerator waitTime()
    {
        
        if (PlayerAttack.instance != null)
        {
            yield return new WaitForSeconds(PlayerAttack.instance.attackSpeed);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
        canCall = true;
    }
}
