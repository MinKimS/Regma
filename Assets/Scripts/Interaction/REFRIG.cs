using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REFRIG : MonoBehaviour
{
    InteractionObjData iod;
    public REFRIGPower rPower;
    public ItemData squid;
    public Transform mobAppear;
    public Transform talkStarting;

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

    //����� �� ����
    public void OpenREFRIG()
    {
        if(iod == null)
        {
            iod = GetComponent<InteractionObjData>();
        }
        if(!isOpened)
        {
            //����� ���峻�� ��
            if (!rPower.isBroken)
            {
                DialogueManager.instance.PlayDlg(iod.objDlg[0]);
                if(!isActivateEvent)
                {
                    isActivateEvent = true;
                    StartCoroutine(ReadyToMobAppear());
                }
            }
            //����� ���峽 ��
            else
            {
                DialogueManager.instance.PlayDlg(iod.objDlg[1]);
                SmartphoneManager.instance.SetInvenItem(squid);
                isOpened = true;
                iod.IsOkInteracting = false;
                StartCoroutine(GetSquid());
            }
        }
    }

    public void DlgStart()
    {
        if (isOpened)
        {
        }
        print("test");
        SmartphoneManager.instance.phone.StartTalk();
    }

    IEnumerator ReadyToMobAppear()
    {
        yield return new WaitWhile(() => DialogueManager.instance._dlgState != DialogueManager.DlgState.End);
        mobAppear.gameObject.SetActive(true);
    }
    IEnumerator GetSquid()
    {
        yield return new WaitWhile(() => DialogueManager.instance._dlgState != DialogueManager.DlgState.End);
        TimelineManager.instance.timelineController.SetTimelineStart("GetSquid");
        yield return new WaitWhile(() => TimelineManager.instance._Tlstate != TimelineManager.TlState.End);
        talkStarting.gameObject.SetActive(true);
        talkStarting.position = PlayerInfoData.instance.playerTr.position;
    }
}
