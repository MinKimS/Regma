using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class REFRIG : MonoBehaviour
{
    InteractionObjData iod;
    public REFRIGPower rPower;

    bool isOpened = false;

    private void Awake()
    {
        iod = GetComponent<InteractionObjData>();
        if(rPower == null)
        {
            rPower = FindObjectOfType<REFRIGPower>();
        }
    }

    //����� �� ����
    public void OpenREFRIG()
    {
        if(!isOpened)
        {
            //����� ���峻�� ��
            if (!rPower.IsBroken)
            {
                DialogueManager.instance.PlayDlg(iod.objDlg[0]);
            }
            //����� ���峽 ��
            else
            {
                DialogueManager.instance.PlayDlg(iod.objDlg[1]);
                isOpened = true;
                iod.IsOkInteracting = false;
            }
        }
    }

    //��¡�� ȹ��
    void GetSquid()
    {
        ////����ִ� ���Կ��� ����
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
