using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ob : MonoBehaviour
{
    public GameObject[] ob;

    private void OnEnable()
    {
        for (int i = 0; i < ob.Length; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                ob[i].SetActive(true);
            }
            else
            {
                ob[i].SetActive(false);
            }
        }
    }
}
