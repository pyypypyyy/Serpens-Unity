using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged Attack")]
    public GameObject projectilePrefab; // Զ�̹�������Ԥ����
    public Transform firePoint; // �����
    public float projectileSpeed = 5f; // �ӵ��ٶ�
    public float attackCooldown = 1f; // ������ȴʱ��
    private float attackTimer = 0f; // ������ʱ��

    protected override void Update()
    {
        base.Update();
        if (player != null)
        {
            // ��鹥����ȴʱ�䲢���ں��ʵľ����ڷ���Զ�̹���
            if (attackTimer <= 0f && Vector2.Distance(transform.position, player.position) <= attackDistance)
            {
                Attack(); // ����Զ�̹���
                attackTimer = attackCooldown; // ���ù�����ȴʱ��
            }
            else
            {
                attackTimer -= Time.deltaTime; // ���¹�����ȴʱ��
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
