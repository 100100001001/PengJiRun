using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameover = false;
    public Text scoreText;
    public GameObject gameoverUI;

    private int score = 0;

    public GameObject menuPanel;

    public int hpCount = 3;
    public GameObject[] hpPrefabPositive;
    public GameObject[] hpPrefabNegative;

    void Start()
    {
        for (int i = 0; i < 3; i++) hpPrefabPositive[i].SetActive(true);
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
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int newScore)
    {
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
    }

    public void UIControl(string type)
    {
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
        hpCount--;

        hpPrefabPositive[hpCount].SetActive(false);
        hpPrefabNegative[hpCount].SetActive(true);

        if (hpCount <= 0) return true;
        return false;
    }
}
