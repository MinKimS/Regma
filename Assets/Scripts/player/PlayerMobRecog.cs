using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobRecog : MonoBehaviour
{
    PlayerHide hide;
    private Vector3 lastSafePosition; // ���ΰ��� ��ġ�� ������ ����

    private void Awake()
    {
        hide = GetComponent<PlayerHide>();
    }

    void Start()
    {
        lastSafePosition = transform.position; // ���ΰ��� ���� ��ġ�� ������ ��ġ�� �ʱ�ȭ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            if (hide != null)
            {
                if (!hide.isHide)
                {
                    print("be found player");
                    AudioManager.instance.StopSFXAll();
                    AudioManager.instance.SFXPlay("�ֹ�_������ü1 �����߰�");
                    // �浹 �� ������ ��ġ�� ���ΰ��� �ǵ���
                  //  transform.position = lastSafePosition;
                }
            }
        }
    }
}
