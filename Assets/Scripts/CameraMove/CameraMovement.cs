using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 相机移动速度
    public float rotationSpeed = 30.0f; // 相机旋转速度

    void Update()
    {
        // 获取玩家的输入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 计算移动方向
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // 如果有输入，则移动相机
        if (moveDirection != Vector3.zero)
        {
            // 将移动方向转换为相对于相机的世界空间方向
            moveDirection = transform.TransformDirection(moveDirection);

            // 移动相机
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        // 处理相机旋转
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}

