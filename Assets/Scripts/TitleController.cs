using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    int btnSelectIdx = 1;
    public BtnData[] btnLists;
    private void Start() {
        SelectBtn();
    }
    void Update()
    {
        //위의 버튼으로 이동
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(btnSelectIdx < btnLists.Length)
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
            if(btnLists[btnSelectIdx].btnID==0)
            {
                LoadingManager.LoadScene("Intro");
            }
            if(btnLists[btnSelectIdx].btnID==1)
            {
                Application.Quit();
                print("quit");
            }
        }
    }

    void SelectBtn()
    {
        btnLists[btnSelectIdx].btnImg.color = Color.gray;
        for(int i = 0; i < btnLists.Length; i++)
        {
            if(i == btnSelectIdx)
            {continue;}
            btnLists[i].btnImg.color = Color.white;
        }
    }
}
