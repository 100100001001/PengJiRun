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
        stars = new GameObject[starLen];

        for (int i = 0; i < starLen; i++)
        {
            stars[i] = Instantiate(starPrefab, poolPosition, Quaternion.identity);
        }

        // 마지막 배치 시점 초기화
        lastSpawnTime = 0f;
        // 다음번 배치까지의 시간 간격을 초기화
        timeBetSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        // 게임오버 상태에서는 동작하지 않음
        if (GameManager.instance.isGameover) return;

        // 마지막 배치 시점에서 timeBetSpawn 이상 시간이 흘렀다면,
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // 기록된 마지막 배치 시점을 현재 시점으로 갱신
            lastSpawnTime = Time.time;

            // 다음 배치까지의 시간 간격을 timeBetSpawnMin, timeBetSpawnMax 사이에서 랜덤 가져오기
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