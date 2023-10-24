using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerandaPot : MonoBehaviour
{
    BoxCollider2D bc;
    Animator anim;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetTrigger("wilting");
            print("animation");
            bc.enabled = false;
        }
    }
}
