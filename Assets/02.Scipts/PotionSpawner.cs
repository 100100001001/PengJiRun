using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    private GameObject[] potions;
    public GameObject potionPrefab;
    public float xPos = 20f;
    public float potionYMin = -3f;
    public float potionYMax = 2f;
    private int potionIdx = 0;
    private int potionLen = 3;

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
        potions = new GameObject[potionLen];

        for (int i = 0; i < potionLen; i++)
        {
            potions[i] = Instantiate(potionPrefab, poolPosition, Quaternion.identity);
        }

        // ������ ��ġ ���� �ʱ�ȭ
        lastSpawnTime = 0f;
        // ������ ��ġ������ �ð� ������ �ʱ�ȭ
        timeBetSpawn = 0f;
    }

    void Update()
    {

        // ���ӿ��� ���¿����� �������� ����
        if (GameManager.instance.isGameover || PlayerController.feverTime || PlayerController.potionTime) return;

        // ������ ��ġ �������� timeBetSpawn �̻� �ð��� �귶�ٸ�,
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // ��ϵ� ������ ��ġ ������ ���� �������� ����
            lastSpawnTime = Time.time;

            // ���� ��ġ������ �ð� ������ timeBetSpawnMin, timeBetSpawnMax ���̿��� ���� ��������
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            float potionYPos = Random.Range(potionYMin, potionYMax);

            potions[potionIdx].SetActive(false);
            potions[potionIdx].SetActive(true);
            potions[potionIdx].transform.position = new Vector2(xPos, potionYPos);

            potionIdx++;

            if (potionIdx >= potionLen)
            {
                potionIdx = 0;
            }
            
        }
    }
}
