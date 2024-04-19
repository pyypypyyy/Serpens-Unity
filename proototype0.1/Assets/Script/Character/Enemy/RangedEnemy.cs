using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged Attack")]
    public GameObject projectilePrefab; // 远程攻击物体预制体
    public Transform firePoint; // 发射点
    public float projectileSpeed = 5f; // 子弹速度
    public float attackCooldown = 1f; // 攻击冷却时间
    private float attackTimer = 0f; // 攻击计时器

    protected override void Update()
    {
        base.Update();
        if (player != null)
        {
            // 检查攻击冷却时间并且在合适的距离内发射远程攻击
            if (attackTimer <= 0f && Vector2.Distance(transform.position, player.position) <= attackDistance)
            {
                Attack(); // 发动远程攻击
                attackTimer = attackCooldown; // 重置攻击冷却时间
            }
            else
            {
                attackTimer -= Time.deltaTime; // 更新攻击冷却时间
            }
        }
    }

    private void Attack()
    {
        // 创建并发射远程攻击
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (player.position - firePoint.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }
}
