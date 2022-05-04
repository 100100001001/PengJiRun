using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// GameScene으로 바꾸는 스크립트
public class StartButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
