using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObSpawner : MonoBehaviour
{
    // obstacle
    private GameObject[] ob;
    public GameObject obPrefab;
    public float obScaleMin = 1f;
    public float obScaleMax = 3f;
    private float xPos = 20f;
    public float obYMin = -3f;
    public float obYMax = 2f;
    private int obIdx = 0;
    public int obLen = 10;

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
        ob = new GameObject[obLen];

        for (int i = 0; i < obLen; i++)
        {
            ob[i] = Instantiate(obPrefab, poolPosition, Quaternion.identity);
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

            float obYPos = Random.Range(obYMin, obYMax);
            ob[obIdx].SetActive(false);
            ob[obIdx].SetActive(true);
            float obScale = Random.Range(obScaleMin, obScaleMax);
            ob[obIdx].transform.localScale = new Vector3(obScale, obScale, obScale);
            ob[obIdx].transform.position = new Vector2(xPos, obYPos);

            obIdx++;

            if (obIdx >= obLen)
            {
                obIdx = 0;
            }
        }
    }
}
