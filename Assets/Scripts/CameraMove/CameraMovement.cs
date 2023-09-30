using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // ����ƶ��ٶ�
    public float rotationSpeed = 30.0f; // �����ת�ٶ�

    void Update()
    {
        // ��ȡ��ҵ�����
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �����ƶ�����
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // ��������룬���ƶ����
        if (moveDirection != Vector3.zero)
        {
            // ���ƶ�����ת��Ϊ��������������ռ䷽��
            moveDirection = transform.TransformDirection(moveDirection);

            // �ƶ����
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        // ���������ת
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

