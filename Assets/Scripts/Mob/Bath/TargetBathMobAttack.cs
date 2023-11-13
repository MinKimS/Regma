using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBathMobAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //¸÷ ÇÑÅ× ÀâÈù °æ¿ì ¹°¿¡ ²ø°í °¡±â
        if(collision.CompareTag("Hand"))
        {
            BathMobHand hand = collision.GetComponentInParent<BathMobHand>();

            hand.isCatchSomething = true;

            if (hand != null && hand.isTryCatchSomething)
            {
                transform.SetParent(collision.transform);

                hand.MoveIntoWater(0.4f);
            }
        }
    }
}
