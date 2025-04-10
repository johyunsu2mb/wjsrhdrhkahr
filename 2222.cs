using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    public float mouseSensitivity = 400f; //���콺����

    private float MouseY;
    private float MouseX;

    void Update()
    {
        Rotate();
    }
    private void Rotate()
    {

        MouseX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;

        MouseY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        MouseY = Mathf.Clamp(MouseY, -90f, 90f); //Clamp�� ���� �ּҰ� �ִ밪�� ���� �ʵ�����

        transform.localRotation = Quaternion.Euler(MouseY, MouseX, 0f);// �� ���� �Ѳ����� ���
    }
}