using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    private Enemy enemy;

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
    }
    
    public void OnFixedUpdate()
    {
        
    }
    
    public void OnExit()
    {
       
    }

    

   
}
