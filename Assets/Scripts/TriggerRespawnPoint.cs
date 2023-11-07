using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RespawnManager;

public class TriggerRespawnPoint : MonoBehaviour
{
    [SerializeField] Transform targetPosition;
    [SerializeField] bool isTargetMyself = true;
    [SerializeField] Damaging damageCount;

    private void Awake()
    {
        if(isTargetMyself)
           targetPosition = transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RespawnManager.Instance.ChangeUpdatingMethod(ChangeMethod.MobBased);

            if (RespawnManager.Instance != null && RespawnManager.Instance.currentMethod == RespawnManager.ChangeMethod.MobBased)
            {
                RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(targetPosition);
            }
            // Destroy(this.gameObject);

            if (damageCount.GetDamage() == 3)
            {
                print("±Í½ÅÀÌ¶û Ãæµ¹ + µ¥¹ÌÁö 3¹ø");
                RespawnManager.Instance.ChangeUpdatingMethod(ChangeMethod.DamageBased);
            }
        }

        


    }

    private void Update()
    {
        
    }
}
