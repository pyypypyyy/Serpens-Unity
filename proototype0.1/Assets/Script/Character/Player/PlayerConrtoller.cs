using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public InputActions inputActions;
    public Vector2 input;
    public float normalSpeed = 3f;
    public float attackSpeed = 1f;
    private float currentSpeed;

    public bool isMeleeAttack;

    public bool isDodging;
    public float dodgeForce;
    public float dodgeTimer = 0f;
    public float dodgeDuration = 0f;
    public float dodgeCooldown = 2f;
    private bool isDodgeOnCooldown = false;

    public AudioSource moveSound;
    public AudioSource attackSound;
    public AudioSource dodgeSound;


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
        inputActions.GamePlay.Dodge.started += isDodge;

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
        if (!isMeleeAttack)
            Dodge();
    }

    void Move()
    {
        //Set current speed according to attack status
        currentSpeed = isMeleeAttack ? attackSpeed : normalSpeed;

        rb.velocity = input * currentSpeed;

        if (!moveSound.isPlaying && rb.velocity.magnitude > 0.1f)
            moveSound.Play();

        if (input.x > 0)//left
        {
            sr.flipX = true;
        }
        if(input.x < 0)//right
        {
            sr.flipX = false;   
        }
    }

    void Dodge()
    {
        if (isDodgeOnCooldown)
        {
            dodgeTimer += Time.fixedDeltaTime;
            if (dodgeTimer >= dodgeCooldown)
            {
                isDodgeOnCooldown = false;
                dodgeTimer = 0;
                
            }
        }
        if (isDodging)
        {
            if (!isDodgeOnCooldown)
            {
                if (dodgeTimer <= dodgeDuration) 
                {
                    rb.AddForce(input * dodgeForce, ForceMode2D.Impulse);

                    dodgeTimer += Time.fixedDeltaTime;
                }
                else
                {
                    isDodging = false;
                    isDodgeOnCooldown = true;
                    dodgeTimer = 0f;
                }

            }          
        }
    }

    

    private void MeleeAttack(InputAction.CallbackContext obj)
    {
        if (!isDodging && !isDodgeOnCooldown)
        { 
            anim.SetTrigger("meleeAttack");
            isMeleeAttack = true;

          
        }
       
    }
    private void isDodge(InputAction.CallbackContext obj)
    {
        if (!isDodgeOnCooldown)
        {
            isDodging = true;
            dodgeSound.Play();
        }
        
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
        StartCoroutine(DelayedSceneLoad("Level_1", 3.0f));
    }

    IEnumerator DelayedSceneLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay time
        SceneManager.LoadScene(sceneName);      // Load the specified scene
    }
    void SetAnimation()
    {
        anim.SetBool("isDodge", isDodging);
        anim.SetFloat("speed", rb.velocity.magnitude);
        anim.SetBool("isMeleeAttack", isMeleeAttack);
        anim.SetBool("isDead", isDead);
        
    }

    public void PlayAttackSound()
    {
        attackSound.Play();
    }

    void SwitchActionMap(InputActionMap actionMap)
    {
        inputActions.Disable();
        actionMap.Enable();
    }
}