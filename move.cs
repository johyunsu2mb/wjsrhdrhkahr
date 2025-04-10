using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // SceneManager를 사용하기 위해 추가
using UnityEngine.UI;  // UI 텍스트를 사용하려면 이 네임스페이스를 추가

public class PlayerMove : MonoBehaviour
{
    const float SPEED_JUMP = 5.0f; // 점프 속도
    const float SPEED_MOVE = 3.0f; // 이동 속도

    Rigidbody2D rb;
    bool leftPressed = false;  // 왼쪽 키 눌림 여부
    bool rightPressed = false; // 오른쪽 키 눌림 여부

    Animator animator; // 애니메이터 컴포넌트

    public Text messageText; // 화면에 표시할 메시지 텍스트 (UI Text)
    private bool messageDisplayed = false;  // 메시지가 표시되었는지 여부

    // Start는 첫 번째 프레임 전에 호출됩니다.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D 컴포넌트 가져오기
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
    }

    // Update는 매 프레임마다 호출됩니다.
    void Update()
    {
        if (rb != null)
        {
            float dist = SPEED_MOVE * Time.deltaTime;  // 이동 거리 계산
            Vector2 pos = transform.position;  // 현재 위치 저장


            // 좌측 이동
            if (Input.GetKeyDown(KeyCode.LeftArrow))  // 왼쪽 화살표 키를 눌렀을 때
            {
                leftPressed = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))  // 왼쪽 화살표 키를 뗐을 때
            {
                leftPressed = false;
            }
            if (leftPressed)
            {
                pos.x -= dist;  // 왼쪽으로 이동
                transform.localScale = new Vector3(-2, 2, 2); // 왼쪽 방향으로 캐릭터 뒤집기
            }

            // 우측 이동
            if (Input.GetKeyDown(KeyCode.RightArrow))  // 오른쪽 화살표 키를 눌렀을 때
            {
                rightPressed = true;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))  // 오른쪽 화살표 키를 뗐을 때
            {
                rightPressed = false;
            }
            if (rightPressed)
            {
                pos.x += dist;  // 오른쪽으로 이동
                transform.localScale = new Vector3(2, 2, 2); // 오른쪽 방향으로 캐릭터 뒤집기
            }

            transform.position = pos;  // 새 위치로 갱신

            

            // 점프
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))  // 마우스 클릭 또는 위 화살표 키를 눌렀을 때
            {
                if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)  // 캐릭터가 땅에 있는지 확인 (y속도가 거의 0이면 땅에 있는 것으로 간주)
                {
                    Vector2 jumpVelocity = rb.linearVelocity;
                    jumpVelocity.y = SPEED_JUMP;  // y 속도를 점프 속도로 설정
                    rb.linearVelocity = jumpVelocity;   // 변경된 속도 적용
                }
            }
        }
    }

    // 충돌이 발생했을 때 호출되는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 "Enemy" 태그일 때 씬을 다시 로드
        if (collision.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.CompareTag("Respawn"))
        {
            if (messageText != null && !messageDisplayed)  // 메시지가 이미 표시되지 않은 경우
            {
                messageText.text = "게임을 클리어 했노";  // 문구 출력
                messageDisplayed = true;  // 메시지를 한 번만 표시하도록 플래그 설정
            }
        }
    }
}
