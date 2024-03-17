
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrolState : IState

{
    private Enemy enemy;
    private Vector2 direction;

    public EnemyPatrolState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        GeneratePatrolPoint();
        enemy.animator.Play("Walk");
    }

    public void OnUpdate()
    {
        //Determine if  are injured
        if (enemy.isHurt)
        {
            enemy.TransitionState(EnemyStateType.Hurt);
        }

        //Find player on patrol, switching to pursuit status
        enemy.GetPlayerTransform();

        if (enemy.player != null)
        {
            enemy.TransitionState(EnemyStateType.Chase);

        }

        //If the list of path points is empty, perform path calculation
        if (enemy.pathPointList == null || enemy.pathPointList.Count <= 0)
        {
            GeneratePatrolPoint();
        }
        else
        {
            //When the enemy reaches the current path point, increment the current index and perform a path calculation
            if (Vector2.Distance(enemy.transform.position, enemy.pathPointList[enemy.currentIndex]) <= 0.1f)
            {
                enemy.currentIndex++;
                //Arrival at patrol point
                if (enemy.currentIndex >= enemy.pathPointList.Count)
                {
                    enemy.TransitionState(EnemyStateType.Idle);
                }
                else
                {
                    direction = (enemy.pathPointList[enemy.currentIndex] - enemy.transform.position).normalized;
                    enemy.MovementInput = direction;
                }
            }
            else
            {//Collision handling
                if (enemy.rb.velocity.magnitude < enemy.currentSpeed && enemy.currentIndex < enemy.pathPointList.Count)
                {
                    if(enemy.rb.velocity.magnitude == 0)
                    {
                        direction = (enemy.pathPointList[enemy.currentIndex] - enemy.transform.position).normalized;
                        enemy.MovementInput = direction;
                    }
                    else
                    {
                        enemy.TransitionState(EnemyStateType.Idle);
                    }
                }
            }
        }
    }

    public void OnFixedUpdate()
    {
        enemy.Move();
    }

   
    
    public void OnExit()
    {
        
    }

    //Get random patrol points
    public void GeneratePatrolPoint()
    {
        while (true)
        {
            //Randomly select an index
            int i = Random.Range(0, enemy.patrolPoints.Length);
    
            //Exclude current index
            if (enemy.targetPointIndex != i )
            {
                enemy.targetPointIndex = i;
                break;
            }
        }
        //Give the patrol point to the Generate Path Point function
        enemy.GeneratePath(enemy.patrolPoints[enemy.targetPointIndex].position);
    }
}
