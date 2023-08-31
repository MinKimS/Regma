using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class REFRIG : MonoBehaviour
{
    InteractionObjData iod;
    public REFRIGPower rPower;
    public GameObject mobAppearEvent;

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
            if (!rPower.IsBroken)
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
                isOpened = true;
                iod.IsOkInteracting = false;
            }
        }
    }

    //오징어 획득
    void GetSquid()
    {
        ////비어있는 슬롯에만 저장
        //for (int i = 0; i < SmartphoneManager.instance.filesInven.slotList.Count; i++)
        //{
        //    if (!SmartphoneManager.instance.filesInven.slotDataList[i].isFull)
        //    {
        //        SetItem(i, item);
        //        SmartphoneManager.instance.filesInven.slotDataList[i].isFull = true;
        //        SmartphoneManager.instance.maxFilesSlot++;
        //        break;
        //    }
        //}
    }
}
