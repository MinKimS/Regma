using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseLadder : MonoBehaviour
{
    public Transform upperLandPos;  // 위 방향키를 눌렀을 때 도착 위치
    public Transform lowerLandPos;  // 아래 방향키를 눌렀을 때 도착 위치

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = PlayerInfoData.instance.playerRb;

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                // 중력을 0으로 설정
                playerRb.gravityScale = 0f;

                // 플레이어 위치 설정
                PlayerInfoData.instance.playerTr.position = lowerLandPos.position;

                // 플레이어에게 힘을 주어 아래로 이동
                playerRb.AddForce(Vector2.down * 10f);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                // 중력을 원래 값으로 설정
                playerRb.gravityScale = 1f;

                // 플레이어 위치 설정
                PlayerInfoData.instance.playerTr.position = upperLandPos.position;
            }
        }
    }
}
