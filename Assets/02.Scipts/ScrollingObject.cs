using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f;
    public static float add = 0.001f;

    void Update()
    {

        if (!GameManager.instance.isGameover)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            speed += add;
        }
    }
}
