using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenHideObj : MonoBehaviour
{
    public MobAppear appear;
    BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(appear.isLastMob && collision.CompareTag("Player"))
        {
            col.enabled = false;
            gameObject.SetActive(false);
            print("you can't hide here");
        }
    }
}
