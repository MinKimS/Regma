using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobRunWildTrigger : MonoBehaviour
{
    public BathMobController bmc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            bmc.movement.StartRunningWild();
            bmc.hand.SetToyIdx(4);
            Destroy(gameObject);
        }
    }
}
