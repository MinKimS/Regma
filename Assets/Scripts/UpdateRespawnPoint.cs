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
            // ResponManager.Instance �� null���� üũ�ϴ� ���� �����ϴ�.
            if (RespawnManager.Instance != null)
            {
                if (damageCount.GetDamage() == 0)
                {
                    transform.position = player.position;
                    RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(RespawnManager.ChangeMethod.DamageBased, transform);
                    Debug.Log($"������ ��ġ ������ : {transform.position}");
                }

                //RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(transform);

            }

            //RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(transform);
            yield return new WaitForSeconds(routineSpeed);
        }
    }
}
