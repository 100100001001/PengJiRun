using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어가 사망 시 재생할 오디오 클립
    public AudioClip deathClip;
    // 점프 힘
    public float jumpForce = 700f;

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

    // 상황별로 재생되는 오디오 클립
    public AudioClip sparkClip;     // 장애물에 닿았을 때
    public AudioClip jumpClip;      // 점프
    public AudioClip starClip;      // 별에 닿았을 때
    public AudioClip hpClip;        // hp
    public AudioClip potionClip;    // 포션을 먹었을 때
    public AudioClip feverClip;     // fever 포션을 먹었을 때
    public AudioClip endClip;       // 포션의 효과가 끝났을 때

    private Transform playerTransform;
    public static bool potionTime = false;
    private float potionTimeCnt = 10f;
    public static bool feverTime = false;
    private float feverTimeCnt = 12f;

    // fever 포션을 먹었을 때 장애물, 발판 비활성화를 위해 게임 오브젝트 받아 오기
    public GameObject platformPrefab;
    public GameObject obPrefab;

    public SpriteRenderer spriteRenderer;
    int tmp = 0;

    void Start()
    {
        // 전역변수의 초기화 진행
        // 게임 오브젝트로부터 사용할 컴포넌트들을 가져와 변수에 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();

        platformPrefab.GetComponent<BoxCollider2D>().enabled = true;
        obPrefab.transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 사용자의 입력을 감지하고 점프하는 처리
        // 1. 현재 상황에 알맞은 애니메이션을 재생
        // 2. 마우스 왼쪽 클릭을 감지하고 점프
        // 3. 마우스 왼쪽 버튼을 오래 누르면 높이 점프

        // 사망 시 더 이상 처리를 진행하지 않고 종료
        if (isDead) return;

        // 점프
        if (Input.GetMouseButtonDown(0))
        {
            playerRigidbody.velocity = Vector2.zero;

            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            playerAudio.PlayOneShot(jumpClip);
            GameManager.instance.AddScore(1);
        }

        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);

        // 포션을 먹었을 때 시간 제한, 초기화
        if (potionTime)
        {
            potionTimeCnt -= Time.deltaTime;
            if (potionTimeCnt < 0)
            {
                potionTimeCnt = 10f;
                playerTransform.localScale = new Vector2(1, 1);
                potionTime = false;
                feverTime = false;
                playerAudio.PlayOneShot(endClip);
            }
        }

        // 피버타임 포션을 먹었을 때 시간 제한, 초기화
        if (feverTime)
        {
            animator.SetBool("Fever", feverTime);

            feverTimeCnt -= Time.deltaTime;
            Debug.Log((int)feverTimeCnt);
            tmp++;

            if (feverTimeCnt < 3)
            {
                if (tmp % 2 == 0)
                {
                    spriteRenderer.color = new Color32(255, 255, 255, 100);
                }
                else
                {
                    spriteRenderer.color = new Color32(255, 255, 255, 255);

                }
            }

            if (feverTimeCnt < 0)
            {
                feverTimeCnt = 12f;
                playerTransform.localScale = new Vector2(1, 1);
                potionTime = false;
                feverTime = false;
                animator.SetBool("Fever", feverTime);
                playerAudio.PlayOneShot(endClip);

                platformPrefab.GetComponent<BoxCollider2D>().enabled = true;
                obPrefab.transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = true;

                spriteRenderer.color = new Color32(255, 255, 255, 255);
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
        // 커지는 포션 먹었을 때

        animator.SetBool("Fever", false);
        playerTransform.localScale = new Vector2(2f, 2f);
        potionTime = true;
        feverTime = false;
    }

    void potionMinus()
    {
        // 작아지는 포션 먹었을 때

        animator.SetBool("Fever", false);
        playerTransform.localScale = new Vector2(0.5f, 0.5f);
        potionTime = true;
        feverTime = false;
    }

    void potionFever()
    {
        // 피버타임 포션 먹었을 때

        playerTransform.localScale = new Vector2(5f, 5f);
        feverTime = true;
        potionTime = false;

        platformPrefab.GetComponent<BoxCollider2D>().enabled = false;
        obPrefab.transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿자마자 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
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
                GameManager.instance.AddScore(10);
                collision.gameObject.SetActive(false);
                break;
            case "HP":
                playerAudio.PlayOneShot(hpClip);
                GameManager.instance.HPPlus();
                collision.gameObject.SetActive(false);
                break;
            case "PotionPlus":
                playerAudio.PlayOneShot(potionClip);
                GameManager.instance.AddScore(20);
                potionPlus();
                collision.gameObject.SetActive(false);
                break;
            case "PotionMinus":
                playerAudio.PlayOneShot(potionClip);
                GameManager.instance.AddScore(20);
                potionMinus();
                collision.gameObject.SetActive(false);
                break;
            case "PotionFever":
                playerAudio.PlayOneShot(feverClip);
                GameManager.instance.AddScore(40);
                potionFever();
                collision.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
