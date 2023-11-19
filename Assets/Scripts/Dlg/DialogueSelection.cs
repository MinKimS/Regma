using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSelection : MonoBehaviour
{
    int selectIdx = 0;
    public Image[] btnLists;
    public Dialogue dlg;

    public Frame frame;

    private void Start()
    {
        SelectBtn();
    }
    void Update()
    {
        //위의 버튼으로 이동
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectIdx > 0)
            {
                selectIdx--;
                SelectBtn();
            }
        }
        //아래의 버튼으로 이동
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectIdx < btnLists.Length - 1)
            {
                selectIdx++;
                SelectBtn();
            }
        }
        //선택된 버튼 클릭
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(selectIdx == 0)
            {
                frame.isOnlyChild = true;
            }
            else if(selectIdx == 1)
            {
                frame.isOnlyChild = false;
            }
            DialogueManager.instance._dlgState = DialogueManager.DlgState.End;
            DialogueManager.instance.PlayDlg(dlg);
            frame.isOkErase = true;
            gameObject.SetActive(false);
        }
    }

    void SelectBtn()
    {
        btnLists[selectIdx].color = Color.white;
        for (int i = 0; i < btnLists.Length; i++)
        {
            if (i == selectIdx)
            { continue; }
            btnLists[i].color = Color.gray;
        }
    }
}
