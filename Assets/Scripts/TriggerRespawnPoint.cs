using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RespawnManager;

public class TriggerRespawnPoint : MonoBehaviour
{
    [SerializeField] Transform targetPosition;
    [SerializeField] bool isTargetMyself = true;

    private void Awake()
    {
        if(isTargetMyself)
           targetPosition = transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RespawnManager.Instance.ChangeUpdatingMethod(RespawnManager.ChangeMethod.MobBased);
            RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(targetPosition);
            

        }
                // Destroy(this.gameObject);
            
    }
}
