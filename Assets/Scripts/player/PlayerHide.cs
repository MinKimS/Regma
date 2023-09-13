using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    [HideInInspector]
    public bool isHide = false;
    CapsuleCollider2D col;
    Rigidbody2D rb;

    private void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("HideObj"))
        {
            isHide = true;
            col.isTrigger = true;
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isHide)
        {
            isHide = false;
            col.isTrigger = false;
            rb.gravityScale = 1;
        }
    }
}
