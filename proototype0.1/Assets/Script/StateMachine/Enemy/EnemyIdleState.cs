using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    private Enemy enemy;

    private float Timer = 0;


    public EnemyIdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        enemy.animator.Play("Idle");
        enemy.rb.velocity = Vector2.zero;
    }

     public void OnUpdate()
    {
        if (enemy.isHurt)
        {
            enemy.TransitionState(EnemyStateType.Hurt);
        }


        enemy.GetPlayerTransform();

        if (enemy.player != null)
        {
            if (enemy.distance > enemy.attackDistance)
            {
                enemy.TransitionState(EnemyStateType.Chase);
            }
            else if (enemy.distance <= enemy.attackDistance)
            {
                enemy.TransitionState(EnemyStateType.Attack);
            }
        }
        else
        {
            if(Timer <= enemy.IdleDuration)
            {
                Timer += Time.deltaTime;
            }
            else
            {
                Timer = 0;
                enemy.TransitionState(EnemyStateType.Patrol);
            }
        }
    }
    
    public void OnFixedUpdate()
    {
        
    }
    
    public void OnExit()
    {
       
    }

    

   
}
