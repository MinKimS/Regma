using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
         

            if (RespawnManager.Instance != null )
            {
                RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(RespawnManager.ChangeMethod.MobBased, targetPosition);
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
