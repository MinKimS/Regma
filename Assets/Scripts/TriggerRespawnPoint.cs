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

            if (RespawnManager.Instance != null && RespawnManager.Instance.currentMethod != RespawnManager.ChangeMethod.DamageBased)
            {
                RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(targetPosition);
            }

            else
            {
                RespawnManager.Instance.ChangeUpdatingMethod(ChangeMethod.DamageBased);
            }

            // Destroy(this.gameObject);


        }

        //if (damageCount.GetDamage() != 0)
        //{
        //    print("귀신 위치 활성화 + 데미지 3번");
        //    RespawnManager.Instance.ChangeUpdatingMethod(ChangeMethod.DamageBased);
        //    //RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(targetPosition);
        //    //RespawnManager.Instance.OnGameOver.Invoke();
        //}




    }

    private void Update()
    {
        
    }
}
