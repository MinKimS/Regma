using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerHiding : MonoBehaviour
{
    public CheckHidingTracing checker;

    //�� �̻� ���� ���ϰ� �ϴ� �̺�Ʈ
    public Transform block;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            checker.blockObj = block;
        }
    }
}
