using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour
{
    // 생성할 발판의 원본 프리팹
    public GameObject platformPrefab;
    // 생성할 발판 수
    public int count = 5;

    // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMin = 1.25f;
    // 다음 배치까지의 시간 간격 최댓값
    public float timeBetSpawnMax = 2.25f;
    // 다음 배치까지의 시간 간격
    private float timeBetSpawn;

    // 배치할 위치의 최소 y값
    public float yMin = -5f;
    // 배치할 위치의 최대 y값
    public float yMax = 1.5f;
    // 배치할 위치의 x값
    private float xPos = 20f;

    // 미리 생성한 발판들을 보관할 배열
    private GameObject[] platforms;
    // 사용할 현재 순번의 발판
    private int currentIndex = 0;

    // 초반에 생성한 발판을 화면 밖에 숨겨둘 위치
    private Vector2 poolPosition = new Vector2(0, -25);
    // 마지막 배치 시점
    private float lastSpawnTime;

    // +HP
    private GameObject[] hpPlus;
    public GameObject hpPlusPrefab;
    public float hpXMin = -1.5f;
    public float hpXMax = 1.5f;
    public float hpYMin = -3f;
    public float hpYMax = 2f;
    private int hpIdx = 0;
    private int hpLen = 3;

    // obstacle
    private GameObject[] ob;
    public GameObject obPrefab;
    public float obScaleMin = 1f;
    public float obScaleMax = 3f;

    // star
    private GameObject[] stars;
    public GameObject starPrefab;
    public float starXMin = -1.5f;
    public float starXMax = 1.5f;
    public float starYMin = -3f;
    public float starYMax = 2f;
    public int starIdx = 0;
    public int starLen = 2;

    void Start()
    {
        // 변수를 초기화하고 사용할 발판을 미리 생성

        // count만큼의 공간을 가지는 새로운 발판 배열 생성
        platforms = new GameObject[count];
        ob = new GameObject[count];
        hpPlus = new GameObject[hpLen];
        stars = new GameObject[starLen];

        // count만큼 루프하면서 발판 생성s
        for (int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
            ob[i] = Instantiate(obPrefab, poolPosition, Quaternion.identity);
        }

        for (int i = 0; i < hpLen; i++)
        {
            hpPlus[i] = Instantiate(hpPlusPrefab, poolPosition, Quaternion.identity);
        }
        //Instantiate(hpPlusPrefab, poolPosition, Quaternion.identity);

        for (int i = 0; i < starLen; i++)
        {
            stars[i] = Instantiate(starPrefab, poolPosition, Quaternion.identity);
        }


        // 마지막 배치 시점 초기화
        lastSpawnTime = 0f;
        // 다음번 배치까지의 시간 간격을 초기화
        timeBetSpawn = 0f;
    }

    void Update()
    {
        // 순서를 돌아가며 주기적 발판을 배치

        // 게임오버 상태에서는 동작하지 않음
        if (GameManager.instance.isGameover) return;

        // 마지막 배치 시점에서 timeBetSpawn 이상 시간이 흘렀다면,
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // 기록된 마지막 배치 시점을 현재 시점으로 갱신
            lastSpawnTime = Time.time;

            // 다음 배치까지의 시간 간격을 timeBetSpawnMin, timeBetSpawnMax 사이에서 랜덤 가져오기
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            // 배치할 위치의 높이를 yMin과 yMax 사이에서 랜덤 가져오기
            float yPos = Random.Range(yMin, yMax);

            // +hp
            float hpXPos = Random.Range(hpXMin, hpXMax);
            float hpYPos = Random.Range(hpYMin, hpYMax);

            // 사용할 현재 순번의 발판 게임 오브젝트를 비활성화하고 바로 즉시 다시 활성화.
            // 이 때, 발판의 Platform 컴포넌트의 OnEnable() 메서드가 실행됨
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            // 현재 순번의 발판을 화면 오른쪽에 재배치
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

            // ob
            float obXPos = Random.Range(hpXMin, hpXMax);
            float obYPos = Random.Range(hpYMin, hpYMax);
            ob[currentIndex].SetActive(false);
            ob[currentIndex].SetActive(true);
            float obScale = Random.Range(obScaleMin, obScaleMax);
            ob[currentIndex].transform.localScale = new Vector3(obScale, obScale, obScale);
            ob[currentIndex].transform.position = new Vector2(obXPos, obYPos);

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

            // hp 다 찼을 때
            if (GameManager.instance.hpCount >= 3) for (int i = 0; i < hpLen; i++) hpPlus[i].SetActive(false);

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

            // 순번 넘기기
            currentIndex++;

            // 마지막 순번에 도달했다면
            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}
