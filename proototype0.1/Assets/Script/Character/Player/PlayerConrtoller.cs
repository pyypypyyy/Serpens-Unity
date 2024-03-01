using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    public InputActions inputActions;
    public Vector2 input;
    public float moveSpeed = 5f;

    public bool isMeleeAttack;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private float inputX, inputY;
    private float stopX, stopY;

    public bool isDead;

    private void Awake()
    {
        inputActions = new InputActions();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        inputActions.GamePlay.MeleeAttack.started += MeleeAttack;
        
    }

   

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        input = inputActions.GamePlay.Move.ReadValue<Vector2>().normalized; 
        SetAnimation();

    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = input * moveSpeed;

        if(input.x < 0)//left
        {
            sr.flipX = true;
        }
        if(input.x > 0)//right
        {
            sr.flipX = false;   
        }
    }
    private void MeleeAttack(InputAction.CallbackContext obj)
    {
        anim.SetTrigger("meleeAttack");
        isMeleeAttack = true;
    }

    public void PlayerHurt()
    {
        anim.SetTrigger("hurt");
    }

    public void PlayerDead()
    {
        isDead = true;
        //ban Gameplay input
        SwitchActionMap(inputActions.UI);//switch to UI inputmap
    }
    void SetAnimation()
    {
        anim.SetFloat("speed", rb.velocity.magnitude);
        anim.SetBool("isMeleeAttack", isMeleeAttack);
        anim.SetBool("isDead", isDead);
    }

    void SwitchActionMap(InputActionMap actionMap)
    {
        inputActions.Disable();
        actionMap.Enable();
    }
}