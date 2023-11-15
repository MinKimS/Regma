using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseLadder : MonoBehaviour
{
    public Transform upperLandPos;  // �� ����Ű�� ������ �� ���� ��ġ
    public Transform lowerLandPos;  // �Ʒ� ����Ű�� ������ �� ���� ��ġ

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = PlayerInfoData.instance.playerRb;

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                // �߷��� 0���� ����
                playerRb.gravityScale = 0f;

                // �÷��̾� ��ġ ����
                PlayerInfoData.instance.playerTr.position = lowerLandPos.position;

                // �÷��̾�� ���� �־� �Ʒ��� �̵�
                playerRb.AddForce(Vector2.down * 10f);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                // �߷��� ���� ������ ����
                playerRb.gravityScale = 1f;

                // �÷��̾� ��ġ ����
                PlayerInfoData.instance.playerTr.position = upperLandPos.position;
            }
        }
    }
}
