using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitZ : MonoBehaviour
{
    public float zPosition = 0f; // 限制的Z轴位置

    // Update is called once per frame
    void Update()
    {
        // 获取当前位置
        Vector3 currentPosition = transform.position;

        // 将Z轴位置设置为固定值
        currentPosition.z = zPosition;

        // 更新位置
        transform.position = currentPosition;
    }
}