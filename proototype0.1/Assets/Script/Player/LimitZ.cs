using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitZ : MonoBehaviour
{
    public float zPosition = 0f; // ���Ƶ�Z��λ��

    // Update is called once per frame
    void Update()
    {
        // ��ȡ��ǰλ��
        Vector3 currentPosition = transform.position;

        // ��Z��λ������Ϊ�̶�ֵ
        currentPosition.z = zPosition;

        // ����λ��
        transform.position = currentPosition;
    }
}