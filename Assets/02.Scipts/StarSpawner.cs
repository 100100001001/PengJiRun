﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    // obstacle
    //private GameObject[] ob;
    //public GameObject obPrefab;
    //public float obScaleMin = 1f;
    //public float obScaleMax = 3f;
    //private int obIdx = 0;
    //public int obLen = 10;

    //// +HP
    //private GameObject[] hpPlus;
    //public GameObject hpPlusPrefab;
    //public float hpXMin = -1.5f;
    //public float hpXMax = 1.5f;
    //public float hpYMin = -3f;
    //public float hpYMax = 2f;
    //private int hpIdx = 0;
    //private int hpLen = 3;

    // star
    private GameObject[] stars;
    public GameObject starPrefab;
    private float xPos = 20f;
    public float starYMin = -3f;
    public float starYMax = 2f;
    public int starIdx = 0;
    public int starLen = 5;

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
        //ob = new GameObject[obLen];
        //hpPlus = new GameObject[hpLen];
        stars = new GameObject[starLen];

        //for (int i = 0; i < obLen; i++)
        //{
        //    ob[i] = Instantiate(obPrefab, poolPosition, Quaternion.identity);
        //}

        //for (int i = 0; i < hpLen; i++)
        //{
        //    hpPlus[i] = Instantiate(hpPlusPrefab, poolPosition, Quaternion.identity);
        //}

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
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax) - (ScrollingObject.addSpeed * 10);

            // ob
            //float obXPos = Random.Range(hpXMin, hpXMax);
            //float obYPos = Random.Range(hpYMin, hpYMax);
            //ob[obIdx].SetActive(false);
            //ob[obIdx].SetActive(true);
            //float obScale = Random.Range(obScaleMin, obScaleMax);
            //ob[obIdx].transform.localScale = new Vector3(obScale, obScale, obScale);
            //ob[obIdx].transform.position = new Vector2(obXPos, obYPos);

            //// +hp
            //float hpXPos = Random.Range(hpXMin, hpXMax);
            //float hpYPos = Random.Range(hpYMin, hpYMax);

            //// +hp
            //if (GameManager.instance.hpCount < 3 && Random.Range(0, 2) == 0)
            //{
            //    hpPlus[hpIdx].SetActive(false);
            //    hpPlus[hpIdx].SetActive(true);
            //    hpPlus[hpIdx].transform.position = new Vector2(hpXPos, hpYPos);
            //    //hpPlusPrefab.SetActive(true);
            //    //hpPlusPrefab.transform.position = new Vector2(xPos, hpYPos);

            //    hpIdx++;

            //    if (hpIdx >= hpLen)
            //    {
            //        hpIdx = 0;
            //    }
            //}

            //// hp 다 찼을 때
            //if (GameManager.instance.hpCount >= 3) for (int i = 0; i < hpLen; i++) hpPlus[i].SetActive(false);

            // stars
            float starYPos = Random.Range(starYMin, starYMax);
            stars[starIdx].SetActive(false);
            stars[starIdx].SetActive(true);
            stars[starIdx].transform.position = new Vector2(xPos, starYPos);

            //obIdx++;

            //if (obIdx >= obLen)
            //{
            //    obIdx = 0;
            //}

            starIdx++;

            if (starIdx >= starLen)
            {
                starIdx = 0;
            }
        }
    }
}