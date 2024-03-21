using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged Attack")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 5f;
    public float attackCooldown = 1f;
    private float attackTimer = 0f;

    protected override void Update()
    {
        base.Update();
        if (player != null)
        {
            // ��鹥����ȴʱ��
            if (attackTimer <= 0f && Vector2.Distance(transform.position, player.position) <= attackDistance)
            {
                Attack();
                attackTimer = attackCooldown; // ���ù�����ȴʱ��
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    private void Attack()
    {
        // ����������Զ�̹���
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (player.position - firePoint.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }
}
