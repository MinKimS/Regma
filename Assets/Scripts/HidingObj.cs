using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObj : MonoBehaviour
{
    public BathMobEye eye;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //플레이어가 숨어있는 동안 찾을 수 없음
        if(collision.CompareTag("Player"))
        {
            eye.StopRolling();
            print("hiding!!");
        }
    }
}
