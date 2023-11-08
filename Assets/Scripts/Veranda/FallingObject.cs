using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RespawnManager;

public class FallingObject : MonoBehaviour
{
    Vector3 origin;
    //public UpdateRespawnPoint updaterespawnPoint; // UpdateRespawnPoint ��ũ��Ʈ ����

    private void OnEnable()
    {
        origin = transform.position;
        transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("fall on player");

            //RespawnManager.Instance.ChangeUpdatingMethod(ChangeMethod.DamageBased);
            RespawnManager.Instance.OnGameOver.Invoke();
        }

        //�ٴ��̳� �÷��̾ �������� �Ⱥ��̰� ����
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.position = origin;
    }
}
