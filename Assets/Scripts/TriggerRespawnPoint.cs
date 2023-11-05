using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResponManager;

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
            ResponManager.Instance.ChangeUpdatingMethod(ResponManager.ChangeMethod.MobBased);
            ResponManager.Instance.OnUpdateRespawnPoint.Invoke(targetPosition);
            

        }
                // Destroy(this.gameObject);
            
    }
}
