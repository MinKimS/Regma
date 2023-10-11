using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecognizingHidingPlayer : MonoBehaviour
{
    public LightControl lightControl;
    bool isInHideArea = false;
    public PlayerHide playerHide;
    
    //부엌에서 쓰이는지 욕실에서 쓰이는지 체크
    public bool isInBath = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어가 숨음
        if (collision.CompareTag("HidingPoint"))
        {
            isInHideArea = true;
            SpriteRenderer sp = collision.gameObject.GetComponentInParent<SpriteRenderer>();
            if(sp != null)
            {
                sp.color = Color.gray;
            }
            else
            {
                Debug.LogWarning("No SpriteRenderer");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //플레이어가 안 숨음
        if (collision.CompareTag("HidingPoint"))
        {
            isInHideArea = false;
            collision.gameObject.GetComponentInParent<PlayerHide>().isTryHiding = false;
            collision.gameObject.GetComponentInParent<PlayerHide>().isHide = false;
            SpriteRenderer sp = collision.gameObject.GetComponentInParent<SpriteRenderer>();
            if (sp != null)
            {
                sp.color = Color.white;
            }
            else
            {
                Debug.LogWarning("No SpriteRenderer");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("HidingPoint"))
        {
            if(!isInBath)
            {
                //플레이어가 완전히 숨어졌는지 설정
                if (isInHideArea && !lightControl.isLightOn)
                {
                    playerHide.isTryHiding = true;
                }
                else
                {
                    playerHide.isTryHiding = false;
                    playerHide.isHide = false;
                }
            }
            else
            {
                if(isInHideArea)
                {
                    playerHide.isHide = true;
                }
                else
                {
                    playerHide.isHide = false;
                }
            }
        }
    }
}
