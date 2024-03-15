using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IState
{
    private Enemy enemy;

    public EnemyChaseState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        enemy.animator.Play("Walk");
    }

     public void OnUpdate()
    {
        enemy.GetPlayerTransform();

        enemy.AutoPath();

        if(enemy != null)
        {
            //Determine if the list of path points is empty
            if (enemy.pathPointList == null || enemy.pathPointList.Count <= 0)
                return;
            //Whether in range of attack
            if (enemy.distance <= enemy.attackDistance)
            {
                enemy.TransitionState(EnemyStateType.Attack);
            }

            else
            {
                //Chase player
                Vector2 direction = (enemy.pathPointList[enemy.currentIndex] - enemy.transform.position).normalized;
                enemy.MovementInput = direction;
            }
        }

    }
    
     public void OnFixedUpdate()
    {
        Move();
    }
    public void OnExit()
    {
       
    }

    void Move()
    {
        if (enemy.MovementInput.magnitude > 0.1f && enemy.currentSpeed >= 0)
        {
            enemy.rb.velocity = enemy.MovementInput * enemy.currentSpeed;
            if (enemy.MovementInput.x < 0)//left
            {
                enemy.sr.flipX = false;
            }
            if (enemy.MovementInput.x > 0)//right
            {
                enemy.sr.flipX = true;
            }
        }
        else
        {
            enemy.rb.velocity = Vector2.zero;
        }
    }


}
