using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f; // 子弹的伤害值

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果子弹碰撞到了玩家，就对玩家造成伤害
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject); // 销毁子弹
        }
    }
}
