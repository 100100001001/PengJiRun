using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles;
    private bool stepped = false;

    public GameObject[] stars;

    private void OnEnable()
    {
        stepped = false;

        for (int i = 0; i < obstacles.Length; i++) obstacles[i].SetActive(Random.Range(0, 4) == 0 ? true : false);
        for (int i = 0; i < stars.Length; i++) stars[i].SetActive(Random.Range(0, 2) == 0 ? true : false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && !stepped)
        {
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }
}
