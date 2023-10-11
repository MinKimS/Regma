using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobTryCatchPlayerTrigger : MonoBehaviour
{
    public BathMobController bmc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            bmc.isTryCatchPlayer = true;
            Destroy(gameObject);
        }
    }
}
