using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItemSpawner : MonoBehaviour
{
    // star
    private GameObject[] stars;
    public GameObject starPrefab;
    public float starXMin = -1.5f;
    public float starXMax = 1.5f;
    public float starYMin = -3f;
    public float starYMax = 2f;
    public int starIdx = 0;
    public int starLen = 3;

    // ���� ��ġ������ �ð� ���� �ּڰ�
    public float timeBetSpawnMin = 1.25f;
    // ���� ��ġ������ �ð� ���� �ִ�
    public float timeBetSpawnMax = 2.25f;
    // ���� ��ġ������ �ð� ����
    private float timeBetSpawn;
    // �ʹݿ� ������ ������ ȭ�� �ۿ� ���ܵ� ��ġ
    private Vector2 poolPosition = new Vector2(0, -25);
    // ������ ��ġ ����
    private float lastSpawnTime;

    void Start()
    {
        stars = new GameObject[starLen];

        for (int i = 0; i < starLen; i++)
        {
            stars[i] = Instantiate(starPrefab, poolPosition, Quaternion.identity);
        }

        // ������ ��ġ ���� �ʱ�ȭ
        lastSpawnTime = 0f;
        // ������ ��ġ������ �ð� ������ �ʱ�ȭ
        timeBetSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        // ���ӿ��� ���¿����� �������� ����
        if (GameManager.instance.isGameover) return;

        // ������ ��ġ �������� timeBetSpawn �̻� �ð��� �귶�ٸ�,
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // ��ϵ� ������ ��ġ ������ ���� �������� ����
            lastSpawnTime = Time.time;

            // ���� ��ġ������ �ð� ������ timeBetSpawnMin, timeBetSpawnMax ���̿��� ���� ��������
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            // stars
            float starXPos = Random.Range(starXMin, starXMax);
            float starYPos = Random.Range(starYMin, starYMax);
            stars[starIdx].SetActive(false);
            stars[starIdx].SetActive(true);
            stars[starIdx].transform.position = new Vector2(starXPos, starYPos);

            starIdx++;

            if (starIdx >= starLen)
            {
                starIdx = 0;
            }
        }
    }
}