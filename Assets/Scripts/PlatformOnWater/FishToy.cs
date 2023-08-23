using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishToy : MonoBehaviour
{
    Rigidbody2D rb;
    public float bouncePower = 5f;
    public float bounceTerm = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FlappingFish());
    }

    IEnumerator FlappingFish()
    {
        var wait = new WaitForSeconds(bounceTerm);
        
        while(true)
        {
            yield return wait;

            rb.AddForce(Vector2.up * bouncePower, ForceMode2D.Impulse);
        }
    }
}
