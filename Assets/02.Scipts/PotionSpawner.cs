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

    // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMin = 1.25f;
    // 다음 배치까지의 시간 간격 최댓값
    public float timeBetSpawnMax = 2.25f;
    // 다음 배치까지의 시간 간격
    private float timeBetSpawn;
    // 초반에 생성한 발판을 화면 밖에 숨겨둘 위치
    private Vector2 poolPosition = new Vector2(0, -25);
    // 마지막 배치 시점
    private float lastSpawnTime;

    void Start()
    {
        potions = new GameObject[potionLen];

        for (int i = 0; i < potionLen; i++)
        {
            potions[i] = Instantiate(potionPrefab, poolPosition, Quaternion.identity);
        }

        // 마지막 배치 시점 초기화
        lastSpawnTime = 0f;
        // 다음번 배치까지의 시간 간격을 초기화
        timeBetSpawn = 0f;
    }

    void Update()
    {

        // 게임오버 상태에서는 동작하지 않음
        if (GameManager.instance.isGameover || PlayerController.feverTime || PlayerController.potionTime) return;

        // 마지막 배치 시점에서 timeBetSpawn 이상 시간이 흘렀다면,
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // 기록된 마지막 배치 시점을 현재 시점으로 갱신
            lastSpawnTime = Time.time;

            // 다음 배치까지의 시간 간격을 timeBetSpawnMin, timeBetSpawnMax 사이에서 랜덤 가져오기
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
