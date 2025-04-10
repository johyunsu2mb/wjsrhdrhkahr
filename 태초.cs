using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 씬 관리 클래스(씬 생성, 씬 로드, 씬 합치기, 여러 씬 중 현재 active 되어있는 씬이 무엇인지 설정)
using UnityEngine.SceneManagement;

// Player가 떨어질 때 시작 위치로 돌려놓기
public class 태초 : MonoBehaviour
{
    // OutArea에 물체 닿았을 때 자동 호출
    void OnTriggerEnter(Collider col)
    {
        // 충돌된 물체가 Player면
        if (col.gameObject.tag == "Player")
        {
            // 현재 씬 다시 로드하기(처음부터 다시 시작하기)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}