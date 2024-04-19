using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f; // �ӵ����˺�ֵ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����ӵ���ײ������ң��Ͷ��������˺�
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject); // �����ӵ�
        }
    }
}
