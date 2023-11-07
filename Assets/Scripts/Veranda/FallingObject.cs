using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RespawnManager;
using static UnityEngine.UI.Image;

public class FallingObject : MonoBehaviour
{
    Vector3 origin;

    private void OnEnable()
    {
        origin = transform.position;
        transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f,360f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            print("fall on player");
            RespawnManager.Instance.ChangeUpdatingMethod(ChangeMethod.DamageBased);

            if (RespawnManager.Instance != null && RespawnManager.Instance.currentMethod == RespawnManager.ChangeMethod.DamageBased)
            {

                //RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(transform);
                print("�������� ��ȯ");
                RespawnManager.Instance.OnGameOver.Invoke();
            }
            
            //RespawnManager.Instance.ChangeUpdatingMethod(RespawnManager.ChangeMethod.DamageBased);
            //RespawnManager.Instance.OnGameOver.Invoke();
        }

        print("false");
        //�ٴ��̳� �÷��̾ �������� �Ⱥ��̰� ����
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.position = origin;
    }
}
