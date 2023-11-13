using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobHandAttack : MonoBehaviour
{
    BathMobHand hand;

    private void Awake()
    {
        hand = GetComponentInParent<BathMobHand>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Toy"))
        {
            hand.isCatchSomething = true;
            collision.transform.parent.SetParent(transform);

            if (hand.isTryCatchSomething)
            {
                hand.MoveIntoWater(0.4f);
            }
        }
    }
}
