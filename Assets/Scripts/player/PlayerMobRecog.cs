using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobRecog : MonoBehaviour
{
    PlayerHide hide;

    private void Awake()
    {
        hide = GetComponent<PlayerHide>();
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
                    AudioManager.instance.SFXPlay("�ֹ�_������ü1 �����߰�");
                }
            }
        }
    }
}
