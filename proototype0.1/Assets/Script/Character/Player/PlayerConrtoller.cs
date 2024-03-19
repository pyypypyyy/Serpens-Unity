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

    public float dashSpeed = 10f; // 冲刺速度
    public float dashDuration = 0.5f; // 冲刺持续时间

    public bool isMeleeAttack;
    public bool isDashing; // 是否正在进行冲刺



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
        inputActions.GamePlay.Dash.started += ctx => Dash();

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
        float speed = isDashing ? dashSpeed : moveSpeed;

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

    private void Dash()
    {
        // 如果正在冲刺或者已经死亡，则不执行冲刺操作
        if (isDashing || isDead)
            return;

        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        anim.SetTrigger("dash");

        // 冲刺持续一定时间后恢复正常速度
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
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
        anim.SetBool("isDashing", isDashing);
    }

    void SwitchActionMap(InputActionMap actionMap)
    {
        inputActions.Disable();
        actionMap.Enable();
    }
}