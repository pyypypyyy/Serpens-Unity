using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

public class Enemy : Character
{
    public float damage;

    public UnityEvent<Vector2> OnMovementInput;
    public UnityEvent OnAttack;

    [SerializeField] private Transform player;
    [SerializeField] private float chaseDistance = 3f;
    [SerializeField] private float attackDistance = 0.8f;

    private Seeker seeker;
    private List<Vector3> pathPointList;
    private int currentIndex = 0;//Pathpoint Index
    private float pathGenerateInterval = 0.5f;//Paths produced every 0.5 seconds
    private float pathGenerateTimer = 0f;

    [Header("attack")]
    public float meleeAttackDamage;
    public LayerMask playerLayer;
    public float AttackCooldownDuration = 2f;

    private bool isAttack = true;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        seeker = GetComponent<Seeker>();
    }
    private void Update()
    {
        if (player == null)
            return;
        float distance = Vector2.Distance(player.position, transform.position);
        
        if (distance < chaseDistance)
        {
            AutuoPath();

            if (pathPointList == null)
                return;

            if (distance <= attackDistance)
            {
                //attack player
                OnMovementInput?.Invoke(Vector2.zero);//stop move
                if(isAttack)
                {
                    isAttack = false;
                    OnAttack?.Invoke();
                    //StartCoroutine(nameof(AttackCooldownCoroutine));
                }
                
                //player flip
                float x = player.position.x - transform.position.x;
                if(x > 0)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX=false;
                }
            }
            else
            {
                //Vector2 direction = player.position - transform.position;
                Vector2 direction = (pathPointList[currentIndex] - transform.position).normalized;
                OnMovementInput?.Invoke(direction);
            }
        }
        else
        {
            OnMovementInput?.Invoke(Vector2.zero);
        }
    }
    //automatic pathfinding
    private void AutuoPath()
    {
        pathGenerateTimer += Time.deltaTime;
        //Get path points at regular intervals
        if (pathGenerateTimer >= pathGenerateInterval)
        {
            GeneratePath(player.position);
            pathGenerateTimer = 0;//reset timer
        }
        //Path calculation when path list is empty
        if (pathPointList == null || pathPointList.Count <= 0 )
        {
            GeneratePath(player.position);
        }
        //When the enemy reaches the current path point, increment the index currentIndex for path calculation
        else if (Vector2.Distance(transform.position, pathPointList[currentIndex]) <= 0.1f)
        {
            currentIndex++;
            if(currentIndex >= pathPointList.Count)
                GeneratePath(player.position);
        }
    }
    //Get Path Points
    private void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        //Start, Finish, Callback Functions
        seeker.StartPath(transform.position, target, Path =>
        {
            pathPointList = Path.vectorPath;
        });
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().TakeDamage(damage);

        }
    }

    private void MeleeAttackAnimEvent()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackDistance, playerLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            hitCollider.GetComponent<Character>().TakeDamage(meleeAttackDamage);
        }
    }

    IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(AttackCooldownDuration);
        isAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

}
