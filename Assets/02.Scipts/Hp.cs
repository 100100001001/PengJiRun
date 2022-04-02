using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public GameObject[] hpPlus;

    private void OnEnable()
    {
        for (int i = 0; i < hpPlus.Length; i++) hpPlus[i].SetActive(Random.Range(0, 2) == 0 ? true : false);
    }
}