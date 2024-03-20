using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ��ɫ�� Transform ���
    public Vector3 offset; // ������ɫ֮���ƫ����

    void LateUpdate()
    {
        if (target != null)
        {
            // ���������Ŀ��λ��
            Vector3 desiredPosition = target.position + offset;

            // ʹ�� SmoothDamp ����ʹ���ƽ���ƶ���Ŀ��λ��
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
        }
    }
}