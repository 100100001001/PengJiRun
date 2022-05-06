using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    public GameObject[] potionPlus;
    public GameObject[] potionMinus;
    public GameObject[] potionFever;

    private void OnEnable()
    {
        for (int i = 0; i < potionPlus.Length; i++) potionPlus[i].SetActive(Random.Range(0, 7) == 0 ? true : false);
        for (int i = 0; i < potionMinus.Length; i++) potionMinus[i].SetActive(Random.Range(0, 7) == 0 ? true : false);
        //for (int i = 0; i < potionFever.Length; i++) potionFever[i].SetActive(Random.Range(0, 10) == 0 ? true : false);
        for (int i = 0; i < potionFever.Length; i++) potionFever[i].SetActive(Random.Range(0, 1) == 0 ? true : false);
    }
}
