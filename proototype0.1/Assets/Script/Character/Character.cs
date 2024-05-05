using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("attributes")]
    [SerializeField]protected float maxHealth;
    [SerializeField]protected float currentHealth;

    [Header("invincible")]
    public bool invulnerable;
    public float invulnerableDuration;

    [Header("UI")]
    public UnityEvent<float, float> OnHealthUpdate;

    public UnityEvent onHurt;
    public UnityEvent onDeath;
    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
        OnHealthUpdate?.Invoke(maxHealth, currentHealth);
    }

    public virtual void TakeDamage(float damage)
    {
        if (invulnerable)
            return;
        if (currentHealth - damage> 0f)
        {
            currentHealth -= damage;
            StartCoroutine(nameof(InvulnerableCoroutine));
            //run hurt animation
            onHurt?.Invoke();
        }
        else
        {
            Die();
        }
        OnHealthUpdate?.Invoke(maxHealth, currentHealth);
    }

    public virtual void Die()
    {
        currentHealth = 0f;
        //run die animation
        onDeath?.Invoke();
    }


    protected virtual IEnumerator InvulnerableCoroutine()
    {
        invulnerable = true;

        //wait for the invulnerableduration
        yield return new WaitForSeconds(invulnerableDuration); 

        invulnerable = false;
    }
}
