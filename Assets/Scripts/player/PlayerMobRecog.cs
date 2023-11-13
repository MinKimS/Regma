using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobRecog : MonoBehaviour
{
    PlayerHide hide;
    private Vector3 lastSafePosition; // 주인공의 위치를 저장할 변수

    private void Awake()
    {
        hide = GetComponent<PlayerHide>();
    }

    void Start()
    {
        lastSafePosition = transform.position; // 주인공의 시작 위치를 안전한 위치로 초기화
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
                    AudioManager.instance.SFXPlay("주방_괴생명체1 도원발견");
                    AudioManager.instance.StopSFX("주방_괴생명체1 도원 추격");
                    collision.GetComponentInParent<MoveAlongThePath>().gameObject.SetActive(false);
                    // 충돌 시 안전한 위치로 주인공을 되돌림
                  //  transform.position = lastSafePosition;
                }
            }
        }
    }
}
