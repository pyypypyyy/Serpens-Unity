using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Character>().TakeDamage(damage);
            Destroy(gameObject); // 碰撞后销毁投射物
        }
    }
}