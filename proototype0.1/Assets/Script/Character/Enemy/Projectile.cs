using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Character>().TakeDamage(damage);
            Destroy(gameObject); // ��ײ������Ͷ����
        }
    }
}