using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSpawner : MonoBehaviour
{
    // +HP
    private GameObject[] hpPlus;
    public GameObject hpPlusPrefab;
    public float hpXMin = -1.5f;
    public float hpXMax = 1.5f;
    public float hpYMin = -3f;
    public float hpYMax = 2f;
    private int hpIdx = 0;
    private int hpLen = 3;

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
        hpPlus = new GameObject[hpLen];

        for (int i = 0; i < hpLen; i++)
        {
            hpPlus[i] = Instantiate(hpPlusPrefab, poolPosition, Quaternion.identity);
        }

        // ������ ��ġ ���� �ʱ�ȭ
        lastSpawnTime = 0f;
        // ������ ��ġ������ �ð� ������ �ʱ�ȭ
        timeBetSpawn = 0f;
    }

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

            // +hp
            float hpXPos = Random.Range(hpXMin, hpXMax);
            float hpYPos = Random.Range(hpYMin, hpYMax);

            // +hp
            if (GameManager.instance.hpCount < 3 && Random.Range(0, 2) == 0)
            {
                hpPlus[hpIdx].SetActive(false);
                hpPlus[hpIdx].SetActive(true);
                hpPlus[hpIdx].transform.position = new Vector2(hpXPos, hpYPos);
                //hpPlusPrefab.SetActive(true);
                //hpPlusPrefab.transform.position = new Vector2(xPos, hpYPos);

                hpIdx++;

                if (hpIdx >= hpLen)
                {
                    hpIdx = 0;
                }
            }
        }
    }
}
