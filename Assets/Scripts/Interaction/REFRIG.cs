using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REFRIG : MonoBehaviour
{
    InteractionObjData iod;
    public REFRIGPower rPower;
    public GameObject mobAppearEvent;
    public ItemData squid;
    public GameEvent mobAppear;
    public BoxCollider2D afterOpenEventCol;

    bool isOpened = false;
    bool isActivateEvent = false;

    private void Awake()
    {
        iod = GetComponent<InteractionObjData>();
        if (rPower == null)
        {
            rPower = FindObjectOfType<REFRIGPower>();
        }
    }

    //냉장고 문 열기
    public void OpenREFRIG(GameObject objEvent)
    {
        if(iod == null)
        {
            iod = GetComponent<InteractionObjData>();
        }
        if(!isOpened)
        {
            //어댑터 고장내기 전
            if (!rPower.isBroken)
            {
                DialogueManager.instance.PlayDlg(iod.objDlg[0]);
                if(!isActivateEvent)
                {
                    isActivateEvent = true;
                    objEvent.SetActive(true);
                }
            }
            //어댑터 고장낸 후
            else
            {
                DialogueManager.instance.PlayDlg(iod.objDlg[1]);
                SmartphoneManager.instance.SetInvenItem(squid);
                isOpened = true;
                iod.IsOkInteracting = false;
                afterOpenEventCol.enabled = true;
                afterOpenEventCol.transform.position = PlayerInfoData.instance.playerTr.position;
            }
        }
    }

    public void DlgStart()
    {
        if (isOpened && DialogueManager.instance._dlgState == DialogueManager.DlgState.End)
        {
            print("test");
            SmartphoneManager.instance.phone.StartTalk();
        }
    }
}
