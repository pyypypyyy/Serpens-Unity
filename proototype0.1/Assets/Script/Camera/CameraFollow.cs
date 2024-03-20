using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 角色的 Transform 组件
    public Vector3 offset; // 相机与角色之间的偏移量

    void LateUpdate()
    {
        if (target != null)
        {
            // 计算相机的目标位置
            Vector3 desiredPosition = target.position + offset;

            // 使用 SmoothDamp 方法使相机平滑移动到目标位置
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
        }
    }
}