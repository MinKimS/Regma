using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KitchenDamageObj : MonoBehaviour
{
    IEnumerator damage;

    public float damageTimeGap;

    //damage image
    public Damaging damaging;

    private void Awake()
    {
        damage = Damage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(damage);
        }
    }

    IEnumerator Damage()
    {
        WaitForSeconds wait = new WaitForSeconds(damageTimeGap);

        while(true)
        {
            print("damage");
            damaging.Damage();
            yield return wait;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(damage);
    }
}
