using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어가 사망 시 재생할 오디오 클립
    public AudioClip deathClip;
    // 점프 힘
    public float jumpForce = 700f;

    // 누적 점프 횟수
    private int jumpCount = 0;
    // 플레이어가 바닥에 닿았는지 확인
    private bool isGrounded = false;
    // 플레이어가 죽었냐 살았냐 = 사망 상태
    private bool isDead = false;
    // 사용할 리지드바디 컴포넌트
    private Rigidbody2D playerRigidbody;
    // 사용할 오디오 소스 컴포넌트
    private AudioSource playerAudio;
    // 사용할 애니메이터 컴포넌트
    private Animator animator;

    void Start()
    {
        // 전역변수의 초기화 진행
        // 게임 오브젝트로부터 사용할 컴포넌트들을 가져와 변수에 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 사용자의 입력을 감지하고 점프하는 처리
        // 1. 현재 상황에 알맞은 애니메이션을 재생
        // 2. 마우스 왼쪽 클릭을 감지하고 점프
        // 3. 마우스 왼쪽 버튼을 오래 누르면 높이 점프
        // 4. 최대 점프 횟수에 도달하면 점프를 못하기 막기

        // 사망 시 더 이상 처리를 진행하지 않고 종료
        if (isDead) return;

        if (Input.GetMouseButtonDown(0) && jumpCount < 3)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;

            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            playerAudio.Play();
        }

        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);
    }

    void Die()
    {
        // 사망 처리
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerRigidbody.velocity = Vector2.zero;

        isDead = true;

        GameManager.instance.OnPlayerDead();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿자마자 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에 벗어나자마자 처리

        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 트리거 콜라이더를 가진 장애물과의 충돌 감지

        if (isDead) return;
        switch (collision.tag)
        {
            case "Dead":
                Die();
                break;
            case "Spark":
                if (GameManager.instance.Crash()) Die();
                break;
            case "Star":
                GameManager.instance.AddScore(50);
                collision.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
