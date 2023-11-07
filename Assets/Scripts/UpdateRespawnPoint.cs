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
            // ResponManager.Instance 가 null인지 체크하는 것이 좋습니다.
            if (RespawnManager.Instance != null && RespawnManager.Instance.currentMethod == RespawnManager.ChangeMethod.DamageBased)
            {
                if (damageCount.GetDamage() == 0)
                {
                    transform.position = player.position;
                    RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(transform);
                    Debug.Log($"리스폰 위치 갱신함 : {transform.position}");
                }

               
            }

            //RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(transform);
            yield return new WaitForSeconds(routineSpeed);
        }
    }
}
