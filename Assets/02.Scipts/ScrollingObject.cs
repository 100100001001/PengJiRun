using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f;
    //public static float addSpeed = 0.0001f;
    public static float addSpeed = 0.001f;
    //public static float addSpeed = 2f;

    //public static bool addTime = false;
    //public float addTimeCnt = 20f;


    void Update()
    {

        if (!GameManager.instance.isGameover)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            speed += addSpeed;
            Debug.Log(speed);

            //addTimeCnt -= Time.deltaTime;
            //Debug.Log(speed);

            //if (addTimeCnt < 0)
            //{
            //    if (speed >= 20) return;
            //    speed += addSpeed;
            //    addTimeCnt = 20f;
            //}
        }
    }
}
