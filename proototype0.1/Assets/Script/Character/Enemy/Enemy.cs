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

    private void Update()
    {
        if (player == null)
            return;
        float distance = Vector2.Distance(player.position, transform.position);
        
        if (distance < chaseDistance)
        {
            if(distance <= attackDistance)
            {
                OnMovementInput?.Invoke(Vector2.zero);//stop move
                OnAttack?.Invoke();
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
        if(collision.CompareTag("Player")) 
        {
            collision.GetComponent<Character>().TakeDamage(damage);
        
        }
    }
}
