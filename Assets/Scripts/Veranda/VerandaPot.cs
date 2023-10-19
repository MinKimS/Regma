using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerandaPot : MonoBehaviour
{
    BoxCollider2D bc;
    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            print("animation");
            bc.enabled = false;
        }
    }
}
