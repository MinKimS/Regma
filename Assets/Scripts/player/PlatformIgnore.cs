using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIgnore : MonoBehaviour
{
    public Collider2D platformCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // "Collision" 대신 "collision"으로 수정
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, true); // "collision.GetComponent<Collider2D>()" 대신 "collision"으로 수정
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, false);
        }
    }
}
