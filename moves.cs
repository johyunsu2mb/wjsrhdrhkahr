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
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서를 화면 안에서 고정
        Cursor.visible = false;                     // 마우스 커서를 보이지 않도록 설정

        rb = GetComponent<Rigidbody>();             // Rigidbody 컴포넌트 가져오기
        rb.freezeRotation = true;                   // Rigidbody의 회전을 고정하여 물리 연산에 영향을 주지 않도록 설정

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

        yRotation += mouseX;    // 마우스 X축 입력에 따라 수평 회전 값을 조정
        xRotation += -mouseY;    // 마우스 Y축 입력에 따라 수직 회전 값을 조정

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 수직 회전 값을 -90도에서 90도 사이로 제한

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // 카메라의 회전을 조절
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // 플레이어 캐릭터의 회전을 조절
    }
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce / 2, ForceMode.Impulse); // 힘을 가해 점프
    }
    void Move()
    {
        h = Input.GetAxisRaw("Horizontal"); // 수평 이동 입력 값
        v = Input.GetAxisRaw("Vertical");   // 수직 이동 입력 값

        // 입력에 따라 이동 방향 벡터 계산
        Vector3 moveVec = transform.forward * v + transform.right * h;

        // 이동 벡터를 정규화하여 이동 속도z와 시간 간격을 곱한 후 현재 위치에 더함
        transform.position += moveVec.normalized * moveSpeed * Time.deltaTime;
    }
}