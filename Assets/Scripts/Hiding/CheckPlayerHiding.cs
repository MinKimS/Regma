using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerHiding : MonoBehaviour
{
    public CheckHidingTracing checker;

    //더 이상 숨지 못하게 하는 이벤트
    public Transform block;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            checker.blockObj = block;
        }
    }
}
