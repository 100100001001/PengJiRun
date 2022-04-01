using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �����ϰ� �ֱ������� ���ġ�ϴ� ��ũ��Ʈ
public class PlatformSpawner : MonoBehaviour
{
    // ������ ������ ���� ������
    public GameObject platformPrefab;
    // ������ ���� ��
    public int count = 5;

    // ���� ��ġ������ �ð� ���� �ּڰ�
    public float timeBetSpawnMin = 1.25f;
    // ���� ��ġ������ �ð� ���� �ִ�
    public float timeBetSpawnMax = 2.25f;
    // ���� ��ġ������ �ð� ����
    private float timeBetSpawn;

    // ��ġ�� ��ġ�� �ּ� y��
    public float yMin = -5f;
    // ��ġ�� ��ġ�� �ִ� y��
    public float yMax = 1.5f;
    // ��ġ�� ��ġ�� x��
    private float xPos = 20f;

    // �̸� ������ ���ǵ��� ������ �迭
    private GameObject[] platforms;
    // ����� ���� ������ ����
    private int currentIndex = 0;

    // �ʹݿ� ������ ������ ȭ�� �ۿ� ���ܵ� ��ġ
    private Vector2 poolPosition = new Vector2(0, -25);
    // ������ ��ġ ����
    private float lastSpawnTime;

    // +HP
    //public GameObject hpPlusPrefab;
    //private GameObject[] hpPlus;
    //public float hpXMin = -1.5f;
    //public float hpXMax = 1.5f;

    void Start()
    {
        // ������ �ʱ�ȭ�ϰ� ����� ������ �̸� ����

        // count��ŭ�� ������ ������ ���ο� ���� �迭 ����
        platforms = new GameObject[count];
        //hpPlus = new GameObject[1];

        // count��ŭ �����ϸ鼭 ���� ����s
        for (int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }

        //hpPlus[0] = Instantiate(hpPlusPrefab, poolPosition, Quaternion.identity);

        // ������ ��ġ ���� �ʱ�ȭ
        lastSpawnTime = 0f;
        // ������ ��ġ������ �ð� ������ �ʱ�ȭ
        timeBetSpawn = 0f;
    }

    void Update()
    {
        // ������ ���ư��� �ֱ��� ������ ��ġ

        // ���ӿ��� ���¿����� �������� ����
        if (GameManager.instance.isGameover) return;

        // ������ ��ġ �������� timeBetSpawn �̻� �ð��� �귶�ٸ�,
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // ��ϵ� ������ ��ġ ������ ���� �������� ����
            lastSpawnTime = Time.time;

            // ���� ��ġ������ �ð� ������ timeBetSpawnMin, timeBetSpawnMax ���̿��� ���� ��������
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            // ��ġ�� ��ġ�� ���̸� yMin�� yMax ���̿��� ���� ��������
            float yPos = Random.Range(yMin, yMax);

            // +hp
            //float hpXPos = Random.Range(hpXMin, hpXMax);
            //float hpYPos = Random.Range(yPos, yMax) + 3f;


            // ����� ���� ������ ���� ���� ������Ʈ�� ��Ȱ��ȭ�ϰ� �ٷ� ��� �ٽ� Ȱ��ȭ.
            // �� ��, ������ Platform ������Ʈ�� OnEnable() �޼��尡 �����
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            // ���� ������ ������ ȭ�� �����ʿ� ���ġ
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

            // +hp
            //hpPlus[currentIndex].SetActive(false);
            //hpPlus[currentIndex].SetActive(true);
            //hpPlus[currentIndex].transform.position = new Vector2(hpXPos, hpYPos);
            

            // ���� �ѱ��
            currentIndex++;

            // ������ ������ �����ߴٸ�
            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}
