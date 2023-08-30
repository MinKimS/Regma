using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    //start btn
    int btnSelectIdx = 1;
    public Image[] btnLists;

    private void Start() {
        SelectBtn();
    }
    void Update()
    {
        //위의 버튼으로 이동
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(btnSelectIdx < btnLists.Length-1)
            {
                btnSelectIdx++;
                SelectBtn();
            }
        }
        //아래의 버튼으로 이동
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(btnSelectIdx > 0)
            {
                btnSelectIdx--;
                SelectBtn();
            }
        }
        //선택된 버튼 클릭
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(btnSelectIdx == 1)
            {
                LoadingManager.LoadScene("Intro");
            }
            else
            {
                Application.Quit();
                print("quit");
            }
        }
    }

    void SelectBtn()
    {
        btnLists[btnSelectIdx].color = Color.gray;
        for(int i = 0; i < btnLists.Length; i++)
        {
            if(i == btnSelectIdx)
            {continue;}
            btnLists[i].color = Color.white;
        }
    }
}
