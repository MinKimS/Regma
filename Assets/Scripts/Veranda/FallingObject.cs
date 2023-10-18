using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class FallingObject : MonoBehaviour
{
    Vector3 origin;
    private void OnEnable()
    {
        origin = transform.position;
        print(origin);
        print("set position that start falling");
        transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f,360f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            print("fall on player");
        }

        print("false");
        //바닥이나 플레이어에 떨어지면 안보이게 설정
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.position = origin;
        print(origin);
    }
}
