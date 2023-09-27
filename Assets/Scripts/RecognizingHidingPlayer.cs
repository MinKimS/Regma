using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecognizingHidingPlayer : MonoBehaviour
{
    public LightControl lightControl;
    bool isInHideArea = false;
    public PlayerHide playerHide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�÷��̾ ����
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
        //�÷��̾ �� ����
        if (collision.CompareTag("HidingPoint"))
        {
            isInHideArea = false;
            collision.gameObject.GetComponentInParent<PlayerHide>().isTryHiding = false;
            collision.gameObject.GetComponentInParent<PlayerHide>().isHide = false;
            print("not hide");
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

    private void Update()
    {
        //�÷��̾ ������ ���������� ����
        if (isInHideArea && !lightControl.isLightOn)
        {
            playerHide.isTryHiding = true;
            print("hide");
        }
        else
        {
            playerHide.isTryHiding = false;
            playerHide.isHide = false;
            print("not hide");
        }
    }
}
