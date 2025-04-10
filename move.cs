using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // SceneManager�� ����ϱ� ���� �߰�
using UnityEngine.UI;  // UI �ؽ�Ʈ�� ����Ϸ��� �� ���ӽ����̽��� �߰�

public class PlayerMove : MonoBehaviour
{
    const float SPEED_JUMP = 5.0f; // ���� �ӵ�
    const float SPEED_MOVE = 3.0f; // �̵� �ӵ�

    Rigidbody2D rb;
    bool leftPressed = false;  // ���� Ű ���� ����
    bool rightPressed = false; // ������ Ű ���� ����

    Animator animator; // �ִϸ����� ������Ʈ

    public Text messageText; // ȭ�鿡 ǥ���� �޽��� �ؽ�Ʈ (UI Text)
    private bool messageDisplayed = false;  // �޽����� ǥ�õǾ����� ����

    // Start�� ù ��° ������ ���� ȣ��˴ϴ�.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D ������Ʈ ��������
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
    }

    // Update�� �� �����Ӹ��� ȣ��˴ϴ�.
    void Update()
    {
        if (rb != null)
        {
            float dist = SPEED_MOVE * Time.deltaTime;  // �̵� �Ÿ� ���
            Vector2 pos = transform.position;  // ���� ��ġ ����


            // ���� �̵�
            if (Input.GetKeyDown(KeyCode.LeftArrow))  // ���� ȭ��ǥ Ű�� ������ ��
            {
                leftPressed = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))  // ���� ȭ��ǥ Ű�� ���� ��
            {
                leftPressed = false;
            }
            if (leftPressed)
            {
                pos.x -= dist;  // �������� �̵�
                transform.localScale = new Vector3(-2, 2, 2); // ���� �������� ĳ���� ������
            }

            // ���� �̵�
            if (Input.GetKeyDown(KeyCode.RightArrow))  // ������ ȭ��ǥ Ű�� ������ ��
            {
                rightPressed = true;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))  // ������ ȭ��ǥ Ű�� ���� ��
            {
                rightPressed = false;
            }
            if (rightPressed)
            {
                pos.x += dist;  // ���������� �̵�
                transform.localScale = new Vector3(2, 2, 2); // ������ �������� ĳ���� ������
            }

            transform.position = pos;  // �� ��ġ�� ����

            

            // ����
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))  // ���콺 Ŭ�� �Ǵ� �� ȭ��ǥ Ű�� ������ ��
            {
                if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)  // ĳ���Ͱ� ���� �ִ��� Ȯ�� (y�ӵ��� ���� 0�̸� ���� �ִ� ������ ����)
                {
                    Vector2 jumpVelocity = rb.linearVelocity;
                    jumpVelocity.y = SPEED_JUMP;  // y �ӵ��� ���� �ӵ��� ����
                    rb.linearVelocity = jumpVelocity;   // ����� �ӵ� ����
                }
            }
        }
    }

    // �浹�� �߻����� �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� "Enemy" �±��� �� ���� �ٽ� �ε�
        if (collision.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.CompareTag("Respawn"))
        {
            if (messageText != null && !messageDisplayed)  // �޽����� �̹� ǥ�õ��� ���� ���
            {
                messageText.text = "������ Ŭ���� �߳�";  // ���� ���
                messageDisplayed = true;  // �޽����� �� ���� ǥ���ϵ��� �÷��� ����
            }
        }
    }
}
