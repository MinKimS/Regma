using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObj : MonoBehaviour
{
    public BathMobEye eye;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //�÷��̾ �����ִ� ���� ã�� �� ����
        if(collision.CompareTag("Player"))
        {
            eye.StopRolling();
            print("hiding!!");
        }
    }
}
