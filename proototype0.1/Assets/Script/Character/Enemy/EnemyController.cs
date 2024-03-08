using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("attributes")]
    [SerializeField] private float currentSpeed = 0;

    public Vector2 MovementInput {  get; set; }

    [Header("attack")]
    [SerializeField] private bool isAttack = true;
    [SerializeField] private float attackCoolDuration = 1f;

    [Header("knockback")]
    [SerializeField] private bool isKonckback = true;
    [SerializeField] private float KonckbackForce = 0.1f;
    [SerializeField] private float KonckbackForceDuration = 0.1f;


    private Rigidbody2D rb;
    private Collider2D enemyCollider;
    private SpriteRenderer sr;
    private Animator anim;

    private bool isDead;
    private bool isHurt;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();    
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead)
        Move();
        SetAnimation();
    }

    void Move()
    {
        if(MovementInput.magnitude > 0.1f && currentSpeed >= 0)
        {
            rb.velocity = MovementInput * currentSpeed;
            if(MovementInput.x < 0 )//left
            {
                sr.flipX = false;
            }
            if(MovementInput.x > 0 )//right
            {
                sr.flipX = true;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Attack()
    {
        if(isAttack)
        {
            isAttack = false;
            StartCoroutine(nameof(AttackCoroutine));
        }
    }

    IEnumerator AttackCoroutine()
    {
        anim.SetTrigger("attack");

        yield return new WaitForSeconds(attackCoolDuration);
        isAttack = true;
    }

    public void EnemyHurt()
    {
        isHurt = true;
        anim.SetTrigger("hurt");
    }

    public void Knockback(Vector3 pos)
    {
        if(!isKonckback || isDead)
        {
            return;
        }
        StartCoroutine(KnockbackCoroutine(pos));
    }

    IEnumerator KnockbackCoroutine(Vector3 pos)
    {
        var direction = (transform.position - pos).normalized;
        rb.AddForce(direction * KonckbackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(KonckbackForceDuration);
        isHurt = false;
    }
    public void EnemyDead()
    {
        rb.velocity = Vector2.zero;
        isDead = true;
        enemyCollider.enabled = false;
    }
    void SetAnimation()
    {
        anim.SetBool("isWalk", MovementInput.magnitude > 0);
        anim.SetBool("isDead", isDead);
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

}
