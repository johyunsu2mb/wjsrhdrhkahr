using UnityEngine;

public class move : MonoBehaviour
{
    Rigidbody rb;

    [Header("Rotate")]
    public float mouseSpeed;
    float yRotation;
    float xRotation;
    Camera cam;
    [Header("Jump")]
    public float jumpForce;

    [Header("Ground Check")]
    public float playerHeight;
    bool grounded;
    [Header("Move")]
    public float moveSpeed;
    float h;
    float v;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // ���콺 Ŀ���� ȭ�� �ȿ��� ����
        Cursor.visible = false;                     // ���콺 Ŀ���� ������ �ʵ��� ����

        rb = GetComponent<Rigidbody>();             // Rigidbody ������Ʈ ��������
        rb.freezeRotation = true;                   // Rigidbody�� ȸ���� �����Ͽ� ���� ���꿡 ������ ���� �ʵ��� ����

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;    // ���콺 X�� �Է¿� ���� ���� ȸ�� ���� ����
        xRotation += -mouseY;    // ���콺 Y�� �Է¿� ���� ���� ȸ�� ���� ����

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // ���� ȸ�� ���� -90������ 90�� ���̷� ����

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // ī�޶��� ȸ���� ����
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // �÷��̾� ĳ������ ȸ���� ����
    }
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce / 2, ForceMode.Impulse); // ���� ���� ����
    }
    void Move()
    {
        h = Input.GetAxisRaw("Horizontal"); // ���� �̵� �Է� ��
        v = Input.GetAxisRaw("Vertical");   // ���� �̵� �Է� ��

        // �Է¿� ���� �̵� ���� ���� ���
        Vector3 moveVec = transform.forward * v + transform.right * h;

        // �̵� ���͸� ����ȭ�Ͽ� �̵� �ӵ�z�� �ð� ������ ���� �� ���� ��ġ�� ����
        transform.position += moveVec.normalized * moveSpeed * Time.deltaTime;
    }
}