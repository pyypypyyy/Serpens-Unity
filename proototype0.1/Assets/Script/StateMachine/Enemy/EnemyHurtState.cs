using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : IState
{
    private Enemy enemy;
    private Vector2 direction;//knockback direction
    private float Timer;

    public EnemyHurtState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        enemy.animator.Play("Hurt");
    }
    public void OnUpdate()
    {
        if(enemy.isKnockback)
        {
            if(enemy.player != null)
            {
                direction = (enemy.transform.position - enemy.player.position).normalized;

            }
            else
            {
                Transform player = GameObject.FindWithTag("Player").transform;
                direction = (enemy.transform.position - enemy.player.position).normalized;
            }
        }
    }
   

    public void OnFixedUpdate()
    {
        if (Timer <= enemy.knockbackForceDuration)
        {
            enemy.rb.AddForce(direction * enemy.knockbackForce, ForceMode2D.Impulse);
            Timer += Time.fixedDeltaTime;

        }
        else
        {
            Timer = 0;
            enemy.isHurt = false;
            enemy.TransitionState(EnemyStateType.Idle);
        }
    }

    public void OnExit()
    {
        enemy.isHurt = false;
    }
}
    
