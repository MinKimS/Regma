using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseLadder : MonoBehaviour
{
    public Transform landPos;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))))
        {
            PlayerInfoData.instance.playerTr.position = landPos.position;
        }
    }
}
