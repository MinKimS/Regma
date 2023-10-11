using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObj : MonoBehaviour
{
    PlayerHide hide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            hide.isHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            hide.isHide = false;
        }
    }

    //public BathMobEye eye;
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //플레이어가 숨어있는 동안 찾을 수 없음
    //    if (collision.CompareTag("Player"))
    //    {
    //        eye.isPlayerHide = true;
    //        print("hiding!!");
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Player"))
    //    {
    //        eye.isPlayerHide = false;
    //        print("no hide");
    //    }
    //}
}
