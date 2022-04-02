using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItemRandom : MonoBehaviour
{
    public GameObject[] starsRandom;
    public GameObject[] stars;
    public GameObject[] ob;
    public GameObject[] hpPlus;

    private void OnEnable()
    {
        for (int i = 0; i < starsRandom.Length; i++) starsRandom[i].SetActive(Random.Range(0, 2) == 0 ? true : false);
        for (int i = 0; i < stars.Length; i++) stars[i].SetActive(Random.Range(0, 2) == 0 ? true : false);
        for (int i = 0; i < ob.Length; i++) ob[i].SetActive(Random.Range(0, 2) == 0 ? true : false);
        for (int i = 0; i < hpPlus.Length; i++) hpPlus[i].SetActive(Random.Range(0, 5) == 0 ? true : false);
    }
}
