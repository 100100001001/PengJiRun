using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;    // 싱글톤 처리

    public bool isGameover = false;        // 게임오버 상태 표시
    public Text scoreText;                 // 점수 텍스트
    public GameObject gameoverUI;          // 게임오버 UI

    private int score = 0;                 // 게임 점수

    public GameObject menuPanel;           // 메뉴

    public int hpCount = 3;                // 체력
    public GameObject[] hpPrefabPositive;  // 채워진 체력
    public GameObject[] hpPrefabNegative;  // 깎인 체력

    public Text recordText;                // 점수 기록

    void Start()
    {
        for (int i = 0; i < 3; i++) hpPrefabPositive[i].SetActive(true);  // 채워진 체력만 띄워 줌

        float bestTime = PlayerPrefs.GetFloat("BestTime");
        recordText.text = "" + (int)bestTime;                             // 최고 점수 텍스트
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 게임오버 상태이면서 화면을 누르면 게임이 재시작
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int newScore)
    {
        // 게임오버 상태가 아닐 때 점수를 추가해 줌

        if (!isGameover)
        {
            score += newScore;
            scoreText.text = ""+ score;
        }
    }

    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        // 이전까지의 최고 기록과 현재 생존 시간을 비교
        if (bestTime < score)
        {
            // 최고 기록 값을 현재 생존 시간 값으로 변경
            bestTime = score;
            // 변경된 최고 기록을 'BestTime' 키로 저장
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        recordText.text = "" + (int)bestTime;
    }

    public void UIControl(string type)
    {
        // 메뉴 버튼을 눌렀을 때

        switch (type)
        {
            case "menuOn":
                menuPanel.SetActive(true);
                Time.timeScale = 0f;
                break;
            case "menuOff":
                menuPanel.SetActive(false);
                Time.timeScale = 1f;
                break;
            case "restart":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1f;
                break;
            case "exit":
                Application.Quit();
                break;
        }
    }

    public bool Crash()
    {
        if (PlayerController.feverTime) return false;

        hpCount--;

        hpPrefabPositive[hpCount].SetActive(false);
        hpPrefabNegative[hpCount].SetActive(true);

        if (hpCount <= 0) return true;
        return false;
    }

    public void HPPlus()
    {
        hpPrefabPositive[hpCount].SetActive(true);
        hpPrefabNegative[hpCount].SetActive(false);

        hpCount++;

        if (hpCount >= 3)
        {
            hpCount = 3;
        }
    }
}
