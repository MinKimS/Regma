using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSelection : MonoBehaviour
{
    int selectIdx = 0;
    public Image[] btnLists;
    public Dialogue dlg;

    private void Start()
    {
        SelectBtn();
    }
    void Update()
    {
        //���� ��ư���� �̵�
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectIdx > 0)
            {
                selectIdx--;
                SelectBtn();
            }
        }
        //�Ʒ��� ��ư���� �̵�
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectIdx < btnLists.Length - 1)
            {
                selectIdx++;
                SelectBtn();
            }
        }
        //���õ� ��ư Ŭ��
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(selectIdx == 0)
            {
                print("11111");
                DialogueManager.instance.PlayDlg(dlg);
            }
            else if(selectIdx == 1)
            {
                print("222222");
                DialogueManager.instance.PlayDlg(dlg);
            }
        }
    }

    void SelectBtn()
    {
        btnLists[selectIdx].color = Color.gray;
        for (int i = 0; i < btnLists.Length; i++)
        {
            if (i == selectIdx)
            { continue; }
            btnLists[i].color = Color.white;
        }
    }
}
