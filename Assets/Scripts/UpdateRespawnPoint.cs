using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRespawnPoint : MonoBehaviour
{
    [SerializeField] float routineSpeed = 4f;
    [SerializeField] Transform player;
    [SerializeField] Damaging damageCount;
 
    void Start()
    {
        StartCoroutine(RespawnPositionUpdateRoutine());
    }

    IEnumerator RespawnPositionUpdateRoutine()
    {
        while (true)
        {
            if (damageCount.GetDamage() == 0)
            {
                transform.position = player.position;
                ResponManager.Instance.OnUpdateRespawnPoint.Invoke(transform);
                Debug.Log($"리스폰 위치 갱신함 : {transform.position}");
            }
            yield return new WaitForSeconds(routineSpeed);
        }
    }
}
