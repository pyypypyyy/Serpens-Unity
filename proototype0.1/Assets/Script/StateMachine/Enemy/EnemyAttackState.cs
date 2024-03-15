using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    private Enemy enemy;
    private AnimatorStateInfo info;

    public EnemyAttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        if (enemy.isAttack)
        {
            enemy.animator.Play("Attack");
            enemy.isAttack = false;
        }
       
    }

     public void OnUpdate()
    {
        enemy.rb.velocity = Vector2.zero;

        float x =enemy.player.position.x - enemy.transform.position.x;
        if (x > 0 )
        {
            enemy.sr.flipX = true;
        }
        else
        {
            enemy.sr.flipX = false;
        }
        //Get information about the current animation state of the enemy character
        info = enemy.animator.GetCurrentAnimatorStateInfo(0);

        if ( info.normalizedTime >= 1f)
        {
            enemy.TransitionState(EnemyStateType.Idle);
        }
    }
    
     public void OnFixedUpdate()
    {
        
    }
    public void OnExit()
    {
       
    }

   

   
}
