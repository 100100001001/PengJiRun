using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �÷��̾ ��� �� ����� ����� Ŭ��
    public AudioClip deathClip;
    // ���� ��
    public float jumpForce = 700f;

    // ���� ���� Ƚ��
    private int jumpCount = 0;
    // �÷��̾ �ٴڿ� ��Ҵ��� Ȯ��
    private bool isGrounded = false;
    // �÷��̾ �׾��� ��ҳ� = ��� ����
    private bool isDead = false;
    // ����� ������ٵ� ������Ʈ
    private Rigidbody2D playerRigidbody;
    // ����� ����� �ҽ� ������Ʈ
    private AudioSource playerAudio;
    // ����� �ִϸ����� ������Ʈ
    private Animator animator;

    void Start()
    {
        // ���������� �ʱ�ȭ ����
        // ���� ������Ʈ�κ��� ����� ������Ʈ���� ������ ������ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ������� �Է��� �����ϰ� �����ϴ� ó��
        // 1. ���� ��Ȳ�� �˸��� �ִϸ��̼��� ���
        // 2. ���콺 ���� Ŭ���� �����ϰ� ����
        // 3. ���콺 ���� ��ư�� ���� ������ ���� ����
        // 4. �ִ� ���� Ƚ���� �����ϸ� ������ ���ϱ� ����

        // ��� �� �� �̻� ó���� �������� �ʰ� ����
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
        // ��� ó��
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerRigidbody.velocity = Vector2.zero;

        isDead = true;

        GameManager.instance.OnPlayerDead();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ���ڸ��� �����ϴ� ó��
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �ٴڿ� ����ڸ��� ó��

        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ʈ���� �ݶ��̴��� ���� ��ֹ����� �浹 ����

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
