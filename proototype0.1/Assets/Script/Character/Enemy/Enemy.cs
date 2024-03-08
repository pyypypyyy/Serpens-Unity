using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character
{
    public float damage;

    public UnityEvent<Vector2> OnMovementInput;
    public UnityEvent OnAttack;

    [SerializeField] private Transform player;
    [SerializeField] private float chaseDistance = 3f;
    [SerializeField] private float attackDistance = 0.8f;

    [Header("attack")]
    public float meleeAttackDamage;
    public LayerMask playerLayer;
    public float AttackCooldownDuration = 2f;

    private bool isAttack = true;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (player == null)
            return;
        float distance = Vector2.Distance(player.position, transform.position);
        
        if (distance < chaseDistance)
        {
            if(distance <= attackDistance)
            {
                //attack player
                OnMovementInput?.Invoke(Vector2.zero);//stop move
                if(isAttack)
                {
                    isAttack = false;
                    OnAttack?.Invoke();
                    //StartCoroutine(nameof(AttackCooldownCoroutine));
                }
                
                //player flip
                float x = player.position.x - transform.position.x;
                if(x > 0)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX=false;
                }
            }
            else
            {
                Vector2 direction = player.position - transform.position;
                OnMovementInput?.Invoke(direction.normalized);
            }
        }
        else
        {
            OnMovementInput?.Invoke(Vector2.zero);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().TakeDamage(damage);

        }
    }

    private void MeleeAttackAnimEvent()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackDistance, playerLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            hitCollider.GetComponent<Character>().TakeDamage(meleeAttackDamage);
        }
    }

    IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(AttackCooldownDuration);
        isAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

}
