using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    //start btn
    int btnSelectIdx = 2;
    public Image[] btnLists;
    public Transform howToScreen;

    private void Start() {
        SelectBtn();
    }
    void Update()
    {
        //위의 버튼으로 이동
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if(btnSelectIdx < btnLists.Length-1)
            {
                btnSelectIdx++;
                SelectBtn();
            }
        }
        //아래의 버튼으로 이동
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
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
            if(btnSelectIdx == 2)
            {
                LoadingManager.LoadScene("Intro");
            }
            else if(btnSelectIdx == 1)
            {
                if(GameManager.instance.GetLastScene() != SceneManager.GetActiveScene().name)
                {
                    LoadingManager.LoadScene(GameManager.instance.GetLastScene());
                }
            }
            else
            {
                Application.Quit();
                print("quit");
            }
        }

        ////조작법 창 숨기기
        //if(Input.GetKeyDown(KeyCode.Escape) && howToScreen.gameObject.activeSelf)
        //{
        //    howToScreen.gameObject.SetActive(false);
        //}
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
