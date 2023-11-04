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
            Debug.LogError("startRunwildCatchPlayer");
            bmc.data.IsTryCatchPlayer = true;
            bmc.hand.isTargetPlayer = true;
            Destroy(gameObject);
        }
    }
}
