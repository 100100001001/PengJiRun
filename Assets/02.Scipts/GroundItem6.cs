using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem6 : MonoBehaviour
{
    public GameObject[] stars;

    private void OnEnable()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                stars[i].SetActive(true);
            }
            else
            {
                stars[i].SetActive(false);
            }
        }
    }
}
