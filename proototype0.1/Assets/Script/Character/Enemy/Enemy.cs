using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

//Enumeration of enemy states
public enum EnemyStateType
{
    Idle, Patrol, Chase, Attack, Hurt, Death
}

public class Enemy : Character
{
    [Header("target")]
    public Transform player;

    [Header("patrol")]
    public float IdleDuration;
    public Transform[] patrolPoints;
    public int targetPointIndex = 0;

    [Header("move chase")]
    [SerializeField] public float currentSpeed = 0;

    public Vector2 MovementInput { get; set; }

    
    public float chaseDistance = 3f;
    public float attackDistance = 0.8f;

    private Seeker seeker;
    [HideInInspector] public List<Vector3> pathPointList;
    [HideInInspector] public int currentIndex = 0;//Pathpoint Index
    private float pathGenerateInterval = 0.5f;//Paths produced every 0.5 seconds
    private float pathGenerateTimer = 0f;

    [Header("attack")]
    public float meleeAttackDamage;
    public bool isAttack = true;
    [HideInInspector] public float distance;
    public LayerMask playerLayer;
    public float AttackCooldownDuration = 2f;

    [Header("hurt")]
    public bool isHurt;
    public bool isKnockback = true;
    public float knockbackForce = 10f;
    public float knockbackForceDuration = 0.1f;

    public AudioSource attackAudioSource;
    public AudioSource hurtAudioSource;


    public float damage;
    
    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Collider2D enemyCollider;

    private IState currentState;

    //Dictionary
    private Dictionary<EnemyStateType, IState> states = new Dictionary<EnemyStateType, IState>();

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        //Instantiating the enemy state
        states.Add(EnemyStateType.Idle, new EnemyIdleState(this));
        states.Add(EnemyStateType.Chase, new EnemyChaseState(this));
        states.Add(EnemyStateType.Attack, new EnemyAttackState(this));
        states.Add(EnemyStateType.Hurt, new EnemyHurtState(this));
        states.Add(EnemyStateType.Death, new EnemyDeathState(this));
        states.Add(EnemyStateType.Patrol, new EnemyPatrolState(this));

        TransitionState(EnemyStateType.Idle);
    }

    //Switch Enemy Status
    public void TransitionState(EnemyStateType type)
    {
        if (currentState != null)
        {
            currentState.OnExit();

        }
        //Find the corresponding state by dictionary key, enter a new state
        currentState = states[type];
        currentState.OnEnter();
    }

    private void Start()
    {
        EnemyManager.Instance.enemyCount++;
    }

    private void OnDestroy()
    {
        EnemyManager.Instance.enemyCount--;
    }
    protected virtual void Update()
    {
        currentState.OnUpdate();

        #region animation state machine
        //if (player == null)
        //    return;
        //float distance = Vector2.Distance(player.position, transform.position);

        //if (distance < chaseDistance)
        //{
        //    AutuoPath();

        //    if (pathPointList == null)
        //        return;

        //    if (distance <= attackDistance)
        //    {
        //        attack player
        //        OnMovementInput?.Invoke(Vector2.zero);//stop move
        //        if (isAttack)
        //        {
        //            isAttack = false;
        //            OnAttack?.Invoke();

        //        }

        //        player flip
        //        float x = player.position.x - transform.position.x;
        //        if (x > 0)
        //        {
        //            sr.flipX = true;
        //        }
        //        else
        //        {
        //            sr.flipX = false;
        //        }
        //    }
        //    else
        //    {

        //        Vector2 direction = (pathPointList[currentIndex] - transform.position).normalized;
        //        OnMovementInput?.Invoke(direction);
        //    }
        //}
        //else
        //{
        //    OnMovementInput?.Invoke(Vector2.zero);
        //}
        #endregion
    }

    private void FixedUpdate()
    {
        currentState.OnFixedUpdate();
    }

    //Determine if the player is in chase range
    public void GetPlayerTransform()
    {
        Collider2D[] chaseCollider = Physics2D.OverlapCircleAll(transform.position, chaseDistance, playerLayer);

        if (chaseCollider.Length > 0 )
        {
            player = chaseCollider[0].transform;
            distance = Vector2.Distance(player.position, transform.position);
            
        }
        else
        {
            player = null;
        }
    }

    //automatic pathfinding
    public void AutoPath()
    {
        if (player != null)
        {
            pathGenerateTimer += Time.deltaTime;
            //Get path points at regular intervals
            if (pathGenerateTimer >= pathGenerateInterval)
            {
                GeneratePath(player.position);
                pathGenerateTimer = 0;//reset timer
            }
            //Path calculation when path list is empty
            if (pathPointList == null || pathPointList.Count <= 0)
            {
                GeneratePath(player.position);
            }
            //When the enemy reaches the current path point, increment the index currentIndex for path calculation
            else if (Vector2.Distance(transform.position, pathPointList[currentIndex]) <= 0.1f)
            {
                currentIndex++;
                if (currentIndex >= pathPointList.Count)
                    GeneratePath(player.position);
            }
        }
    }
    //Get Path Points
    public void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        //Start, Finish, Callback Functions
        if (seeker != null)
        {
            //Start, Finish, Callback Functions
            seeker.StartPath(transform.position, target, Path =>
            {
                pathPointList = Path.vectorPath;
            });
        }

    }
    public void Move()
    {
        if (MovementInput.magnitude > 0.1f && currentSpeed >= 0)
        {
            rb.velocity = MovementInput * currentSpeed;
            if (MovementInput.x < 0)//left
            {
                sr.flipX = false;
            }
            if (MovementInput.x > 0)//right
            {
                sr.flipX = true;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
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

            attackAudioSource.Play();
        }
    }

    public void AttackCooldown()
    {
        StartCoroutine(nameof(AttackCooldownCoroutine));
    }

    IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(AttackCooldownDuration);
        isAttack = true;
    }

    public void EnemyHurt()
    {
        isHurt = true;

        hurtAudioSource.Play();
    }

    public void EnemyDie()
    {
        TransitionState(EnemyStateType.Death);
    }

    public void DestoryEnemy()
    {
        Destroy(this.gameObject); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

}
