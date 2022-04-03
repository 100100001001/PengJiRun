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
    //private int jumpCount = 0;
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

    public AudioClip sparkClip;
    public AudioClip jumpClip;
    public AudioClip starClip;
    public AudioClip hpClip;
    public AudioClip potionClip;
    public AudioClip feverClip;
    public AudioClip endClip;

    private Transform playerTransform;
    public static bool feverTime = false;
    public float feverTimeCnt = 10f;
    public static bool potionTime = false;
    public float potionTimeCnt = 5f;

    void Start()
    {
        // 전역변수의 초기화 진행
        // 게임 오브젝트로부터 사용할 컴포넌트들을 가져와 변수에 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
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

        if (Input.GetMouseButtonDown(0)) //&& jumpCount < 3)
        {
            //jumpCount++;
            playerRigidbody.velocity = Vector2.zero;

            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            playerAudio.PlayOneShot(jumpClip);
        }

        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);

        if (potionTime)
        {
            feverTime = false;

            potionTimeCnt -= Time.deltaTime;
            if (potionTimeCnt < 0)
            {
                potionTimeCnt = 0;
                playerTransform.localScale = new Vector3(1, 1, 1);
                potionTime = false;
                feverTime = false;
                playerAudio.PlayOneShot(endClip);
            }
        }

        if (feverTime)
        {
            animator.SetBool("Fever", feverTime);
            potionTime = false;

            feverTimeCnt -= Time.deltaTime;
            if (feverTimeCnt < 0)
            {
                feverTimeCnt = 0;
                playerTransform.localScale = new Vector3(1, 1, 1);
                potionTime = false;
                feverTime = false;
                animator.SetBool("Fever", feverTime);
                playerAudio.PlayOneShot(endClip);
            }
        }
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

    void potionPlus()
    {
        playerTransform.localScale = new Vector3(2f, 2f, 2f);
        potionTime = true;
    }

    void potionMinus()
    {
        playerTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        potionTime = true;
    }

    void potionFever()
    {
        playerTransform.localScale = new Vector3(5f, 5f, 5f);
        feverTime = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿자마자 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            //jumpCount = 0;
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
                playerAudio.PlayOneShot(sparkClip);
                if (GameManager.instance.Crash()) Die();
                break;
            case "Star":
                playerAudio.PlayOneShot(starClip);
                GameManager.instance.AddScore(50);
                collision.gameObject.SetActive(false);
                break;
            case "HP":
                playerAudio.PlayOneShot(hpClip);
                GameManager.instance.HPPlus();
                collision.gameObject.SetActive(false);
                break;
            case "PotionPlus":
                playerAudio.PlayOneShot(potionClip);
                GameManager.instance.AddScore(10);
                potionPlus();
                collision.gameObject.SetActive(false);
                break;
            case "PotionMinus":
                playerAudio.PlayOneShot(potionClip);
                GameManager.instance.AddScore(10);
                potionMinus();
                collision.gameObject.SetActive(false);
                break;
            case "PotionFever":
                playerAudio.PlayOneShot(feverClip);
                GameManager.instance.AddScore(20);
                potionFever();
                collision.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
