using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        NormalState,
        FightingState,
        MovingState,
        RestingState,
    }

    private NavMeshAgent enemyAgent;
    private EnemySpawner spawner;

    private EnemyState state = EnemyState.NormalState;
    private EnemyState childstate = EnemyState.RestingState;
    
    public float restTime = 2;
    private float restTimer = 0;
    
    public int HP = 100;
    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.NormalState)
        {
            if(childstate == EnemyState.RestingState)
            {
                restTimer += Time.deltaTime;
                if(restTimer > restTime)
                {
                    Vector3 randomPosition = FindRandomPosition();
                    enemyAgent.SetDestination(randomPosition);
                    childstate = EnemyState.MovingState;
                }
            }
            else if(childstate == EnemyState.MovingState)
            {
                if (enemyAgent.remainingDistance <= 0)
                {
                    restTimer = 0;
                    childstate = EnemyState.RestingState;
                }
            }
        }
    }

    
    Vector3 FindRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1,1f), 0, Random.Range(-1, 1f));
        return transform.position + randomDir.normalized * Random.Range(2, 5);
    }

    public void SetSpawner(EnemySpawner _spawner)
    {
        spawner = _spawner;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            Destroy(this.gameObject);

            if (spawner != null)
            {
                spawner.DecreaseSpawnedEnemyCount();
            }
        }
    }
}
