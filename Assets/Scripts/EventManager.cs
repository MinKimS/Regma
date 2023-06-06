using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //비활성화 됬다가 활성화 되는 이벤트들
    public GameObject[] Events;

    public void ActiveEvent(int idx)
    {
        Events[idx].SetActive(true);
    }

    public void OutputDlg(Dialogue dlg)
    {
        DialogueManager.instance.PlayDlg(dlg);
    }
}
