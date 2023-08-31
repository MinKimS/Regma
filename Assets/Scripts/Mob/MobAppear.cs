using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAppear : MonoBehaviour
{
    public GameObject[] mob;
    public MoveAlongThePath[] matp;
    public Dialogue[] dlg;

    public void OnTable()
    {
        StartCoroutine(AppearOnTalbeInKitchen());
    }

    IEnumerator AppearOnTalbeInKitchen()
    {
        mob[0].SetActive(true);
        DialogueManager.instance.PlayDlg(dlg[0]);
        while(!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        ActivateTrace(0);
    }

    public void OnCabinet()
    {
        StartCoroutine(AppearOnCabinetInKitchen());
    }

    IEnumerator AppearOnCabinetInKitchen()
    {
        mob[1].SetActive(true);
        DialogueManager.instance.PlayDlg(dlg[0]);
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        ActivateTrace(1);
    }

    public void LastInKitchen()
    {
        StartCoroutine(AppearLastInKitchen());
    }

    IEnumerator AppearLastInKitchen()
    {
        mob[2].SetActive(true);
        DialogueManager.instance.PlayDlg(dlg[0]);
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        ActivateTrace(2);
    }

    //추적 시작시키기
    public void ActivateTrace(int num)
    {
        matp[num].IsTrace = true;
    }
}
