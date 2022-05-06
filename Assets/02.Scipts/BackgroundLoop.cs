using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 배경화면이 무한루프 되는 스크립트
public class BackgroundLoop : MonoBehaviour
{
    private float width;
    private void Awake()
    {
        BoxCollider2D backgrounCollider = GetComponent<BoxCollider2D>(); // 배경의 너비값을 가져오기 위해 BoxCollider2D를 가져옴
        width = backgrounCollider.size.x + 11.4f; // BoxCollider2D의 x값 + Scale을 1.57배 해주었기 때문에 11.4를 더해줌
        //Debug.Log("backgrounCollider.size.x" + backgrounCollider.size.x); // 20
        //Debug.Log("width" + width);                                       // 31.4
    }

    void Update()
    {
        if (transform.position.x <= -width)
        {
            Reposition();
        }
    }

    void Reposition()
    {
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
