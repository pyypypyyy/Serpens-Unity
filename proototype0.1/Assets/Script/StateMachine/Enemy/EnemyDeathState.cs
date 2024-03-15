using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : IState
{
    private Enemy enemy;

    public EnemyDeathState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        enemy.animator.Play("Die");
        enemy.rb.velocity = Vector2.zero;
        enemy.enemyCollider.enabled = false;
    }
    public void OnUpdate()
    {

    }
   
     public void OnFixedUpdate()
    {

    }
    public void OnExit()
    {
    }


}
